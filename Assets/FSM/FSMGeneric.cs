using UnityEngine;
using System.Collections;

public class FSMGeneric : MonoBehaviour {

	#region FSM States
	public enum  FSMStates { Estado_1, Estado_2, Estado_3, Estado_4};
	public FSMStates state = FSMStates.Estado_1;
	#endregion

	#region Variaveis
	// Variaveis
	#endregion

	#region Unity Functions

	void Start () {
	
	}
	

	public void FixedUpdate(){
	

		//switch funciona como um "if" mas só para variaveis inteiras

		switch (state){

		case FSMStates.Estado_1:
			Estado_1_State ();
			break;

		case FSMStates.Estado_2:
			Estado_2_State ();
			break;

		case FSMStates.Estado_3:
			Estado_3_State ();
			break;

		case FSMStates.Estado_4:
			Estado_4_State ();
			break;
		}

	}
	#endregion

	#region Estado 1
	private void Estado_1_State(){

		// if (........)
		//state = FSMStates.Estado_2;
	}
	#endregion

	#region Estado 2
	private void Estado_2_State(){

	}
	#endregion

	#region Estado 3
	private void Estado_3_State(){

	}
	#endregion

	#region Estado 4
	private void Estado_4_State(){

	}
	#endregion
		
}
