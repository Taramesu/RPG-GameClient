using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace RpgGame
{
    public class EscapeKeyHandler : MonoBehaviour
    {
        private void Start()
        {
            var manager = InputManager.Instance;
            InputManager.RegisterKeyEvent(Key.Escape, ClearInputFieldFocus, 1, KeyEventType.KeyDown);
            InputManager.RegisterKeyEvent(Key.Escape, SomeOtherEscapeAction, 2, KeyEventType.KeyDown);
            InputManager.RegisterKeyEvent(Key.Escape, LongPressEscapeAction, 1, KeyEventType.LongPress);
            InputManager.RegisterKeyEvent(Key.Escape, KeyUpEscapeAction, 1, KeyEventType.KeyUp);
        }

        private void OnDestroy()
        {
            InputManager.UnRegisterKeyEvent(Key.Escape, ClearInputFieldFocus);
            InputManager.UnRegisterKeyEvent(Key.Escape, SomeOtherEscapeAction);
            InputManager.UnRegisterKeyEvent(Key.Escape, LongPressEscapeAction);
            InputManager.UnRegisterKeyEvent(Key.Escape, KeyUpEscapeAction);
        }

        private void ClearInputFieldFocus()
        {
            Debug.Log("Clearing input field focus");
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void SomeOtherEscapeAction()
        {
            Debug.Log("Executing other ESC action");
        }

        private void LongPressEscapeAction()
        {
            Debug.Log("Executing long press ESC action");
        }

        private void KeyUpEscapeAction()
        {
            Debug.Log("Executing key up ESC action");
        }
    }
}
