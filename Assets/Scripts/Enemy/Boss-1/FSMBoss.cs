using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBoss : MonoBehaviour {

	//estados
	public enum States {Idle, Stunned, Tired, Dash_3D, Dash_2D, AttackIdle, AttackTired};
	public States MyActualState = States.Idle;
	//Temporizadores
	private float TimerSpawnEnemies;
	[SerializeField] private float CDSpawnEnemies;

	private float StunedTime;
	[SerializeField] private float MaxStunedTime;

	private float TiredTime;
	[SerializeField] private float MaxTiredTime;

	//golpes
	[SerializeField] private int Hits;
	public int HitsReceived;
	//Distancias
	[SerializeField] private int MaxDistanceIdle;
	[SerializeField] private int MaxDistanceTired;


	//Players
	private GameObject Player1_3D, Player2_3D, Player3_3D, Player4_3D;
	//private GameObject Player1_2D, Player2_2D, Player3_2D, Player4_2D;
	//estancia do boss, 3d ou 2d.
	public bool Stance3D;

	//varaveis do boss
	private GameObject Boss_3D, Boss_2D;
	private Rigidbody RB_3D;
	private Rigidbody2D RB_2D;
	[SerializeField] private float Speed;
	private bool InDash;

	[SerializeField] private Transform StartDashPoint;
	[SerializeField] private Transform StartPoint;
	void Start () {
		if (GameObject.FindWithTag ("Player1_3D") != null)
			Player1_3D = GameObject.FindWithTag ("Player1_3D");
		if (GameObject.FindWithTag ("Player2_3D") != null)
			Player2_3D = GameObject.FindWithTag ("Player2_3D");
		if (GameObject.FindWithTag ("Player3_3D") != null)
			Player3_3D = GameObject.FindWithTag ("Player3_3D");
		if (GameObject.FindWithTag ("Player4_3D") != null)
			Player4_3D = GameObject.FindWithTag ("Player4_3D");

		Boss_3D = transform.Find ("Boss_3D").gameObject;
		Boss_2D = transform.Find ("Boss_2D").gameObject;

		RB_3D = Boss_3D.GetComponent<Rigidbody> ();
		RB_2D = Boss_2D.GetComponent<Rigidbody2D> ();

		TimerSpawnEnemies = CDSpawnEnemies;

		Stance3D = true;

		var RightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0.85f, 0, 0)).x;
		var ZBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, -0.5f, 0)).y;
		StartDashPoint.position = new Vector3 (RightBorder, transform.position.y, ZBorder);
	}
	
	// Update is called once per frame
	void Update () {
		switch (MyActualState) {
		case States.Idle:		   IdleState ();   break;
		case States.Stunned:	StunnedState ();   break;
		case States.Tired:		  TiredState ();   break;
		case States.Dash_3D:	Dash_3DState ();   break;
		case States.Dash_2D:	Dash_2DState ();   break;
		}

		if (Stance3D) {
			Boss_3D.SetActive (true);
			Boss_2D.SetActive (false);
		} else {
			Boss_3D.SetActive (false);
			Boss_2D.SetActive (true);
		}
	}

	void IdleState(){
		Boss_3D.transform.position = StartPoint.position;
		TimerSpawnEnemies += Time.deltaTime;
		if (TimerSpawnEnemies >= CDSpawnEnemies) {
			//função de spawnar inimigos.
			TimerSpawnEnemies = 0;
		}
		if (Player4_3D != null && Player3_3D != null && Player2_3D != null) {
			if (Vector3.Distance (transform.position, Player1_3D.transform.position) < MaxDistanceIdle ||
			    Vector3.Distance (transform.position, Player2_3D.transform.position) < MaxDistanceIdle ||
			    Vector3.Distance (transform.position, Player3_3D.transform.position) < MaxDistanceIdle ||
			    Vector3.Distance (transform.position, Player4_3D.transform.position) < MaxDistanceIdle) {
				MyActualState = States.AttackIdle;
			}
		} else if (Player3_3D != null && Player2_3D != null) {
			if (Vector3.Distance (transform.position, Player1_3D.transform.position) < MaxDistanceIdle ||
			    Vector3.Distance (transform.position, Player2_3D.transform.position) < MaxDistanceIdle ||
			    Vector3.Distance (transform.position, Player3_3D.transform.position) < MaxDistanceIdle) {
				MyActualState = States.AttackIdle;
			}
		} else if (Player2_3D != null) {
			if (Vector3.Distance (transform.position, Player1_3D.transform.position) < MaxDistanceIdle ||
			    Vector3.Distance (transform.position, Player2_3D.transform.position) < MaxDistanceIdle) {
				MyActualState = States.AttackIdle;
			}
		} else if(Player1_3D != null) {
			if (Vector3.Distance (transform.position, Player1_3D.transform.position) < MaxDistanceIdle) {
				MyActualState = States.AttackIdle;
			}
		}



		if (HitsReceived == Hits) {
			MyActualState = States.Stunned;
			HitsReceived = 0;
		}
	}

	void StunnedState(){
		//roda animação de stun.
		Boss_3D.transform.position = StartDashPoint.position;
		StunedTime += Time.deltaTime;
		if (StunedTime >= MaxStunedTime) {
			StunedTime = 0;
			Boss_3D.transform.position = StartDashPoint.position;
			MyActualState = States.Dash_3D;
		}
	}

	void Dash_3DState(){
		//roda a animação de dash.
		Stance3D = true;
		InDash = true;
		RB_3D.velocity = new Vector3 (-1 * Speed, 0, 0);
		var Leftborder = Camera.main.ViewportToWorldPoint (new Vector3 (0.01f, 0, 0)).x;
		if (Boss_3D.transform.position.x < Leftborder) {
			RB_3D.velocity = Vector3.zero;
			Boss_2D.transform.position = Boss_3D.transform.position;
			MyActualState = States.Dash_2D;
		}


	}

	void Dash_2DState(){
		//roda a animação de dash.
		Stance3D = false;
		InDash = true;
		RB_2D.velocity = new Vector2 (1 * Speed, 0);
		if (Boss_2D.transform.position.x > StartDashPoint.position.x) {
			RB_2D.velocity = Vector2.zero;
			Boss_3D.transform.position = Boss_2D.transform.position;
			MyActualState = States.Tired;
		}
	}

	void TiredState(){
		//roda animação de Tired.
		Stance3D = true;
		InDash = false;
		TiredTime += Time.deltaTime;
		if (TiredTime >= MaxTiredTime) {
			TiredTime = 0;
			//roda animação de voltar pro trono.
			//essa função sera chamada pela animação
			ReturnToIdle();
		}
	}



	void AttackIdleState(){
		//roda animação de ataque, e quando acabar volta pro Idle
		//essa função sera chamada pela animação
		ReturnToIdle();
	}

	void AttackTiredState(){
		//roda animação de ataque, e quando acabar volta pro Tired
		//essa função sera chamada pela animação
		ReturnToTired();
	}

	void ReturnToIdle(){
		MyActualState = States.Idle;
	}

	void ReturnToTired(){
		MyActualState = States.Tired;
	}
}
