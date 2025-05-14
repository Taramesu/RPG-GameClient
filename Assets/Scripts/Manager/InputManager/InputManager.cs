using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public enum KeyEventType
    {
        KeyDown,
        KeyUp,
        LongPress,
        KeyHold
    }

    public enum MouseEventType
    {
        ClickDown,
        ClickUp,
        LongPress,
        ClickHold
    }

    public partial class InputManager : MonoSingleton<InputManager>
    {
        private static readonly Dictionary<Key, Dictionary<KeyEventType, SortedList<int, List<Action>>>> keyEvents = new();
        private static readonly Dictionary<Key, Dictionary<KeyEventType, int>> currentPriorityIndex = new();
        private static readonly HashSet<Key> specialKeys = new();
        private static readonly Dictionary<Key, float> keyPressTime = new();
        private const float LongPressThreshold = 1.0f;
        private static readonly Dictionary<Key, bool> isLongPressTriggered = new();

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            specialKeys.Add(Key.Escape);
            StartCoroutine(KeyListener());
        }

        private static IEnumerator KeyListener() 
        {
            while(true) 
            {
                foreach (var key in keyEvents.Keys) 
                {
                    HandleKeyEvents(key, KeyEventType.KeyDown, Keyboard.current[key].wasPressedThisFrame);
                    HandleKeyEvents(key, KeyEventType.KeyUp, Keyboard.current[key].wasReleasedThisFrame);
                    HandleKeyHoldAndLongPress(key);
                }
                yield return null;
            }
        }

        /// <summary>
        /// 处理按下和抬起事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eventType"></param>
        /// <param name="isKeyEvent"></param>
        private static void HandleKeyEvents(Key key, KeyEventType eventType, bool isKeyEvent)
        {
            if (isKeyEvent) 
            {
                if (IsInputFieldFocused && !specialKeys.Contains(key))
                {
                    return;
                }
                if (eventType == KeyEventType.KeyDown) 
                {
                    keyPressTime[key] = Time.time;
                    isLongPressTriggered[key] = false;
                }
                else if(eventType == KeyEventType.KeyUp) 
                {
                    keyPressTime.Remove(key);
                }
                ExecuteNextKeyEvent(key, eventType);
            }
        }

        /// <summary>
        /// 处理按住和长按事件
        /// </summary>
        /// <param name="key"></param>
        private static void HandleKeyHoldAndLongPress(Key key)
        {
            if (Keyboard.current[key].ReadValue() > 0.5f && keyPressTime.ContainsKey(key)) 
            {
                if(Time.time - keyPressTime[key] >= LongPressThreshold && !isLongPressTriggered[key])
                {
                    isLongPressTriggered[key] = true;
                    HandleKeyEvents(key, KeyEventType.LongPress, true);
                }
                HandleKeyEvents(key, KeyEventType.KeyHold, true);
            }
        }

        public static void RegisterKeyEvent(Key key, Action action, int priority = 0, KeyEventType eventType = KeyEventType.KeyDown)
        {
            if (!keyEvents.ContainsKey(key))
            {
                keyEvents[key] = new();
                currentPriorityIndex[key] = new();
            }
            if (!keyEvents[key].ContainsKey(eventType))
            {
                keyEvents[key][eventType] = new();
                currentPriorityIndex[key][eventType] = -1;
            }
            if (!keyEvents[key][eventType].ContainsKey(priority))
            {
                keyEvents[key][eventType][priority] = new();
            }
            keyEvents[key][eventType][priority].Add(action);
        }

        public static void UnRegisterKeyEvent(Key key, Action action)
        {
            if(keyEvents.ContainsKey(key))
            {
                foreach(var eventType in keyEvents[key].Keys.ToList())
                {
                    foreach(var priority in keyEvents[key][eventType].Keys.ToList())
                    {
                        keyEvents[key][eventType][priority].Remove(action);
                        if (keyEvents[key][eventType][priority].Count == 0)
                        {
                            keyEvents[key][eventType].Remove(priority);
                        }
                    }
                    if (keyEvents[key][eventType].Count == 0)
                    {
                        keyEvents[key].Remove(eventType);
                        currentPriorityIndex[key].Remove(eventType);
                    }
                }
                if (keyEvents[key].Count == 0)
                {
                    keyEvents.Remove(key);
                    currentPriorityIndex.Remove(key);
                }
            }
        }

        /// <summary>
        /// 按优先级顺序执行按键事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eventType"></param>
        private static void ExecuteNextKeyEvent(Key key, KeyEventType eventType)
        {
            if(keyEvents.ContainsKey(key) && keyEvents[key].ContainsKey(eventType)) 
            {
                var priorities = keyEvents[key][eventType].Keys;
                if(priorities.Count > 0) 
                {
                    int nextPriority = currentPriorityIndex[key][eventType] + 1;
                    if(nextPriority >= priorities.Count)
                    {
                        nextPriority = 0;
                    }
                    currentPriorityIndex[key][eventType] = nextPriority;
                    var priority = priorities.ElementAt(nextPriority);
                    foreach(var action in keyEvents[key][eventType][priority]) 
                    {
                        action?.Invoke();
                    }
                }
                
            }
        }

        public static bool IsInputFieldFocused
        {
            get
            {
                var selected = EventSystem.current.currentSelectedGameObject;
                if(selected != null)
                {
                    return selected.GetComponent<TMPro.TMP_InputField>() != null || selected.GetComponent<UnityEngine.UI.InputField>() != null;
                }
                return false;
            }
        }
    }

    public partial class InputManager
    {
        private static readonly Dictionary<Key, Dictionary<MouseEventType, SortedList<int, List<Action>>>> ClickEvents = new();
        private static readonly Dictionary<Key, Dictionary<MouseEventType, int>> currentPriorityIndexClick = new();
        private static readonly HashSet<Key> specialClicks = new();
        private static readonly Dictionary<Key, float> ClickPressTime = new();
        private static readonly Dictionary<Key, bool> isLongPressTriggeredClick = new();
    }
}