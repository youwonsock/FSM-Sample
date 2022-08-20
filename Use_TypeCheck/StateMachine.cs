using System;
using System.Collections.Generic;

namespace UGD.UnknownProject
{
	/// <summary>
	/// @brief Class FSM을 관리하기 위한 클래스
	/// @details StateMachine은 FSM을 가지는 각 객체가 소유하고 있습니다.
	/// StateMachine은 상태를 변경하고 실행시키는 처리만 담당하며 각 상태의 행동은 State<T>를 상속받는 각 클래스에서 처리합니다.
	/// 
	/// @date last change 2022/08/08
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StateMachine<T> where T : class // Author : @yws
	{
		private T ownerEntity;  // StateMachine의 소유주
		private IState<T> currentState;  // 현재 상태
		private IState<T> previousState; // 이전 상태
		private IState<T> globalState;   // 전역 상태

		// 상태를 저장하는 IState List
		private List<IState<T>> states = new List<IState<T>>();

		/// <summary>
		/// StateMachine의 초기화에 사용되는 메서드.
		/// </summary>
		/// <param name="owner"> StateMachine의 소유주 </param>
		/// <param name="entryState"> owner의 처음 상태 </param>
		public void Setup(T owner, Type entryState)
		{
			ownerEntity = owner;
			currentState = null;
			previousState = null;
			//globalState = null;

			// entryState 상태로 상태 변경
			ChangeState(entryState);
		}

		/// <summary>
		/// 현재 상태와 전역 상태를 실행시키는 메서드.
		/// null인 상태는 실행시키지 않습니다.
		/// </summary>
		public void Execute()
		{
			if (currentState != null)
			{
				currentState.Execute(ownerEntity, this);
			}

			if (globalState != null)
			{
                globalState.Execute(ownerEntity, this);
			}
		}

		/// <summary>
		/// 상태 변경 메서드.
		/// 매개변수 newState가 states List에 없는 타입일 경우 바꾸지 않습니다.
		/// </summary>
		/// <param name="nextState"> 다음 상태 </param>
		public void ChangeState(Type nextState)
		{
			for(int i=0; i<states.Count; i++)
			{
				if (states[i].GetType() == nextState)	// is는 상수 타입을 대상으로만 검사 가능!
                {
					// 현재 재생중인 상태가 있으면 현재 상태 Exit()
					if (currentState != null)
					{
						//현재 상태를 previousState에 저장
						previousState = currentState;

						currentState.Exit(ownerEntity, this);
					}

					// 다음 상태로 변경 및 다음 상태의 Enter() 호출
					currentState = states[i];
					currentState.Enter(ownerEntity, this);
				}
            }
		}

		/// <summary>
		/// 상태 추가 메서드
		/// </summary>
		/// <param name="state"> 상태 </param>
		public void AddState(IState<T> newState)
        {
			states.Add(newState);
        }

		/// <summary>
		/// 전역 상태 설정 매서드.
		/// </summary>
		/// <param name="newState"> 전역 상태 </param>
		public void SetGlobalState(IState<T> newState)
		{
			globalState = newState;
		}

		/// <summary>
		/// 이전 상태 복귀 메서드
		/// </summary>
		public void RevertToPreviousState()
		{
			ChangeState(previousState.GetType());
		}
	}
}
