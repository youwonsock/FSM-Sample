    namespace TestFSMStates
    {
        public class Init : State<TestFSMEntity>
        {
            public override void Enter(TestFSMEntity entity)
            {
                entity.Hp = 3000;
                entity.Mp = 3000;
                entity.Speed = 10;
                Debug.Log($"{entity.gameObject.name} is Start Init State");
            }

            public override void Execute(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Init State");
             
                entity.ChangeState(TestStates.Search);
            }

            public override void Exit(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Init State");
            }
        }

        public class Search : State<TestFSMEntity>
        {
            public override void Enter(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Start Search State");

                // NavMeshAgent를 이용한 patrol
                entity.NavMeshAgent.SetDestination(entity.PatrolPos1.position);
            }

            public override void Execute(TestFSMEntity entity)
            {
                // NavMeshAgent를 이용한 patrol
                if(Vector3.Distance(entity.transform.position, entity.PatrolPos1.position) < 2)
                    entity.NavMeshAgent.SetDestination(entity.PatrolPos2.position);
                else if(Vector3.Distance(entity.transform.position, entity.PatrolPos2.position) < 2)
                    entity.NavMeshAgent.SetDestination(entity.PatrolPos1.position);


                if (entity.Hp < 700)
                    entity.ChangeState(TestStates.Tracking);

                entity.Hp--;

                Debug.Log($"{entity.gameObject.name} is Search State");
            }

            public override void Exit(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Search State");
            }
        }

        public class Tracking : State<TestFSMEntity>
        {
            public override void Enter(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Start Tracking State");
            }

            public override void Execute(TestFSMEntity entity)
            {
                // NavMeshAgent를 이용한 Player 추적
                entity.NavMeshAgent.SetDestination(entity.targetTransform.position);
                

                if (entity.Mp < 500)
                    entity.ChangeState(TestStates.Attack);

                entity.Mp--;
                Debug.Log($"{entity.gameObject.name} is Tracking State");
            }

            public override void Exit(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Tracking State");

                // NavMeshAgent를 이용한 patrol
                entity.NavMeshAgent.SetDestination(entity.PatrolPos1.position);
            }
        }

        public class Attack : State<TestFSMEntity>
        {
            public override void Enter(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Start Attack State");
            }
            public override void Execute(TestFSMEntity entity)
            {
                if(entity.Hp < 0)
                    entity.ChangeState(TestStates.Die);

                entity.Hp--;
                Debug.Log($"{entity.gameObject.name} is Attack State");
            }
            public override void Exit(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Attack State");
            }
        }

        public class Die : State<TestFSMEntity>
        {
            public override void Enter(TestFSMEntity entity)
            {
                entity.Mp = 3000;
                entity.Hp = 3000;
                entity.ChangeState(TestStates.Search);
            }
            public override void Execute(TestFSMEntity entity)
            {
            }
            public override void Exit(TestFSMEntity entity)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Die State");
            }
        }

        public class StateGlobal : State<TestFSMEntity>
        {
            public override void Enter(TestFSMEntity entity)
            {
            }

            public override void Execute(TestFSMEntity entity)
            {
                if(Random.Range(0,100) == 0)
                {
                    Debug.Log($"This is {entity.gameObject} Global State!!!!");
                }
            }

            public override void Exit(TestFSMEntity entity)
            {
            }
        }
    }
