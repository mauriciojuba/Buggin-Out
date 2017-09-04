using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

	[SerializeField] Animator Anim;

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
}
