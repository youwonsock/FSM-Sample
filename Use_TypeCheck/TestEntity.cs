using UnityEngine;
using UnityEngine.AI;

namespace UGD.UnknownProject
{
    /// <summary>
    /// @brief Class Test용 Entity 클래스
    /// @details FSM Test용 임시 클래스입니다
    /// 이 클래스의 method와 field들은 구상한 FSM을 이용하기 위해서 반드시 필요한 요소들을 기술해둔 것입니다.
    /// 
    /// @date last change 2022/08/08
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [RequireComponent(typeof(NavMeshAgent))]
    public class TestFSMEntity : MonoBehaviour // Author : @yws
    {
        #region fields

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
            // stateMachine 생성
            stateMachine = new StateMachine<TestFSMEntity>();

            // namespace TestFSMStates에 정의된 상태 클래스들을 StateMachine의 states List에 추가합니다.
            stateMachine.AddState(new TestFSMStates.Init());
            stateMachine.AddState(new TestFSMStates.Search());
            stateMachine.AddState(new TestFSMStates.Tracking());
            stateMachine.AddState(new TestFSMStates.Attack());
            stateMachine.AddState(new TestFSMStates.Die());
            stateMachine.SetGlobalState(new TestFSMStates.StateGlobal());

            // stateMachine 초기화
            stateMachine.Setup(this, typeof(TestFSMStates.Init));
        }

        #endregion

        #region Unity Event

        private void Awake()
        {
            Setup();
            TryGetComponent<NavMeshAgent>(out navMeshAgent);
        }

        private void Update()
        {
            stateMachine.Execute();
        }

        #endregion
    }
}
