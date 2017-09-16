using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

	[SerializeField] Animator Anim;

	[SerializeField] GameObject RightFightCol, LeftFightCol, HeadFightCol, RightFootFightCol;

	public void SetAttackAnim(int AttackNumber){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("Attack");
		Anim.SetInteger ("AttackNumber", AttackNumber);
	}

	public void SetMovimentAnimation(float Velocity){
		Anim.SetFloat("Mov", Velocity);
	}

	public void SetJumpAnim(){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("Jump");
	}
	public void SetGrounded(bool Grounded){
		Anim.SetBool ("grounded", Grounded);
	}

	public void SetTakeDamageAnim(){
		Anim.SetTrigger ("TakeDamage");
	}

	#region Functions Active/Desactive Fight Colliders
	public void ActiveRightFightCollider(float Damage){
		RightFightCol.GetComponent<FightCollider> ().Damage = Damage;
		RightFightCol.SetActive (true);
	}

	public void DesactiveRightFightCollider(){
		RightFightCol.SetActive (false);
	}

	public void ActiveLeftFightCollider(float Damage){
		LeftFightCol.GetComponent<FightCollider> ().Damage = Damage;
		LeftFightCol.SetActive (true);
	}

	public void DesactiveLeftFightCollider(){
		LeftFightCol.SetActive (false);
	}

	public void ActiveHeadFightCollider(float Damage){
		HeadFightCol.GetComponent<FightCollider> ().Damage = Damage;
		HeadFightCol.SetActive (true);
	}

	public void DesactiveHeadFightCollider(){
		HeadFightCol.SetActive (false);
	}

	public void ActiveRightFootFightCollider(float Damage){
		RightFootFightCol.GetComponent<FightCollider> ().Damage = Damage;
		RightFootFightCol.SetActive (true);
	}

	public void DesactiveRightFootFightCollider(){
		RightFootFightCol.SetActive (false);
	}
	#endregion
}
