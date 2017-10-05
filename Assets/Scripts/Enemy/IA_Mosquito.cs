using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Mosquito : EnemyIA {

	public GameObject[] hitbox;
	GameObject LifeEmblem;
	public float LifeDist = 0.4f;
	float initialTargetLife;
	Transform Parent;

	public override void Start ()
	{
		base.Start ();
		LifeEmblem = GameObject.Find ("LifeEmblem");
		onScreenScale = new Vector3(2f,2f,2f);
		Parent = transform.parent;
	}



	#region Override EnemyIA
	public override void OnScreenIdle ()
	{
		_anim.SetBool("GoingToScreen", false);
		ActualState = State.OnScreenChase;
	}
	public override void OnScreenChase ()
	{
		RB.isKinematic = true;
		LifeDist = Vector3.Distance(transform.position, LifeEmblem.transform.position);
		Debug.Log (LifeDist);
		if (LifeDist > 0.2f) {
			LifeEmblemChase ();
		} else {
			RB.velocity = Vector3.zero;
			_anim.SetBool("walkScreen", false);
			initialTargetLife = Target.GetComponent<PlayerLife> ().LifeAtual;
			ActualState = State.OnScreenAttack;
		}

	}
	public override void OnScreenAttack ()
	{
		float targetLife;
		targetLife = Target.GetComponent<PlayerLife> ().LifeAtual;
		if (targetLife >= initialTargetLife - onScreenAtkStr) {
			_anim.SetBool("LifeDrain", true);
			targetLife -= Time.fixedDeltaTime*10;
			Target.GetComponent<PlayerLife> ().LifeAtual = targetLife;
		} else {
			_anim.SetBool("LifeDrain", false);
			ActualState = State.GoingToWorld;
		}
	}

	public override void DownToGround ()
	{
		if (Screen.GoOffScreen (worldPos, gameObject, Parent)) {
			RB.isKinematic = false;
			RB.useGravity = true;
			onScreen = false;
			ActualState = State.Idle;
			_anim.SetBool("GoingToWorld", true);
			_anim.SetBool("UsingWings", true);
		}
	}

	#endregion

	#region Ataques Mosquito

	public void LifeEmblemChase(){
		Vector3 mov;
		mov = new Vector3 (LifeEmblem.transform.position.x - transform.position.x, LifeEmblem.transform.position.y - transform.position.y, 0);
		mov = Camera.main.transform.TransformVector (mov);
		_anim.SetBool("walkScreen", true);
		transform.Translate (mov * transform.localScale.magnitude * Time.deltaTime * screenSpeed, Space.World);
		transform.LookAt (Camera.main.transform, transform.up + mov);
	}

	public void HitBoxOn()
	{
		if (!onScreen)
			hitbox[0].GetComponent<Collider>().enabled = true;

		else
			hitbox[1].GetComponent<Collider>().enabled = true;
	}

	public void HitBoxOff()
	{
		if (!onScreen)
			hitbox[0].GetComponent<Collider>().enabled = false;

		else
			hitbox[1].GetComponent<Collider>().enabled = false;
	}

	#endregion

}
