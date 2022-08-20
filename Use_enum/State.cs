  /// <summary>
	/// @brief Class FSM의 상태들의 배이스 클래스
	/// @details 추상 클래스인 State는 상태 클래스의 배이스 클래스로 사용되며 T는 상태를 소유하는 클래스입니다 
	///	ex) test_entity는 State<test_entity>를 상태로 가집니다.
	/// 
	/// @date last change 2022/08/06
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public abstract class State<T> where T : class // Author : @yws
	{
		/// <summary>
		/// 해당 상태를 시작할 때 1회 호출
		/// </summary>
		public abstract void Enter(T entity);

		/// <summary>
		/// 해당 상태를 업데이트할 때 매 프레임 호출
		/// </summary>
		public abstract void Execute(T entity);

		/// <summary>
		/// 해당 상태를 종료할 때 1회 호출
		/// </summary>
		public abstract void Exit(T entity);
	}
