using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Boss : MonoBehaviour
{

    public enum State { Idle, Chama_Bomba, LevaDano, Atordoado, Cansado, ATK_Dash_Ground, ATK_Dash_Screen, ATK_Area, Morte , UpToScreen, DowToGround }
    public State ActualState = State.Idle;

    [HideInInspector]
    public GOToScreen Screen;
    [HideInInspector]
    public GameObject player1, player2;
    [HideInInspector]
    public List<GameObject> players;
    [HideInInspector]
    public float[] playersDist = new float[2];
    [HideInInspector]
    public float playerStr;
    [HideInInspector]
    public Animator _anim;
    [HideInInspector]
    public Rigidbody RB;

    [Header("Foco do inimigo")]
    public GameObject Target;
    public Transform Target_On_Screen;
    public float Target_dist;

    [Header("Vida")]
    public float Life = 3000f;
    public bool CanHit;
    public bool hitted;

    [Header("Configurações de Dano")]
    public float DanoDash = 50f;
    public GameObject HitBox_Dash;
    public float DanoArea = 20f;
    public GameObject HitBox_Area;

    [Header("Configurações de Ataques")]
    public float TimeToAtkArea =2;
    public float TimeToAtkDash;
    public float TimeToAtkBomb;
    public float TimeToSumon;
    public GameObject Bomb, BombFail;
    public GameObject BombAim;

    [Header("WayPoint")]
    public Transform[] waypoints;
    public float TimeToNextPoint = 5f;
    public float TimeToChangeTarget = 5f;
    protected float TimeTo;
    protected int currentWaypoint;

    [Header("Configurações de Movimento")]
    public float DashSpeed;
    public float MoveSpeed;
    public float screenSpeed;
    public float rotationSpeed;

    public float StunTime = 10f;

    [Header("Screen")]
    public bool onScreen;

    [Header("Configurações de Alcance")]
    public float Visao;
    public float SafeDist;
    public float EnemyDist;
    protected float targetDistance;
    public float randomOffsetOnScreen;

    public ParticleSystem Hamer;
    public float TimerToSpawnBomb;
    public float Timer;
    public bool StartBombs;
    Vector3 Area1;
    Vector3 Area2;
    // Use this for initialization
    void Start()
    {

        Screen = GameObject.Find("GoToScreen").GetComponent<GOToScreen>();
        RB = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        CheckAllTargets();
        CalculaDistancia();
        StartCoroutine(CalcDist());
    }

    private void FixedUpdate()
    {
        if (StartBombs)
        {
            SpawnBombs();
        }

        switch (ActualState)
        {
            case State.Idle: Idle(); break;
            case State.Chama_Bomba: Call_Bomb(); break;
            case State.LevaDano: TakeDamage(); break;
            case State.Cansado: Cansado(); break;
            case State.Atordoado: Stuned(); break;
            case State.ATK_Dash_Ground: AtkD(); break;
            case State.ATK_Dash_Screen: AtkD_S(); break;
            case State.ATK_Area: AtkA(); break;
            case State.Morte: Die(); break;
            case State.UpToScreen: GoingToScreen(); break;
            case State.DowToGround: GoingToWorld(); break;
        }
    }



    private void Idle()
    {
        //Fica um tempo em idle Antes da mudança de estado
        //Se os PLayers Chegarem perto ele da um ataque em area

        HitBoxOff("Dash");

        if (_anim != null)
        _anim.SetBool("Idle", true);

        CanHit = false;

        Vector3 dir = Target.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        float Tdist = Vector3.Distance(Target.transform.position, gameObject.transform.position);



        if (Tdist < Visao)
        {
            TimeToAtkArea -= Time.deltaTime;
            if(TimeToAtkArea < 0)
            ActualState = State.ATK_Area;
        }

        
        

        TimeToAtkDash -= Time.deltaTime;

        if (TimeToAtkDash <= 2)
        {
            if (_anim != null)
            {
                _anim.SetBool("Idle", false);
                _anim.SetBool("Dash", true);
            }
        }

        else
        {
            TimeToAtkBomb -= Time.deltaTime;
            if (TimeToAtkBomb <= 0)
            {
                ActualState = State.Chama_Bomba;
            }
        }
        
        if (TimeToAtkDash <= 0)
        {
            ActualState = State.ATK_Dash_Ground;
        }

        if (hitted)
            ActualState = State.LevaDano;


    }



    public virtual void TakeDamage()
    {

        _anim.SetTrigger("Damage");


        if (Life <= 0f)
            ActualState = State.Morte;

        if (hitted)
        {
            if (playerStr == 0)
                playerStr = 50;

            if (_anim != null)
                _anim.SetTrigger("Take Damage");

            Life -= playerStr;
            hitted = false;
        }

        if (Life > 0f && StunTime > 0f)
            ActualState = State.Cansado;
        else if(Life > 0f)
            ActualState = State.Idle;
    }

    public virtual void Die()
    {
        _anim.SetBool("Dead", true);
        _anim.SetTrigger("Death");
        
    }


    private void Cansado() //Bos fica Parado um Tempo depois do Dash Na tela
    {
        HitBoxOff("Dash");
        transform.rotation = Quaternion.Euler(0,-180,0);

        if (hitted)
            ActualState = State.LevaDano;

        if (_anim != null)
        {
            _anim.SetBool("Fall", false);
            _anim.SetBool("Injuried", true);
        }

        CanHit = true; 

        StunTime -= Time.deltaTime;
        if (StunTime > 0)
        {
            TimeToSumon -= Time.deltaTime;
            if(TimeToSumon < 0)
            {
                _anim.SetTrigger("Sumon");
                TimeToSumon = 4;
            }
        }
        else
        {
            StunTime = 0;
            print(StunTime);
            ActualState = State.Idle;
        }

       

    }

    private void Stuned() // Depos que o Boss Leva Uma quantidade 
    {
        CanHit = true;

        if (hitted)
            ActualState = State.LevaDano;

    }

    private void Call_Bomb()
    {
        if (_anim != null)
            _anim.SetTrigger("Bomb");

        Area1 = new Vector3(Target.transform.position.x + 7, Target.transform.position.y, Target.transform.position.z + 7);
        Area2 = new Vector3(Target.transform.position.x - 7, Target.transform.position.y, Target.transform.position.z - 7);
        TimerToSpawnBomb = 0;
        TimeToAtkBomb = 30;
        StartBombs = true;
        ActualState = State.Idle;

    }

    private void SpawnBombs()
    {
        Timer += Time.deltaTime;
        TimerToSpawnBomb += Time.deltaTime;
      
        if (TimerToSpawnBomb >= 1)
        {
            float random;
            random = UnityEngine.Random.Range(1, 10);
            random = Mathf.RoundToInt(random);
            float RandomX;
            RandomX = UnityEngine.Random.Range(Area2.x, Area1.x);
            float RandomZ;
            RandomZ = UnityEngine.Random.Range(Area2.z, Area1.z);
            if (random <= 1)
                Instantiate(BombFail, new Vector3(RandomX, Area1.y, RandomZ) + Vector3.up * 20, Target.transform.rotation);
            else
                Instantiate(Bomb, new Vector3(RandomX, Area1.y, RandomZ) + Vector3.up * 20, Target.transform.rotation);

            Instantiate(BombAim, new Vector3(RandomX, Area1.y + 1, RandomZ), Target.transform.rotation);
            TimerToSpawnBomb = 0;
        }

        if (Timer > 20)
        {
            StartBombs = false;
        }
    }

    private void AtkA()
    {
        CanHit = false;

        _anim.SetTrigger("ATK");

        TimeToAtkArea = 2;

        ActualState = State.Idle;

    }

    private void AtkD()
    {

        HitBoxOn("Dash");

        CanHit = false;

        //Dash No Chao

        if (_anim != null)
            _anim.SetBool("IsParolling", true);
        Vector3 dir = waypoints[currentWaypoint].position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (dir.sqrMagnitude <= 1)
        {
            if (_anim != null)
                _anim.SetBool("IsParolling", false);

            currentWaypoint++;
            CalculaDistancia();
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
                _anim.SetTrigger("DashScreen");
                ActualState = State.UpToScreen;
            }
        }
        else
        {
            RB.MovePosition(transform.position + transform.forward * Time.deltaTime * DashSpeed);
        }
    }

    private void AtkD_S()
    {
        CanHit = false;

        

        //Dash Na tela
        RB.isKinematic = true;
        Target_dist = Vector3.Distance(transform.position, Target_On_Screen.position);


        if (Target_dist > 0.3f)
        {
            Vector3 mov;
            mov = new Vector3(Target_On_Screen.position.x - transform.position.x, Target_On_Screen.position.y - transform.position.y, 0);
            mov = Camera.main.transform.TransformVector(mov);
            transform.Translate(mov * transform.localScale.magnitude * Time.deltaTime * screenSpeed, Space.World);
            transform.LookAt(Camera.main.transform, transform.up + mov);
        }
        else
        {
            StunTime = 10f;
            TimeToAtkDash = 15f;
            ActualState = State.DowToGround;
        }


    }

   
    private void GoingToScreen()
    {



        Screen.GoToScreen(gameObject, -50f, 0f, 0f);
        RB.useGravity = false;
        onScreen = true;

        if (Screen.GoToScreen(gameObject, randomOffsetOnScreen))
        {
            ActualState = State.ATK_Dash_Screen;
        }

    }

    private void GoingToWorld()
    {

        if (_anim != null)
        {
            _anim.SetBool("Idle", false);
            _anim.SetBool("Dash", false);
            _anim.SetBool("Fall", true);
        }

        if (Screen.GoOffScreen(waypoints[0], gameObject))
        {
            RB.isKinematic = false;
            RB.useGravity = true;
            onScreen = false;
            ActualState = State.Cansado;
        }
    }


    //Outras Funçoes  /////////////////////////////////////////

    public void Particula(string Part)
    {
        if(Part == "Hamer")
        {
            Hamer.Play();
        }

    }


    public void PlaySound(string Event)
    {
        FMODUnity.RuntimeManager.PlayOneShot(Event, transform.position);
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

    public void HitBoxOn(String Ataque)
    {
        if (Ataque == "Dash")
            HitBox_Dash.GetComponent<Collider>().enabled = true;

        if (Ataque == "Area")
            HitBox_Area.GetComponent<Collider>().enabled = true;
    }

    public void HitBoxOff(String Ataque)
    {
        if (Ataque == "Dash")
            HitBox_Dash.GetComponent<Collider>().enabled = false;

        if (Ataque == "Area")
            HitBox_Area.GetComponent<Collider>().enabled = false;
    }

    public void CheckAllTargets()
    {
        if (GameObject.FindGameObjectWithTag("Player1_3D") != null)
        {
            player1 = GameObject.FindGameObjectWithTag("Player1_3D");
            players.Add(player1);
            if (GameObject.FindGameObjectWithTag("Player2_3D") != null)
            {
                player2 = GameObject.FindGameObjectWithTag("Player2_3D");
                players.Add(player2);
            }
        }
        else if (GameObject.FindGameObjectWithTag("Player2_3D") != null)
        {
            player2 = GameObject.FindGameObjectWithTag("Player2_3D");
            players.Add(player2);
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

    //public void OnTriggerEnter(Collider hit)
    //{
    //    if (hit.CompareTag("playerHitCollider"))
    //    {
    //        playerStr = hit.GetComponent<FightCollider>().Damage;
    //        if (!hitted && CanHit) hitted = true;
    //    }
    //}

}
