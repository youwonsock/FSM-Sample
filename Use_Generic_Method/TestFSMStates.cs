 namespace TestFSMStates // Author : @yws
    {
        public class Init : IState<TestFSMEntity>
        {
            public void Enter(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                entity.Hp = 3000;
                entity.Mp = 3000;
                entity.Speed = 10;
                Debug.Log($"{entity.gameObject.name} is Start Init State");
            }

            public void Execute(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Init State");

                stateMachine.ChangeState<Search>();
            }

            public void Exit(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Init State");
            }
        }

        public class Search : IState<TestFSMEntity>
        {
            public void Enter(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Start Search State");

                // NavMeshAgent를 이용한 patrol
                entity.NavMeshAgent.SetDestination(entity.PatrolPos1);
            }

            public void Execute(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                // NavMeshAgent를 이용한 patrol
                if(Vector3.Distance(entity.transform.position, entity.PatrolPos1) < 2)
                    entity.NavMeshAgent.SetDestination(entity.PatrolPos2);
                else if(Vector3.Distance(entity.transform.position, entity.PatrolPos2) < 2)
                    entity.NavMeshAgent.SetDestination(entity.PatrolPos1);


                if (entity.Hp < 700)
                    stateMachine.ChangeState<Tracking>();

                entity.Hp--;

                Debug.Log($"{entity.gameObject.name} is Search State");
            }

            public void Exit(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Search State");
            }
        }

        public class Tracking : IState<TestFSMEntity>
        {
            public void Enter(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Start Tracking State");
            }

            public void Execute(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                // NavMeshAgent를 이용한 Player 추적
                entity.NavMeshAgent.SetDestination(entity.targetTransform.position);
                

                if (entity.Mp < 500)
                    stateMachine.ChangeState<Attack>();

                entity.Mp--;
                Debug.Log($"{entity.gameObject.name} is Tracking State");
            }

            public void Exit(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Tracking State");

                // NavMeshAgent를 이용한 patrol
                entity.NavMeshAgent.SetDestination(entity.PatrolPos1);
            }
        }

        public class Attack : IState<TestFSMEntity>
        {
            public void Enter(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Start Attack State");
            }
            public void Execute(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                if(entity.Hp < 0)
                    stateMachine.ChangeState<Die>();

                entity.Hp--;
                Debug.Log($"{entity.gameObject.name} is Attack State");
            }
            public void Exit(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Attack State");
            }
        }

        public class Die : IState<TestFSMEntity>
        {
            public void Enter(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                entity.Mp = 3000;
                entity.Hp = 3000;
                stateMachine.ChangeState<Search>();
            }
            public void Execute(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
            }
            public void Exit(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                Debug.Log($"{entity.gameObject.name} is Exit Die State");
            }
        }

        public class StateGlobal : IState<TestFSMEntity>
        {
            public void Enter(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
            }

            public void Execute(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
                if(Random.Range(0,100) < 50)
                {
                    Debug.Log($"This is {entity.gameObject} Global State!!!!");
                }
            }

            public void Exit(TestFSMEntity entity, StateMachine<TestFSMEntity> stateMachine)
            {
            }
        }
    }
