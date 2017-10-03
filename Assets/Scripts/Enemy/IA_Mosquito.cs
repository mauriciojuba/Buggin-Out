using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Mosquito : EnemyIA {

	public GameObject[] hitbox;
	Vector3 _tamanhoNaTela = new Vector3(2f,2f,2f);
	GameObject LifeEmblem;
	Vector3 mov;

	public override void Start ()
	{
		base.Start ();
		LifeEmblem = GameObject.Find ("LifeEmblem");
	}



	#region Override EnemyIA

	public override void UpToScreen ()
	{
		if (Screen.GoToScreen (gameObject,_tamanhoNaTela)) {
			ActualState = State.OnScreenIdle;
		}
	}
	public override void OnScreenIdle ()
	{
		ActualState = State.OnScreenChase;
	}
	public override void OnScreenChase ()
	{
		LifeEmblemDrain ();
	}

	#endregion

	#region Ataques Mosquito

	public void LifeEmblemDrain(){
		float CamSpeed = 0.1f;
		mov = new Vector3 (LifeEmblem.transform.position.x - transform.position.x, LifeEmblem.transform.position.y - transform.position.y, 0);
		mov = Camera.main.transform.TransformVector (mov);
		if (mov.x >= 0.2f) {
			_anim.SetBool ("walkScreen", true);
			transform.Translate (mov * transform.localScale.magnitude * Time.deltaTime * CamSpeed, Space.World);
			transform.LookAt (Camera.main.transform, transform.up + mov);
			if (hitted) {
				ActualState = State.OnScreenDamage;
			}
		} else {
			ActualState = State.GoingToWorld;
		}

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
