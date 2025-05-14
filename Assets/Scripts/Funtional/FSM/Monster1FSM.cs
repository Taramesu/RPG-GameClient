using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class Monster1FSM : MonoBehaviour, IController
    {
        private string sUid;
        private Vector3 lastPosition;
        private Animator animator;

        public string SUid
        {
            get { return sUid; }
            set { sUid = value; }
        }

        public enum States
        {
            Idle,
            Attack,
            Chase,
            Patrol,
            Back,
        }

        public FSM<States> fsm = new();

        private void Start()
        {
            animator = GetComponent<Animator>();
            lastPosition = transform.position;
        }

        public IArchitecture GetArchitecture() => RpgGame.Interface;

        public class Idle : AbstractState<States, Monster1FSM>
        {
            public Idle(FSM<States> fsm, Monster1FSM target) : base(fsm, target) { }


            protected override void OnEnter()
            {
                mTarget.animator.speed = 0;
            }

            protected override void OnUpdate()
            {
                
            }

            protected override void OnExit()
            {
                mTarget.animator.speed = 1;
            }
        }

        public class Attack : AbstractState<States, Monster1FSM>
        {
            public Attack(FSM<States> fsm, Monster1FSM target) : base(fsm, target)
            {
            }

            protected override void OnEnter()
            {
            }

            protected override void OnUpdate()
            {

            }

            protected override void OnExit()
            {
            }
        }

        public class Chase : AbstractState<States, Monster1FSM>
        {
            public Chase(FSM<States> fsm, Monster1FSM target) : base(fsm, target)
            {
            }

            protected override void OnEnter()
            {
            }

            protected override void OnUpdate()
            {

            }

            protected override void OnExit()
            {
            }
        }

        public class Patrol : AbstractState<States, Monster1FSM>
        {
            public Patrol(FSM<States> fsm, Monster1FSM target) : base(fsm, target)
            {
            }

            protected override void OnEnter()
            {
            }

            protected override void OnUpdate()
            {

            }

            protected override void OnExit()
            {
            }
        }

        public class Back : AbstractState<States, Monster1FSM>
        {
            public Back(FSM<States> fsm, Monster1FSM target) : base(fsm, target)
            {
            }

            protected override void OnEnter()
            {
            }

            protected override void OnUpdate()
            {

            }

            protected override void OnExit()
            {
            }
        }
    }
}