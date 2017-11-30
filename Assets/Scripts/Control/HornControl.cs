﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornControl : MonoBehaviour {
    public Vector3 mov;
	Vector3 movAntPos;
    public GameObject AntPosPrefab;
	Rigidbody antposRB;
    [SerializeField] Rigidbody rdb;
	AnimationControl AnimCTRL;
    public GameObject cameragame;
    public GameObject ReferenciaDir;
    public Animator anim;
	public bool natela = false;
    public Vector2 limiteHorizontal,limiteVertical;
	public bool Going = false;
	public bool CanMove = true;
    public Transform telapos;
	public Transform AntPos;
    int i=0;

    public bool UseSpecial;

	float randomOffSetOnScreen;

	[SerializeField] GOToScreen Screen;
	[SerializeField] CameraControl DollyCam;
	[SerializeField] float CamSpeed;
	[SerializeField] float Speed = 5;
    [SerializeField] UseEspecial SpecialRef;
    public float NumberOfSpecial;

    public int PlayerNumber;

	// Use this for initialization
	void Start () {
        PlayerNumber = GetComponent<PlayerNumb>().PlayerNumber;
        DollyCam = GameObject.FindWithTag("DollyCam").GetComponent<CameraControl>();
        ReferenciaDir = GameObject.FindWithTag("Reference");
        telapos = GameObject.FindWithTag("PosTela").transform;
        Screen = GameObject.Find("GoToScreen").GetComponent<GOToScreen>();
        AnimCTRL = GetComponent<AnimationControl> ();
        rdb = gameObject.GetComponent<Rigidbody>();
        cameragame = Camera.main.gameObject;
        ReferenciaDir.transform.up = Vector3.up;

    }
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Escolhendo ponto de retorno? "+escolhendoPontoDeRetorno);
        if (CanMove)
        {
            if (!natela)
            {
                mov = new Vector3(Input.GetAxis("Horizontal P1"), 0, Input.GetAxis("Vertical P1"));
                if (ReferenciaDir != null)
                    mov = ReferenciaDir.transform.TransformVector(mov);
                Vector3 Direction = new Vector3(rdb.velocity.x, 0, rdb.velocity.z);
                Vector3 Directionabs = new Vector3(mov.x, 0, mov.z);

                if (mov.magnitude > 0)
                {
                    //anim.SetBool("Layer1Active", true);
                    anim.SetLayerWeight(1, 1);
                    anim.SetLayerWeight(2, 0);
                }
                else if (mov.magnitude <= 0)
                {
                    // anim.SetBool("Layer1Active", false);
                    anim.SetLayerWeight(2, 1);
                    anim.SetLayerWeight(1, 0);
                }

                if (Directionabs.magnitude > 0.1f)
                {

                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Directionabs), Time.deltaTime * 5);
                }
            }
            else
            {

                mov = new Vector3(Input.GetAxis("Horizontal P1"), Input.GetAxis("Vertical P1"), 0);

                mov = cameragame.transform.TransformVector(mov);
                mov = CheckPositionOnScreen(mov, transform);
                if (!escolhendoPontoDeRetorno)
                {
                    transform.Translate(mov * transform.localScale.magnitude * Time.deltaTime * CamSpeed, Space.World);
                    transform.LookAt(cameragame.transform, transform.up + mov);
                    if (mov.magnitude > 0)
                    {
                        // anim.SetBool("Layer1Active", true);
                        anim.SetLayerWeight(1, 1);
                        anim.SetLayerWeight(2, 0);
                    }
                    else if (mov.magnitude <= 0)
                    {
                        // anim.SetBool("Layer1Active", false);
                        anim.SetLayerWeight(2, 1);
                        anim.SetLayerWeight(1, 0);
                    }
                }
            }
            //		if (Input.GetButtonDown ("X P1") || Input.GetKeyDown (KeyCode.LeftControl)) {
            //			
            //			anim.SetTrigger ("Attack");
            //		}
            if (!UseSpecial)
            {
                if (!natela)
                {
                    if (Input.GetButtonDown("RB P1") && !Going ||
                        Input.GetKeyDown(KeyCode.LeftShift) && !Going)
                    {
                        AntPos = GameObject.Instantiate(AntPosPrefab, transform.position, transform.rotation).transform;
                        antposRB = AntPos.GetComponent<Rigidbody>();
                        AntPos.position = transform.position;
                        AntPos.rotation = transform.rotation;
                        Going = true;
                        randomOffSetOnScreen = Random.Range(-0.05f, 0.05f);
                        anim.SetBool("tocam", !natela);
                        rdb.isKinematic = true;
                        DollyCam.ChecarNaTela();
                    }
                }
                else
                {
                    if (Input.GetButton("RB P1") && !Going ||
                        Input.GetKey(KeyCode.LeftShift) && !Going)
                    {
                        MudaPontoDeRetorno();
                    }
                    if (Input.GetButtonUp("RB P1") && !Going ||
                        Input.GetKeyUp(KeyCode.LeftShift) && !Going)
                    {
                        Going = true;
                        anim.SetBool("tocam", !natela);
                        rdb.isKinematic = true;
                        escolhendoPontoDeRetorno = false;
                    }
                }

                if (Going)
                {
                    if (!natela)
                    {
                        //Screen.GoToScreen(gameObject);
                        if (Screen.GoToScreen(gameObject, randomOffSetOnScreen))
                        {
                            Going = false;
                            natela = true;
                        }
                    }
                    else
                    {
                        if (Screen.GoOffScreen(AntPos, gameObject))
                        {
                            Going = false;
                            natela = false;
                            Destroy(AntPos.gameObject);
                        }
                    }
                }
            }
            else
            {
                rdb.velocity = Vector3.zero;
            }
        }


        #region Especial (Usar e Rotacionar)

        //Estou tentando zerar a velocity do rdb mas ela continua deslizando enquanto usa o especial.. ajudem ae se caso souberem o motivo
        if (!natela)
        {
            if (Input.GetButtonDown("UseSpecial P" + PlayerNumber) && !UseSpecial && NumberOfSpecial >= 100)
            {

                if (SpecialRef.Horn)
                {
                    SpecialRef.StopParticle();
                }
                mov = Vector3.zero;
                UseSpecial = true;
                AnimCTRL.SetSpecial();
                NumberOfSpecial = 0;
                
            }

            if (UseSpecial)
            {
                if (SpecialRef.UsingSpecial())
                {
                    Vector3 movSpecial = new Vector3(Input.GetAxis("Horizontal P1"), 0, Input.GetAxis("Vertical P1"));
                    Vector3 DirectionSpecial = new Vector3(movSpecial.x, 0, movSpecial.z);
                    if (DirectionSpecial.magnitude > 0.1f)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(DirectionSpecial), Time.deltaTime * 5);
                    }
                    CanMove = false;
                    AnimCTRL.SetUsingSpecial(true);
                    if (SpecialRef.Horn)
                    {
                        SpecialRef.SpecialHorn();
                    }
                }
                else
                {
                    AnimCTRL.SetUsingSpecial(false);
                    CanMove = true;
                    UseSpecial = false;
                }
            }
        }
        #endregion
    }
	#region Escolhe retorno World
	bool escolhendoPontoDeRetorno;
	void MudaPontoDeRetorno(){
		if (AntPos != null) {
			escolhendoPontoDeRetorno = true;
            AntPos.GetComponent<Rigidbody>().isKinematic = false;
            AntPos.GetComponent<Rigidbody>().useGravity = true;
            AntPos.GetComponent<Rigidbody>().velocity = Vector3.zero;
            movAntPos = new Vector3 (Input.GetAxis ("Horizontal P1"), 0, Input.GetAxis ("Vertical P1"));
			movAntPos = CheckPositionOnScreen (movAntPos,AntPos);
			//Debug.Log (AntPos+","+ movAntPos);
			AntPos.Translate (movAntPos * Time.deltaTime * CamSpeed*5, Space.World);
		}
	}
	#endregion
	Vector3 CheckPositionOnScreen(Vector3 movFactor,Transform trans){

    // talvez seja necessário tornar público os limites da tela para diferentes objetos, dependo do tamanho.
    // horn limiteHorizontal = 0.1 e 0.9  limiteVertical = -0.1 e 0.5.
		Vector3 pos = Camera.main.WorldToViewportPoint(trans.position);
        // Debug.Log("Posicao Atual: " + pos);
        //saiu pela esquerda
		if(pos.x <= limiteHorizontal.x && movFactor.x<0){
            movFactor.x = 0;
		}
        //saiu pela direita
		if(limiteHorizontal.y <= pos.x && movFactor.x>0){
            movFactor.x = 0;
		}
        //saiu por baixo
		if(pos.y <= limiteVertical.x && movFactor.y<0){
            movFactor.y = 0;
            movFactor.z = 0;
		}
        //saiu por cima
		if(limiteVertical.y <= pos.y && movFactor.y>0){
            movFactor.y = 0;
            movFactor.z = 0;
		}
        return movFactor;
	}


    void FixedUpdate()
    {
		if (!natela) {
			mov = Vector3.ClampMagnitude (mov, 1);
			Vector3 nvel = new Vector3 (mov.x *Speed, 0, mov.z *Speed);

			//rdb.velocity = nvel;
			rdb.AddForce(nvel,ForceMode.VelocityChange);
            AnimCTRL.SetMovimentAnimation(mov.magnitude * Speed);
		} else {
			Vector3 nvel = new Vector3 (mov.x * 5, mov.z * 5, rdb.velocity.z);
			rdb.velocity = nvel;

			AnimCTRL.SetMovimentAnimation (rdb.velocity.magnitude);
		}

    }


//
//	public void SetAttackAnim(int AttackNumber){
//		if (anim == null) {
//			return;
//		}
//		anim.SetTrigger ("Attack");
//		anim.SetInteger ("AttackNumber", AttackNumber);
//	}

    void Special()
    {

    }
}
