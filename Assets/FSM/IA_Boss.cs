using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Boss : MonoBehaviour
{

    public enum State { Idle, Chama_Inimigo, LevaDano, Atordoado, Cansado, ATK_Dash_Ground, ATK_Dash_Screen, ATK_Area, Morte }
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

    [Header("Vida")]
    public float Life = 3000f;
    public bool hitted;

    [Header("Configurações de Dano")]
    public float DanoDash = 50f;
    public GameObject HitBox_Dash;
    public float DanoArea = 20f;
    public GameObject HitBox_Area;

    [Header("Configurações de Ataques")]
    public float TimeToAtk;

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

    [Header("Screen")]
    public bool onScreen;

    [Header("Configurações de Alcance")]
    public float Visao;
    public float SafeDist;
    public float EnemyDist;
    protected float targetDistance;


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
        switch (ActualState)
        {
            case State.Idle: Idle(); break;
            case State.Chama_Inimigo: Call_Enemy(); break;
            case State.LevaDano: TakeDamage(); break;
            case State.Cansado: Cansado(); break;
            case State.Atordoado: Stuned(); break;
            case State.ATK_Dash_Ground: AtkD(); break;
            case State.ATK_Dash_Screen: AtkD_S(); break;
            case State.ATK_Area: AtkA(); break;
            case State.Morte: Die(); break;
        }
    }



    private void Idle()
    {
        //Fica um tempo em idle Antes da mudança de estado
        //Se os PLayers Chegarem perto ele da um ataque em area

        Vector3 dir = Target.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        float Tdist = Vector3.Distance(Target.transform.position, gameObject.transform.position);

        if (Tdist < Visao)
        {
            ActualState = State.ATK_Area;
        }

        TimeToAtk -= Time.deltaTime;
        if(TimeToAtk <= 0)
        {
            ActualState = State.ATK_Dash_Ground;
        }
        

        

    }



    public virtual void TakeDamage()
    {
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

        if (Life > 0f)
            ActualState = State.Idle;
    }

    public virtual void Die()
    {

        _anim.SetTrigger("Death");

    }


    private void Cansado() //Bos fica Parado um Tempo depois do Dash Na tela
    {
        float StunTime = 5; //Tempo que o boss fica Parado

        StunTime -= Time.deltaTime;
        if (StunTime > 0)
        {
            if (_anim != null)
            {

            }
        }
    }

    private void Stuned() // Depos que o Boss Leva Uma quantidade 
    {
        float StunTime = 5; //Tempo que o boss fica atordoado

        StunTime -= Time.deltaTime;
        if (StunTime > 0)
        {
            if (_anim != null)
            {

            }
        }
    }

    private void Call_Enemy()
    {
        //Chama inimigo
    }

    private void AtkA()
    {
        HitBoxOn("Area");
        HitBoxOff("Area");

        ActualState = State.Idle;

    }

    private void AtkD()
    {
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
                ActualState = State.ATK_Dash_Screen;
        }
        else
        {
            RB.MovePosition(transform.position + transform.forward * Time.deltaTime * DashSpeed);
        }
    }

    private void AtkD_S()
    {
        //Dash Na tela
        Screen.GoToScreen(gameObject, -10f, 0, 0f);
        RB.useGravity = false;
        onScreen = true;

        if (onScreen)
        {

            Vector3 mov;
            mov = new Vector3(Target_On_Screen.position.x - transform.position.x, Target_On_Screen.position.y - transform.position.y, 0);
            mov = Camera.main.transform.TransformVector(mov);
            transform.Translate(mov * transform.localScale.magnitude * Time.deltaTime * screenSpeed, Space.World);
            transform.LookAt(Camera.main.transform, transform.up + mov);
        }
    }

   


    //Outras Funçoes 

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

    public void Chase()
    {
        if (_anim != null)
        {
            _anim.SetBool("IsParolling", false);
            _anim.SetBool("FightingWalk", true);
        }
        Vector3 dir = Target.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (targetDistance > EnemyDist && Vector3.Distance(Target.transform.position, gameObject.transform.position) < SafeDist)
        {
            RB.MovePosition(transform.position + transform.forward * Time.deltaTime * DashSpeed);
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

}
