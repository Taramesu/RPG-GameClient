using QFramework;
using RpgGame.Skill;
using System.Collections;
using UnityEngine;

namespace RpgGame
{
    public class Monster1FSM : MonoBehaviour, IController, ICanSendEvent
    {
        private string sUid;
        private Vector3 lastPosition;
        private Animator animator;
        private ObjData target;
        private Vector3 generatePos;
        private SkillManager skillManager;

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
            generatePos = transform.position;
            skillManager = GetComponent<SkillManager>();

            fsm.AddState(States.Idle, new Idle(fsm, this));
            fsm.AddState(States.Attack, new Attack(fsm, this));
            fsm.AddState(States.Chase, new Chase(fsm, this));
            fsm.AddState(States.Patrol, new Patrol(fsm, this));
            fsm.AddState(States.Back, new Back(fsm, this));

            fsm.StartState(States.Patrol);
            sUid = GetComponent<ObjMonoController>().GetsUid();
        }
        private void Update()
        {
            fsm.Update();
            MoveAninmationControll();
            lastPosition = transform.position;
        }

        private void OnDestroy()
        {
            fsm.Clear();
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
                //����
                var attackDir = mTarget.target.transform.position - mTarget.transform.position;
                Quaternion skillRotation = Quaternion.LookRotation(attackDir);
                mTarget.SendEvent(new AttackEvent() { sUid = mTarget.SUid, skillId = 1001, skillRotation = skillRotation });

                //����Ƿ��ڹ�����Χ
                var distance = Vector3.Distance(mTarget.target.transform.position, mTarget.transform.position);
                if (distance > 1.5)
                {
                    mFSM.ChangeState(States.Back);
                }
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
                Debug.Log("enter chase");
            }

            protected override void OnUpdate()
            {
                //�ƶ�
                var dir = Vector3.Normalize(mTarget.target.transform.position - mTarget.transform.position);
                mTarget.GetSystem<MoveSystem>().Move(mTarget.sUid, dir);

                //����Ƿ񵽴﹥������
                var distance = Vector3.Distance(mTarget.target.transform.position, mTarget.transform.position);
                if(distance <= 1.5)
                {
                    mFSM.ChangeState(States.Attack);
                }

                //����Ƿ��뿪׷����Χ
                if (distance > 3)
                {
                    mFSM.ChangeState(States.Back);
                }
            }

            protected override void OnExit()
            {
            }
        }

        public class Patrol : AbstractState<States, Monster1FSM>
        {
            private bool moveLeft;
            private bool patroling;
            public Patrol(FSM<States> fsm, Monster1FSM target) : base(fsm, target)
            {
            }

            protected override void OnEnter()
            {
                patroling = true;
                mTarget.StartCoroutine(Patroling());
                mTarget.target = null;
            }

            protected override void OnUpdate()
            {
                if(moveLeft)
                {
                    mTarget.GetSystem<MoveSystem>().Move(mTarget.sUid, Vector3.left);
                }
                else
                {
                    mTarget.GetSystem<MoveSystem>().Move(mTarget.sUid, Vector3.right);
                }

                //����Ŀ��
                var tree = GenerateTest.Instance.tree;
                var ex = new Vector3(3, 3, 3);
                Bounds bound = new() { center = mTarget.transform.position, extents = ex };
                var targets = tree.QueryBounds(bound);

                targets = targets.FindAll(o => mTarget.GetModel<EntityModel>().GetData(o.sUid).typeId == 0);
                if(targets.Count > 0)
                {
                    mTarget.target = targets[0];
                    if (Vector3.Distance(targets[0].transform.position, mTarget.transform.position) <= 3)
                    {
                        mFSM.ChangeState(States.Chase);
                    }
                }
            }

            private IEnumerator Patroling()
            {
                do
                {
                    yield return new WaitForSeconds(2);
                    moveLeft = !moveLeft;
                } while (patroling == true);
            }

            protected override void OnExit()
            {
                patroling = false;
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
                //����Ƿ�ص�Ѳ��λ
                if(Vector3.Distance(mTarget.transform.position, mTarget.generatePos) <= 0.1f)
                {
                    mFSM.ChangeState(States.Patrol);
                    return;
                }

                //����Ƿ����׷����Χ
                var distance = Vector3.Distance(mTarget.target.transform.position, mTarget.transform.position);
                if (distance <= 3)
                {
                    mFSM.ChangeState(States.Chase);
                    return;
                }

                //�ƶ�
                var dir = Vector3.Normalize(mTarget.generatePos - mTarget.transform.position);
                mTarget.GetSystem<MoveSystem>().Move(mTarget.sUid, dir);
            }

            protected override void OnExit()
            {
            }
        }

        private void MoveAninmationControll()
        {
            if (lastPosition.x < transform.position.x)
            {
                animator.Play("MoveRight");
            }
            else if (lastPosition.x >= transform.position.x)
            {
                animator.Play("MoveLeft");
            }
            else if (lastPosition.z < transform.position.z)
            {
                animator.Play("MoveUp");
            }
            else if (lastPosition.z >= transform.position.z)
            {
                animator.Play("MoveDown");
            }
        }
    }
}