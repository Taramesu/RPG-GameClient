using QFramework;
using UnityEngine;

namespace RpgGame
{
    public class PlayerFSM : MonoBehaviour, IController
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
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
        }

        public FSM<States> fsm = new();

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            lastPosition = transform.position;

            fsm.AddState(States.Idle, new Idle(fsm, this));
            fsm.AddState(States.MoveUp, new MoveUp(fsm, this));
            fsm.AddState(States.MoveDown, new MoveDown(fsm, this));
            fsm.AddState(States.MoveLeft, new MoveLeft(fsm, this));
            fsm.AddState(States.MoveRight, new MoveRight(fsm, this));
            fsm.StartState(States.Idle);
        }

        private void Update()
        {
            fsm.Update();
            lastPosition = transform.position;
        }

        private void OnDestroy()
        {
            fsm.Clear();
        }

        public IArchitecture GetArchitecture()
        {
            return RpgGame.Interface;
        }

        public class Idle : AbstractState<States, PlayerFSM> 
        {
            public Idle(FSM<States> fsm, PlayerFSM target) : base(fsm, target) { }

            protected override void OnEnter()
            {
                mTarget.animator.speed = 0;
            }

            protected override void OnUpdate()
            {
                if(mTarget.lastPosition.x < mTarget.transform.position.x)
                {
                    mFSM.ChangeState(States.MoveRight);
                }
                else if(mTarget.lastPosition.x > mTarget.transform.position.x)
                {
                    mFSM.ChangeState(States.MoveLeft);
                }
                else if(mTarget.lastPosition.z < mTarget.transform.position.z)
                {
                    mFSM.ChangeState(States.MoveUp);
                }
                else if(mTarget.lastPosition.z > mTarget.transform.position.z)
                {
                    mFSM.ChangeState(States.MoveDown);
                }
            }

            protected override void OnExit()
            {
                mTarget.animator.speed = 1;
            }
        }

        public class MoveUp : AbstractState<States, PlayerFSM>
        {
            public MoveUp(FSM<States> fsm, PlayerFSM target) : base(fsm, target) { }

            protected override void OnEnter()
            {
                mTarget.animator.Play(mFSM.CurrentStateId.ToString());
            }

            protected override void OnUpdate()
            {
                if(mTarget.lastPosition.z >= mTarget.transform.position.z)
                {
                    mFSM.ChangeState(States.Idle);
                }
            }
        }

        public class MoveDown : AbstractState<States, PlayerFSM>
        {
            public MoveDown(FSM<States> fsm, PlayerFSM target) : base(fsm, target) { }

            protected override void OnEnter()
            {
                mTarget.animator.Play(mFSM.CurrentStateId.ToString());
            }

            protected override void OnUpdate()
            {
                if (mTarget.lastPosition.z <= mTarget.transform.position.z)
                {
                    mFSM.ChangeState(States.Idle);
                }
            }
        }

        public class MoveLeft : AbstractState<States, PlayerFSM>
        {
            public MoveLeft(FSM<States> fsm, PlayerFSM target) : base(fsm, target) { }

            protected override void OnEnter()
            {
                mTarget.animator.Play(mFSM.CurrentStateId.ToString());
            }

            protected override void OnUpdate()
            {
                if (mTarget.lastPosition.x <= mTarget.transform.position.x)
                {
                    mFSM.ChangeState(States.Idle);
                }
            }
        }

        public class MoveRight : AbstractState<States, PlayerFSM>
        {
            public MoveRight(FSM<States> fsm, PlayerFSM target) : base(fsm, target) { }

            protected override void OnEnter()
            {
                mTarget.animator.Play(mFSM.CurrentStateId.ToString());
            }

            protected override void OnUpdate()
            {
                if (mTarget.lastPosition.x >= mTarget.transform.position.x)
                {
                    mFSM.ChangeState(States.Idle);
                }
            }
        }
    }
}