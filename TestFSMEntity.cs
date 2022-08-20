    public enum TestStates { Init = 0, Search, Tracking, Attack, Die, Global }

    /// <summary>
    /// @brief Class Test용 Entity 클래스
    /// @details FSM Test용 임시 클래스입니다
    /// 이 클래스의 method와 field들은 구상한 FSM을 이용하기 위해서 반드시 필요한 요소들을 기술해둔 것입니다.
    /// 
    /// @date last change 2022/08/06
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [RequireComponent(typeof(NavMeshAgent))]
    public class TestFSMEntity : MonoBehaviour // Author : @yws
    {
        #region fields

        private State<TestFSMEntity>[] states;
        private StateMachine<TestFSMEntity> stateMachine;

        // 추적은 navMesh를 이용할 예정
        private NavMeshAgent navMeshAgent;

        // Test를 위해서 Transform을 임시로 저장
        public Transform targetTransform;
        public Transform PatrolPos1;
        public Transform PatrolPos2;

        #endregion



        #region property

        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Speed { get; set; }
        public NavMeshAgent NavMeshAgent { get { return navMeshAgent; } }

        #endregion



        #region methods
    
        /// <summary>
        /// 초기화 메서드
        /// </summary>
        public void Setup()
        {
            // namespace TestFSMStates에 정의된 상태 클래스들로 states배열을 초기화합니다.
            // 코드가 아닌 인스펙터에서 초기화하는 경우 각각의 상태 클래스를 별도의 .cs파일로 생성해야합니다.
            states = new State<TestFSMEntity>[6];
            states[(int)TestStates.Init] = new TestFSMStates.Init();
            states[(int)TestStates.Search] = new TestFSMStates.Search();
            states[(int)TestStates.Tracking] = new TestFSMStates.Tracking();
            states[(int)TestStates.Attack] = new TestFSMStates.Attack();
            states[(int)TestStates.Die] = new TestFSMStates.Die();

            //전역 상태
            states[(int)TestStates.Global] = new TestFSMStates.StateGlobal();

            // stateMachine 초기화 및 초기 상태, 전역 상태 설정
            stateMachine = new StateMachine<TestFSMEntity>();
            stateMachine.Setup(this, states[(int)TestStates.Init]);
            stateMachine.SetGlobalState(states[(int)TestStates.Global]);
        }

        /// <summary>
        /// 상태 변경 메서드
        /// </summary>
        /// <param name="nextState"> 다음 상태 </param>
        public void ChangeState(TestStates nextState)
        {
            stateMachine.ChangeState(states[(int)nextState]);
        }

        /// <summary>
        /// 이전 상태 복귀 메서드
        /// </summary>
        public void RevertToPreviousState()
        {
            stateMachine.RevertToPreviousState();
        }

        #endregion

        #region Unity Event

        private void Awake()
        {
            Setup();
            TryGetComponent<NavMeshAgent>(out navMeshAgent);
        }

        private void FixedUpdate()
        {
            stateMachine.Execute();
        }

        #endregion
    }
