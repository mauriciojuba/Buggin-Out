using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour, IGroundEnemy, IKillable, IGoToScreen, IScreenEntity {

    public NavMeshAgent _navMeshAgent;


    [HideInInspector]
    public GOToScreen Screen;
    [HideInInspector]
    public Rigidbody RB;
    public Animator _anim;
    [HideInInspector]
    public enum State { Idle, Patrol, Chase, Flee, Attack, TakeDamage,
        Dead, GoingToScreen, OnScreenIdle, OnScreenChase,
        OnScreenAttack, GoingToWorld, OnScreenDamage, OnScreenFall, Falling };
    [HideInInspector]
    public GameObject player1, player2;
    [HideInInspector]
    public List<GameObject> players;
    [HideInInspector]
    public float[] playersDist = new float[2];
    [HideInInspector]
    public Transform worldPos;
    [HideInInspector]
    public float playerStr;
    [HideInInspector]
    public float randomOffsetOnScreen;

    [SerializeField]
    private float multiplyBy;


    public int VoiceChance;

    [FMODUnity.EventRef]
    public string Evento_Liz;
    [FMODUnity.EventRef]
    public string Evento_Horn;

    [Header("Estado atual")]
    public State ActualState = State.Chase;

    [Header("Foco do inimigo")]
    public GameObject Target;

    [Header("Configurações de Combate")]
    public float Life = 300;
    public float lifeMax;
    public float attackStr;
    public float attackDelay;
    public float onScreenAtkStr;
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


        #region Teste NavMesh Start
        //Teste NavMesh
        
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent is not attached");
        }
        else
        {
            _navMeshAgent.SetDestination(waypoints[0].position);
        }
        

        #endregion
        Screen = GameObject.Find("GoToScreen").GetComponent<GOToScreen>();
        RB = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        lifeMax = Life;
        CheckAllTargets();
        TimeTo = TimeToNextPoint;
        CalculaDistancia();
        StartCoroutine(CalcDist());
    }

    public void FixedUpdate()
    {
        if (hitted) ActualState = State.TakeDamage;
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
            case State.GoingToWorld: DownToGround(); break;
            case State.OnScreenIdle: OnScreenIdle(); break;
            case State.OnScreenChase: OnScreenChase(); break;
            case State.OnScreenAttack: OnScreenAttack(); break;
            case State.Falling: OnFalling(); break;
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
        if (GameObject.FindGameObjectWithTag("Player1_3D") != null) {
            player1 = GameObject.FindGameObjectWithTag("Player1_3D");
            players.Add(player1);
            if (GameObject.FindGameObjectWithTag("Player2_3D") != null) {
                player2 = GameObject.FindGameObjectWithTag("Player2_3D");
                players.Add(player2);
            }
        }
        else if (GameObject.FindGameObjectWithTag("Player2_3D") != null) {
            player2 = GameObject.FindGameObjectWithTag("Player2_3D");
            players.Add(player2);
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
        _navMeshAgent.enabled = true;
       
        if (_anim != null)
            _anim.SetBool("IsIdle", true);

        if (Vector3.Distance(Target.transform.position, gameObject.transform.position) < Visao)
        {

            if (_anim != null)
                _anim.SetBool("IsIdle", false);
            ActualState = State.Chase;
        }
        TimeToNextPoint -= Time.deltaTime;
        if (TimeToNextPoint < 0)
        {
            currentWaypoint++;
            CalculaDistancia();
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
            TimeToNextPoint = TimeTo;

            if (_anim != null)
                _anim.SetBool("IsIdle", false);
            ActualState = State.Patrol;
        }
        if (hitted)
            ActualState = State.TakeDamage;
    }
    public virtual void Patrol()
    {
        _navMeshAgent.stoppingDistance = 0;
        if (worldPos != null)
        {
            _navMeshAgent.Warp(worldPos.position);
            Destroy(worldPos.gameObject);
        }
        if (_anim != null)
            _anim.SetBool("IsParolling", true);
        Vector3 dir = waypoints[currentWaypoint].position - transform.position;
        /*transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);*/

        if (dir.sqrMagnitude <= 1)
        {
            if (_anim != null)
                _anim.SetBool("IsParolling", false);
            ActualState = State.Idle;
        }
       // else
       //     RB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);

        #region Teste NavMesh

        // TESTE NavMesh 
        
        if (waypoints != null)
        {
            if (Vector3.Distance(gameObject.transform.position, waypoints[currentWaypoint].position) < 1)
            {
                TimeToNextPoint -= Time.deltaTime;
                if (TimeToNextPoint < 0)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.Length)
                        currentWaypoint = 0;
                    TimeToNextPoint = TimeToChangeTarget;
                    
                    
                }

            }
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        


        #endregion

        if (targetDistance < Visao)
        {
            if (_anim != null)
                _anim.SetBool("IsParolling", false);
            ActualState = State.Chase;
        }
        if (hitted)
            ActualState = State.TakeDamage;
    }


    #region Perseguir

    public virtual void Chase()
    {

        _navMeshAgent.stoppingDistance = 2;

        if (_anim != null)
        {
            _anim.SetBool("IsParolling", false);
            _anim.SetBool("FightingWalk", true);
        }
        Vector3 dir = Target.transform.position;

        _navMeshAgent.SetDestination(Target.transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        //if (targetDistance > EnemyDist && Vector3.Distance(Target.transform.position, gameObject.transform.position) < SafeDist)
        //{
        //    RB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        //}
        attackTimer += Time.deltaTime;
        if (targetDistance <= EnemyDist) {
            _anim.SetBool("FightingWalk", false);
            _anim.SetBool("IsIdle", true);
            if (attackTimer >= attackDelay)
            {
                ActualState = State.Attack;
                attackTimer = 0;
            }
        }
        if (targetDistance > SafeDist + 1)
        {
            if (_anim != null)
                _anim.SetBool("FightingWalk", false);
            ActualState = State.Patrol;
        }
        if (hitted)
            ActualState = State.TakeDamage;
    }

    #endregion

    #region Fugir


    public virtual void Flee()
    {
        if (_anim != null)
            _anim.SetBool("FightingWalk", false);
        TimeToNextPoint = 2;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - Target.transform.position), rotationSpeed * Time.deltaTime);
        //transform.position += transform.forward * speed * Time.deltaTime;


        //temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - Target.transform.position);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + transform.forward * multiplyBy;
        //Debug.Log("runTo = " + runTo);

        //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

        NavMeshHit hit;    // stores the output in a variable called hit

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
        //Debug.Log("hit = " + hit + " hit.position = " + hit.position);


        // And get it to head towards the found NavMesh position
        _navMeshAgent.SetDestination(hit.position);

        if (targetDistance > SafeDist + 2 && Life <= lifeMax * 0.2f)
        {
            if (_anim != null)
            {
                _anim.SetTrigger("GoToScreen");
                _anim.SetBool("UsingWings", false);
                _anim.SetBool("GoingToScreen", true);
            }
            randomOffsetOnScreen = Random.Range(-0.1f, 0.1f);
            _navMeshAgent.enabled = false;
            ActualState = State.GoingToScreen;
        }

        else if (targetDistance > SafeDist + 2)
            ActualState = State.Idle;
    }
    #endregion

    #region Ataque
    public virtual void Attack()
    {
        
            _anim.SetTrigger("ATK1");

        ActualState = State.Chase;

        if (hitted) ActualState = State.TakeDamage;
    }
    #endregion


    #region IKillable
    public virtual void TakeDamage()
    {

        if (Life <= 0f)
        {
            float random = Random.Range(0, 100);
            if (random <= VoiceChance)
            {
                if (Target.name == "Horn")
                    FMODUnity.RuntimeManager.PlayOneShot(Evento_Horn, transform.position);
                if (Target.name == "Liz")
                    FMODUnity.RuntimeManager.PlayOneShot(Evento_Liz, transform.position);
            }
            ActualState = State.Dead;
        }

        if (hitted)
        {
            if (playerStr == 0)
                playerStr = 50;

            _anim.SetTrigger("Take Damage");

            Life -= playerStr;

            
               
            hitted = false;

        }

        if (Life > 0f)
            ActualState = State.Chase;

    }

    public virtual void Die()
    {
        
        _anim.SetTrigger("Death");

    }
    #endregion

    #region IGoToScreen
    public virtual void UpToScreen()
    {
        if (!onScreen)
        {
            worldPos = new GameObject("World Pos Mosquito").transform;
            worldPos.position = transform.position;
            RB.useGravity = false;
            screenSpeed = 0.5f;
            onScreen = true;
        }
        if (Screen.GoToScreen(gameObject, randomOffsetOnScreen)) {
            ActualState = State.OnScreenIdle;
        }
    }
    public virtual void DownToGround()
    {
        

        if (Screen.GoOffScreen(worldPos, gameObject)) {
            TimeToNextPoint = 5;
            RB.isKinematic = false;
            RB.useGravity = true;
            onScreen = false;
            _navMeshAgent.enabled = true;
            // Destroy(worldPos.gameObject);
            ActualState = State.Idle;
        }
    }
    #endregion

    #region IScreenEntity
    public virtual void OnScreenChase()
    {
    }
    public virtual void OnScreenAttack()
    {
    }
    public virtual void OnScreenIdle()
    {
    }
    public virtual void OnScreenDamage()
    {
    }
    public virtual void OnScreenFall()
    {
    }

    public virtual void OnFalling()
    {

    }
    #endregion


    //public void OnTriggerExit(Collider hit){
    //	if (hit.CompareTag ("playerHitCollider")) {
    //		playerStr = hit.GetComponent<FightCollider> ().Damage;
    //		if(!hitted) hitted = true;
    //	}
    //}
#endregion
}
