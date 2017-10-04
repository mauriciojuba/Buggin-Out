using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour,IGroundEnemy,IKillable,IGoToScreen,IScreenEntity {

	[HideInInspector]
	public GOToScreen Screen;
	[HideInInspector]
	public Rigidbody RB;
	[HideInInspector]
	public Animator _anim;
	[HideInInspector]
	public enum State 	{Idle, Patrol, Chase, Flee, Attack, TakeDamage,
						Dead, GoingToScreen, OnScreenIdle, OnScreenChase, 
						OnScreenAttack, GoingToWorld, OnScreenDamage, OnScreenFall};
	[HideInInspector]
	public GameObject player1, player2;
	[HideInInspector]
	public List<GameObject> players;
	[HideInInspector]
	public float[] playersDist = new float[2];

	[Header("Estado atual")]
	public State ActualState = State.Chase;

	[Header("Foco do inimigo")]
	public GameObject Target;

	[Header("Configurações de Combate")]
	public float Life = 300;
	public float lifeMax;
	public float attackStr;
	public float attackDelay;
	public bool hitted;
	public bool grabbed;

	protected float attackTimer;

	[Header("Configurações de Movimento")]
	public float speed;
	public float rotationSpeed;
	public float chaseSpeed;
	public float fleeSpeed;
	public bool onScreen;
	public float screenSpeed;
	public Vector3 onScreenScale;

	[Header("WayPoint")]
	public Transform[] waypoints;
	public float TimeToNextPoint = 5f;
	public float TimeToChangeTarget = 5f;

	protected float TimeTo;
	protected int currentWaypoint;

	[Header("Configurações de Alcance")]
	public float Visao;
	public float SafeDist;
	public float EnemyDist;

	protected float targetDistance;

	public virtual void Start()
	{
		Screen = GameObject.Find ("GoToScreen").GetComponent<GOToScreen>();
		RB = GetComponent<Rigidbody> ();
		_anim = GetComponent<Animator> ();
		lifeMax = Life;
		CheckAllTargets ();
		TimeTo = TimeToNextPoint;
		CalculaDistancia ();
		StartCoroutine(CalcDist());
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
			case State.OnScreenIdle: OnScreenIdle (); break;
			case State.OnScreenChase: OnScreenChase (); break;
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
	IEnumerator CalcDist()
	{
		yield return new WaitForSeconds(TimeToChangeTarget);
		CalculaDistancia();
		StartCoroutine(CalcDist());
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
		_anim.SetBool("IsIdle", true);

		if (Vector3.Distance(Target.transform.position, gameObject.transform.position) < Visao)
		{
			_anim.SetBool("IsIdle", false);
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

			_anim.SetBool("IsIdle", false);
			ActualState = State.Patrol;
		}
		if (hitted)
			ActualState = State.TakeDamage;
	}
	public virtual void Patrol()
	{
		_anim.SetBool("IsParolling", true);
		Vector3 dir = waypoints[currentWaypoint].position - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		if (dir.sqrMagnitude <= 1)
		{
			_anim.SetBool("IsParolling", false);
			ActualState = State.Idle;
		}
		else
			RB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);

		if (targetDistance < Visao)
		{
			_anim.SetBool("IsParolling", false);
			ActualState = State.Chase;
		}
		if (hitted)
			ActualState = State.TakeDamage;
	}
	public virtual void Chase()
	{
		_anim.SetBool ("IsParolling", false);
		_anim.SetBool("FightingWalk", true);
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
			_anim.SetBool("FightingWalk", false);
			ActualState = State.Patrol;
		}
		if (hitted)
			ActualState = State.TakeDamage;
	}
	public virtual void Flee()
	{
		
		_anim.SetBool("FightingWalk", false);
		TimeToNextPoint = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - Target.transform.position), rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * speed * Time.deltaTime;

		if (targetDistance > SafeDist + 2 && Life <= lifeMax * 0.2f)
		{
			_anim.SetTrigger("GoToScreen");
			_anim.SetBool("UsingWings", false);
			_anim.SetBool("GoingToScreen", true);

			ActualState = State.GoingToScreen;
		}

		else if (targetDistance > SafeDist + 2)
			ActualState = State.Idle;
	}
	public virtual void Attack()
	{
		_anim.SetTrigger("ATK1");

		ActualState = State.Chase;

		if (hitted) ActualState = State.TakeDamage;
	}
	#endregion

	#region IKillable
	public virtual void TakeDamage ()
	{
		if (!onScreen)
		{
			_anim.SetBool("FightingWalk", false);
			if (Life > 0 && Life <= lifeMax * 0.2f && !grabbed)
			{
				ActualState = State.Flee;
			}
			else if (Life <= 0) ActualState = State.Dead;
		}
		else
		{
			_anim.SetTrigger("TakeDamageScreen");
			_anim.SetBool ("LifeDrain", false);

			//particulas
			/*if (part != null) {
				ParticleSystem particleemitter = part.GetComponent<ParticleSystem> ();
				if (particleemitter != null) {
					ParticleSystem.EmissionModule emit = particleemitter.emission;
					emit.enabled = false;
				}
				Destroy(part, 5f);
			}*/

			if (Life > 0) {
				ActualState = State.OnScreenFall;
			} else {
				ActualState = State.Dead;
			}
		}
		hitted = false;
		if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Take Damage") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && Life > 0)
		{
			ActualState = State.Chase;
		}
	}
	public virtual void Die ()
	{
	}
	#endregion

	#region IGoToScreen
	public virtual void UpToScreen ()
	{
		if (!onScreen)
		{
			RB.useGravity = false;
			screenSpeed = 0.5f;
			onScreen = true;
		}
		if (Screen.GoToScreen (gameObject)) {
			ActualState = State.OnScreenIdle;
		}
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
	public virtual void OnScreenFall ()
	{
	}
	#endregion


}