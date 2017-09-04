using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApplyCombo: MonoBehaviour{
	private Combo combo1 = new Combo(new string[] {"X","X","X"});
	private Combo combo2 = new Combo(new string[] {"X","X","Y"});
	[SerializeField] private Movimentacao3D SetAnim;
	[SerializeField] private Animator Anim;
	[SerializeField] private Fight DamageRef;
	public List<int> IndexCombo;
	public bool Attacking;
	public bool ChargingAttack;
	public bool ChargedAttack;

	void Start(){
		combo1.PlayerNumber = GetComponent<PlayerNumb> ().PlayerNumber;
		combo2.PlayerNumber = GetComponent<PlayerNumb> ().PlayerNumber;
		//AnimRef = GetComponent<Movimentacao3D> ();
	}

	void Update(){
//		if (Input.GetButtonDown ("Y P" + AnimRef.PlayerNumber)) {
//			ChargingAttack = true;
//			AnimRef.SetStrongAttackAnim ("Charging");
//			DamageRef.StrongDamage = DamageRef.MinStrongDamage;
//		}
//		if (Input.GetButtonUp ("Y P" + AnimRef.PlayerNumber)) {
//			ExecuteChargedAttack ();
//		}
//		if (!ChargingAttack) {
	//	if(!AnimRef.InDialogue){
		if (combo1.CheckCombo ()) {
//				if (!IndexCombo.Contains (combo1.CurrentIndex)) {
//					IndexCombo.Add (combo1.CurrentIndex);
//				}
			IndexCombo.Add (combo1.currentButton);
			SetAnim.SetAttackAnim (IndexCombo [0]);
			Attacking = combo1.Attacking;
		}
		if (combo2.CheckCombo ()) {
			//				if (!IndexCombo.Contains (combo1.CurrentIndex)) {
			//					IndexCombo.Add (combo1.CurrentIndex);
			//				}
			IndexCombo.Add (combo2.currentButton);
			SetAnim.SetAttackAnim (IndexCombo [0]);
			Attacking = combo2.Attacking;
		}
			if (IndexCombo.Count > 0) {
			if (Anim.GetCurrentAnimatorStateInfo (1).IsName ("Attack " + IndexCombo [0])) {
					if (IndexCombo.Count > 1) {
					SetAnim.SetAttackAnim (IndexCombo [1]);
						IndexCombo.Remove (IndexCombo [0]);
					} else {
					SetAnim.SetAttackAnim (0);
						IndexCombo.Clear ();
					}
				}

				if (!Attacking) {
				SetAnim.SetAttackAnim (0);
					IndexCombo.Clear ();
				}
			}
			if (Time.time > combo1.TimeLastButtonPressed + combo1.TimeBetweenButtons) {
				Attacking = false;
			}
		Anim.SetBool ("Attacking", Attacking);
//		} if(ChargingAttack) {
//			SetChargedAttack ();
//		}
//
//		if(!ChargedAttack)
//			AnimRef.CanMove = true;
//		
	//	}
	}

	void SetChargedAttack(){
		if (ChargingAttack) {
			//AnimRef.CanMove = false;
			if (DamageRef.StrongDamage < DamageRef.MaxStrongDamage) {
				DamageRef.StrongDamage += 10 * Time.deltaTime;
			} else {
				ExecuteChargedAttack ();
				ChargingAttack = false;
			}
		}
	}

	void ExecuteChargedAttack(){
		ChargedAttack = true;
		//AnimRef.SetStrongAttackAnim ("Release");
	}
}



public class Combo{
	public string[] Buttons;
	public int PlayerNumber;
	public int CurrentIndex = 0;
	public int currentButton;
	public bool Attacking;
	public Movimentacao3D Mov;

	public float TimeBetweenButtons = 0.4f;
	public float TimeLastButtonPressed;
	public Combo(string[] b){
		Buttons = b;
	}

	public bool CheckCombo(){
		if (Time.time > TimeLastButtonPressed + TimeBetweenButtons) {
			CurrentIndex = 0;
			Attacking = false;
		}
		if (CurrentIndex < Buttons.Length) {
//			if (Mov.KeyboardCanControl) {
//				if (Buttons [CurrentIndex] == "X" && Input.GetButtonDown ("X P" + PlayerNumber) || Buttons [CurrentIndex] == "X" && Input.GetKeyDown (KeyCode.J) ||
//				    Buttons [CurrentIndex] == "Y" && Input.GetButtonDown ("Y P" + PlayerNumber) || Buttons [CurrentIndex] == "Y" && Input.GetKeyDown (KeyCode.I)) {
//					TimeLastButtonPressed = Time.time;
//					CurrentIndex++;
//					Attacking = true;
//					return true;
//				}
//			} else {
				if (Buttons [CurrentIndex] == "X" && Input.GetButtonDown ("X P" + PlayerNumber) ||
					Buttons [CurrentIndex] == "Y" && Input.GetButtonDown ("Y P" + PlayerNumber)) {

				if (Buttons [CurrentIndex] == "X") {
					currentButton = 1;
				} else if (Buttons [CurrentIndex] == "Y") {
					currentButton = 2;
				}
					TimeLastButtonPressed = Time.time;
					CurrentIndex++;
					Attacking = true;
					return true;
				}
			//}
		}
		if (CurrentIndex >= Buttons.Length) {
			CurrentIndex = 0;
		}
		return false;
	}

}
