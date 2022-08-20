namespace UGD.UnknownProject

{   /// <summary>
	/// @brief Interface FSM의 상태들의 Interface
	/// @details State는 상태 클래스의 인터페이스로 사용되며 T는 상태를 소유하는 클래스입니다 
	///	ex) test_entity의 StateMachine는 IState<test_entity>를 상태로 가집니다.
	/// 
	/// @date last change 2022/08/08
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IState<T> where T : class //@Author : @yws
    {
		/// <summary>
		/// 해당 상태를 시작할 때 1회 호출
		/// </summary>
		public void Enter(T entity, StateMachine<T> stateMachine);

		/// <summary>
		/// 해당 상태를 업데이트할 때 매 프레임 호출
		/// </summary>
		public void Execute(T entity, StateMachine<T> stateMachine);

		/// <summary>
		/// 해당 상태를 종료할 때 1회 호출
		/// </summary>
		public void Exit(T entity, StateMachine<T> stateMachine);
	}
}
