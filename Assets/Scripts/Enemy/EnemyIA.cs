using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour,IGroundEnemy,IKillable, IAnimated<Animator>,IGoToScreen,IScreenEntity {

	[HideInInspector]
	public Rigidbody RB;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public enum State 	{Idle, Patrol, Chase, Flee, Attack, TakeDamage,
						Dead, GoingToScreen, OnScreenIdle, OnScreenChase, 
						OnScreenAttack, GoingToWorld, OnScreenDamage};
	[HideInInspector]
	public GameObject player1, player2;
	[HideInInspector]
	public List<GameObject> players;
	[HideInInspector]
	public float[] playersDist = new float[2];
	[HideInInspector]
	float Life;

	[Header("Estado atual")]
	public State ActualState = State.Chase;

	[Header("Foco do inimigo")]
	public GameObject Target;

	[Header("Configurações de Combate")]
	public float lifeMax;
	public float attackStr;
	public float attackDelay;
	public bool hitted;

	protected float attackTimer;

	[Header("Configurações de Movimento")]
	public float speed;
	public float rotationSpeed;
	public float chaseSpeed;
	public float fleeSpeed;
	public bool onScreen;

	[Header("WayPoint")]
	public Transform[] waypoints;
	public float TimeToNextPoint = 5f;

	protected float TimeTo;
	protected int currentWaypoint;

	[Header("Configurações de Alcance")]
	public float Visao;
	public float SafeDist;
	public float EnemyDist;

	protected float targetDistance;

	public void Start()
	{
		RB = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		Life = lifeMax;
		CheckAllTargets ();
		CalculaDistancia ();
	}

	public void FixedUpdate()
	{
		targetDistance = Vector3.Distance(Target.transform.position, gameObject.transform.position);
		switch (ActualState)
		{
		case State.Idle: Idle(); break;
		case State.Patrol: Patrol(); break;
		case State.Chase: Chase(); break;
		case State.Attack: Attack(); break;
		case State.TakeDamage: TakeDamage(); break;
		case State.Flee: Flee(); break;
		case State.Dead: Die(); break;
		case State.GoingToScreen: UpToScreen(); break;
		case State.GoingToWorld: DownToGround (); break;
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, Visao);
		Gizmos.color = Color.red;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, SafeDist);
	}

	#region IGroundEnemy
	public void CheckAllTargets()
	{
		if (GameObject.FindGameObjectWithTag ("Player1_3D") != null) {
			player1 = GameObject.FindGameObjectWithTag ("Player1_3D");
			players.Add (player1);
			if (GameObject.FindGameObjectWithTag ("Player2_3D") != null) {
				player2 = GameObject.FindGameObjectWithTag ("Player2_3D");
				players.Add (player2);
			}
		}
		else if (GameObject.FindGameObjectWithTag ("Player2_3D") != null) {
			player2 = GameObject.FindGameObjectWithTag ("Player2_3D");
			players.Add (player2);
		}
	}
	public void CalculaDistancia()
	{
		if (players.Count > 1)
		{
			for (int i = 0; i < players.Count; i++)
			{
				playersDist[i] = Vector3.Distance(players[i].transform.position, gameObject.transform.position);
			}

			if (playersDist[0] < playersDist[1])
				Target = players[0];
			else
				Target = players[1];
		}
		else if (players.Count == 1)
		{
			Target = players[0];
		}
	}
	public virtual void Idle()
	{
		//MosquitoAni.SetBool("IsIdle", true);

		if (Vector3.Distance(Target.transform.position, gameObject.transform.position) < Visao)
		{
			//MosquitoAni.SetBool("IsIdle", false);
			ActualState = State.Chase;
		}
		TimeToNextPoint -= Time.deltaTime;
		if (TimeToNextPoint < 0)
		{
			currentWaypoint++;
			CalculaDistancia ();
			if (currentWaypoint >= waypoints.Length)
				currentWaypoint = 0;
			TimeToNextPoint = TimeTo;

			//MosquitoAni.SetBool("IsIdle", false);
			ActualState = State.Patrol;
		}
		if (hitted)
			ActualState = State.TakeDamage;
	}
	public virtual void Patrol()
	{
		Vector3 dir = waypoints[currentWaypoint].position - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		if (dir.sqrMagnitude <= 1)
		{
			//MosquitoAni.SetBool("IsParolling", false);
			ActualState = State.Idle;
		}
		else
			RB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);

		if (targetDistance < Visao)
		{
			//MosquitoAni.SetBool("IsParolling", false);
			ActualState = State.Chase;
		}
		if (hitted)
			ActualState = State.TakeDamage;
	}
	public virtual void Chase()
	{
		//MosquitoAni.SetBool ("IsParolling", false);
		//MosquitoAni.SetBool("FightingWalk", true);
		Vector3 dir = Target.transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		if (targetDistance > EnemyDist && Vector3.Distance(Target.transform.position, gameObject.transform.position) < SafeDist)
		{
			RB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
		}
		attackTimer += Time.deltaTime;
		if (targetDistance <= EnemyDist && attackTimer >= attackDelay)
		{
			ActualState = State.Attack;
			attackTimer = 0;
		}
		if (targetDistance > SafeDist + 1)
		{
			//MosquitoAni.SetBool("FightingWalk", false);
			ActualState = State.Patrol;
		}
		if (hitted)
			ActualState = State.TakeDamage;
	}
	public virtual void Flee()
	{
	}
	public virtual void Attack()
	{
	}
	#endregion

	#region IKillable
	public virtual void TakeDamage ()
	{
	}
	public virtual void Die ()
	{
	}
	#endregion

	#region IGoToScreen
	public void UpToScreen ()
	{
	}
	public void DownToGround ()
	{
	}
	#endregion

	#region IScreenEntity
	public virtual void OnScreenChase ()
	{
	}
	public virtual void OnScreenAttack ()
	{
	}
	public virtual void OnScreenIdle ()
	{
	}
	public virtual void OnScreenDamage ()
	{
	}
	#endregion

	#region IAnimated
	public virtual void Animate (Animator anim, string variable, float value)
	{
	}
	#endregion


}