using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao3D : MonoBehaviour {

	[Header("controle de velocidade")]
	[Tooltip("A velocidade é controlada pelo analógico?")]
	[SerializeField] private bool SpeedControl;

    private Rigidbody rb;
	//Array de direções que o player pode olhar
	public Transform[] Directions;
	//Direção atual que o player esta olhando
	public int ActualDirection;

	//define a velocidade de movimentação do player
	public float Speed;

	//define se o player esta em movimento ou não.
	public bool InMovement;

	//Define o numero do Player.
	public int PlayerNumber;

	//GO do personagem que sera movimentado.
	public GameObject Player;
	

	[SerializeField] private float JumpForce;
	public bool InGround;

	public Camera CameraMain;
    public CameraControl DollyCam;
    public Transform playerRoot;
    public Transform direcoes;
    public Transform camScreen;
    public float velTransicao;
    public bool onScreen;
    Quaternion direct2D;
    float _2dY,_2dX;

    public LayerMask NoIgnoredLayers = -1;
	[SerializeField] private float MaxJump;
	private bool Jumping;

	public bool CanMove;
	public bool Stunned;

	bool allowUp,allowDown,allowRight,allowLeft;
	public Animator Anim;
    public Transform model;
	public GameObject Sombra;
	public LayerMask SombraIgnorePlayer;

	[SerializeField] private GameObject ResurrectCol;
	[SerializeField] public bool KeyboardCanControl;

	[SerializeField] private float ScreenX,ScreenY,ScreenZ;
	[SerializeField] private float RotScreenX, RotScreenY, RotScreenZ;
	[SerializeField] private float WorldX, WorldY, WorldZ;


	public bool InDialogue;
	public bool Liz;
	void Start () {
		camScreen = GameObject.FindWithTag ("ScreenGlass").transform;
		DollyCam = GameObject.FindWithTag ("DollyCam").GetComponent<CameraControl> ();
		CanMove = true;
        rb = Player.GetComponent<Rigidbody>();
        CameraMain = Camera.main;
		ActualDirection = 3;
        direct2D = Quaternion.Euler(90f, 0f, 0f);
		if (PlayerNumber == 1)
			KeyboardCanControl = true;
	  
    }

	void Update(){
		ResurrectCol.gameObject.SetActive (Stunned);
		TestPosition ();
		SetAnimations();
		if (CanMove) {
			Jump ();
			if (!InDialogue) {
				CastShadowOnJump ();
				goToScreen ();
				exitScreen ();
			}
		}
    }

	void FixedUpdate () {
		if (CanMove) {
			if (!InDialogue) {
				DirectionDefinition ();
			}
		}
		

	}


	//Define a direção que o player olha de acordo com os botoes que ele aperta.
	void DirectionDefinition(){
		if (KeyboardCanControl) {
			if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) > 0 || Input.GetKey(KeyCode.W)) {
				InMovement = true;
				//Define a direção para Cima-Esquerda.
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 || Input.GetKey(KeyCode.A)) {
					ActualDirection = 4;
					direct2D = Quaternion.Euler (225f, 90f, 90f);
				} 
				//Define a direção para Cima-Direita.
				else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 || Input.GetKey(KeyCode.D)) {
					ActualDirection = 5;
					direct2D = Quaternion.Euler (315f, 90f, 90f);
				} 
				//Define a direção para Cima.
				else {
					ActualDirection = 0;
					direct2D = Quaternion.Euler (270f, 90f, 90f);

				}
			} else if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) < 0 || Input.GetKey(KeyCode.S)) {
				InMovement = true;
				//Define a direção para Baixo-Esquerda
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 || Input.GetKey(KeyCode.A)) {
					ActualDirection = 6;
					direct2D = Quaternion.Euler (135f, 90f, 90f);
				} 
				//Define a direção para Baixo-Direita
				else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 || Input.GetKey(KeyCode.D)) {
					ActualDirection = 7;
					direct2D = Quaternion.Euler (45f, 90f, 90f);
				} 
				//Define a direção para Baixo.
				else {
					ActualDirection = 1;
					direct2D = Quaternion.Euler (90f, 90f, 90f);
				}
			} 
			//Define a direção para Esquerda
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 || Input.GetKey(KeyCode.A)) {
				InMovement = true;
				ActualDirection = 2;
				direct2D = Quaternion.Euler (180f, 90f, 90f);

			} 
			//Define a direção para Direita
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 || Input.GetKey(KeyCode.D)) {
				InMovement = true;
				ActualDirection = 3;
				direct2D = Quaternion.Euler (0f, 90f, 90f);
			} else {
				InMovement = false;
			}
		} else {
			if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) > 0) {
				InMovement = true;
				//Define a direção para Cima-Esquerda.
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0) {
					ActualDirection = 4;
					direct2D = Quaternion.Euler (225f, 90f, 90f);
				} 
			//Define a direção para Cima-Direita.
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0) {
					ActualDirection = 5;
					direct2D = Quaternion.Euler (315f, 90f, 90f);
				} 
			//Define a direção para Cima.
			else {
					ActualDirection = 0;
					direct2D = Quaternion.Euler (270f, 90f, 90f);

				}
			} else if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) < 0) {
				InMovement = true;
				//Define a direção para Baixo-Esquerda
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0) {
					ActualDirection = 6;
					direct2D = Quaternion.Euler (135f, 90f, 90f);
				} 
			//Define a direção para Baixo-Direita
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0) {
					ActualDirection = 7;
					direct2D = Quaternion.Euler (45f, 90f, 90f);
				} 
			//Define a direção para Baixo.
			else {
					ActualDirection = 1;
					direct2D = Quaternion.Euler (90f, 90f, 90f);
				}
			} 
		//Define a direção para Esquerda
		else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0) {
				InMovement = true;
				ActualDirection = 2;
				direct2D = Quaternion.Euler (180f, 90f, 90f);
            
			} 
		//Define a direção para Direita
		else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0) {
				InMovement = true;
				ActualDirection = 3;
				direct2D = Quaternion.Euler (0f, 90f, 90f);
			} else {
				InMovement = false;
			}
		}
        if (!onScreen) transform.LookAt(new Vector3(Directions[ActualDirection].position.x, transform.position.y, Directions[ActualDirection].position.z));
        else if (reachScreen) transform.localRotation = direct2D;

        //Define se o Player esta em movimento ou não, alterando apenas a velocidade em x e em z, para nao alterar a gravidade.
        if (!onScreen)
        {
            if (InMovement)
            {
                Vector3 V3 = rb.velocity;
				if (SpeedControl) {
					if (Input.GetAxis ("Horizontal P" + PlayerNumber) > 0 && allowRight) {
						V3.x = transform.forward.x * Input.GetAxis ("Horizontal P" + PlayerNumber) * Speed;
					} else if (Input.GetAxis ("Horizontal P" + PlayerNumber) < 0 && allowLeft) {
						V3.x = transform.forward.x * -Input.GetAxis ("Horizontal P" + PlayerNumber) * Speed;
					}
					if (Input.GetAxis ("Vertical P" + PlayerNumber ) > 0 && allowUp) {
						V3.z = transform.forward.z * Input.GetAxis ("Vertical P" + PlayerNumber) * Speed;
					} else if (Input.GetAxis ("Vertical P" + PlayerNumber) < 0 && allowDown) {
						V3.z = transform.forward.z * -Input.GetAxis ("Vertical P" + PlayerNumber) * Speed;
					}
				} else {
					V3.x = transform.forward.x * Speed;
					V3.z = transform.forward.z * Speed;
				}
                rb.velocity = V3;
            }
            else if (!InMovement)
            {
                Vector3 V3 = rb.velocity;
                V3.x = transform.forward.x * 0;
                V3.z = transform.forward.z * 0;
                rb.velocity = V3;
            }
        }
        else
        {
            if (InMovement)
            {
                Vector3 V3 = rb.velocity;
                V3.x = transform.forward.x * Speed / 10;
                V3.z = transform.forward.z * Speed / 10;
                V3.y = transform.forward.z * Speed / 10;
				if(!allowDown && V3.y<0){
					V3.y = 0;
					V3.z = 0;
				}
				if(!allowUp && V3.y>0){
					V3.y = 0;
					V3.z = 0;
				}
				if(!allowLeft && V3.x<0){
					V3.x = 0;
				}
				if(!allowRight && V3.x>0){
					V3.x = 0;
				}
                rb.velocity = V3;
            }
            else if (!InMovement)
            {
                Vector3 V3 = rb.velocity;
                V3.x = transform.forward.x * 0;
                V3.z = transform.forward.z * 0;
                V3.y = transform.forward.z * 0;
                rb.velocity = V3;
            }
        }
    }

    
	//função para pulo
	void Jump(){
		//verifica se o player esta encostando no chão
		InGround = Physics.Linecast (Player.transform.position, Player.transform.position - Vector3.up * 1.1f, NoIgnoredLayers);
		//Debug.DrawLine (Player.transform.position,Player.transform.position - Vector3.up * 1.1f);
		if (!InDialogue) {
			if (KeyboardCanControl) {
				if (Input.GetButtonDown ("A P" + PlayerNumber) && InGround && !onScreen || Input.GetKeyDown (KeyCode.K) && InGround && !onScreen) {

					Jumping = true;
					SetJumpAnim ();
					Vector3 V3 = rb.velocity;
					V3.y = JumpForce;
					rb.velocity = V3;
				}
				if (Input.GetButtonUp ("A P" + PlayerNumber) || Input.GetKeyUp (KeyCode.K)) {
					Jumping = false;
				}
			} else {
				//se estiver no chao, pula, apertando A no controle.
				if (Input.GetButtonDown ("A P" + PlayerNumber) && InGround && !onScreen) {
			
					Jumping = true;
					SetJumpAnim ();
					Vector3 V3 = rb.velocity;
					V3.y = JumpForce;
					rb.velocity = V3;
				}
				if (Input.GetButtonUp ("A P" + PlayerNumber)) {
					Jumping = false;
				}
			}

			if (Jumping && !onScreen) {
				Vector3 V3 = rb.velocity;
				V3.y += JumpForce;
				rb.velocity = V3;
            

			}
			if (rb.velocity.y > MaxJump && !onScreen) {
				Jumping = false;
			}
			if (KeyboardCanControl) {
				if (Input.GetButtonDown ("LB P" + PlayerNumber) && !onScreen && !PauseMenu.gamePaused || Input.GetKeyDown (KeyCode.Q) && !onScreen && !PauseMenu.gamePaused) {
					//desabilita gravidade coloca o player como child da tela e faz o caminho do player pra tela(MoveTowards) e diminui o tamanho do player, pra não ficar gigante ao se aproximar
					ActualDirection = 1;
					SetAnimOnScreen ();
					Jumping = true;
					Vector3 V3 = rb.velocity;
					V3.y = JumpForce;
					rb.velocity = V3;
					rb.useGravity = false;
					transform.SetParent (camScreen);
					_2dX = Random.Range (-1.5f, +1.5f);
					_2dY = Random.Range (-0.8f, +0.8f);
					onScreen = true;
				}
			} else {
				if (Input.GetButtonDown ("LB P" + PlayerNumber) && !onScreen && !PauseMenu.gamePaused) {
					//desabilita gravidade coloca o player como child da tela e faz o caminho do player pra tela(MoveTowards) e diminui o tamanho do player, pra não ficar gigante ao se aproximar
					SetAnimOnScreen ();
					Jumping = true;
					Vector3 V3 = rb.velocity;
					V3.y = JumpForce;
					rb.velocity = V3;
					rb.useGravity = false;
					transform.SetParent (camScreen);
					_2dX = Random.Range (-1.5f, +1.5f);
					_2dY = Random.Range (-0.8f, +0.8f);
					onScreen = true;
				}
			}
		}
    }

    // variavel que fala quando o personagem vai voltar para o 3D
    [SerializeField] private bool reachScreen;
    public bool toWorld;

bool landOnScreen;
float preventMovLock = 0;
    void goToScreen()
    {
        //testa se o jogador pediu pra ir pra tela
        if (onScreen)
		{
            //variavel para parar de controlar o jogador após ele chegar na tela
			if (!reachScreen && !PauseMenu.gamePaused)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(camScreen.localPosition.x + _2dX,
                    camScreen.localPosition.y + _2dY, camScreen.localPosition.z), velTransicao);
                //rotação pra deixar o modelo pronto pra movimentação na tela e colocar os pés do modelo no "vidro"
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), velTransicao/6);
				if (transform.localPosition == new Vector3(camScreen.localPosition.x + _2dX, camScreen.localPosition.y + _2dY,camScreen.localPosition.z))
				{
					landOnScreen = true;
					ScreenShake.Instance.Shake(0.2f,0.1f);
					ScreenShake.Instance.Blur();
				}
				if(landOnScreen){
					transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(90, Vector3.right), velTransicao/2);
                	//model.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(0, Vector3.right), velTransicao/2);
					model.localEulerAngles = Vector3.Lerp(model.localEulerAngles, new Vector3(RotScreenX,RotScreenY,RotScreenZ), velTransicao/2);
					model.localPosition = Vector3.Lerp (model.localPosition, new Vector3 (ScreenX, ScreenY, ScreenZ), velTransicao / 2);
					if(preventMovLock>=3f){
						reachScreen = true;
						preventMovLock = 0f;
					}else{
						preventMovLock+=Time.deltaTime;
					}
				}
				if(model.localRotation.x >= 0.58f && transform.localRotation.x >= 0.69f){
					reachScreen = true;
				}
            }
            //coloca as esferas de direções na tela também;
            direcoes.transform.SetParent(camScreen);
            direcoes.transform.localRotation = Quaternion.AngleAxis(270, Vector3.right);


            //se o jogador pedir pra descer
			if (KeyboardCanControl) {
				if (Input.GetButtonDown ("RB P" + PlayerNumber) && !PauseMenu.gamePaused || Input.GetKeyDown(KeyCode.T) && !PauseMenu.gamePaused) {
					//tira ele do parent, ativa a variavel que fala que é pra ir pro 3D, liga a gravidade, e desativa a variavel que fala q ele ta na tela
					//SetAnimOffScreen();
					ActualDirection = 0;
					SetAnimOffScreen ();
					transform.SetParent (playerRoot);
					direcoes.transform.SetParent (playerRoot);
					toWorld = true;
					reachScreen = false;
					landOnScreen = false;
					onScreen = false;
				}
			} else {
				if (Input.GetButtonDown ("RB P" + PlayerNumber) && !PauseMenu.gamePaused) {
					//tira ele do parent, ativa a variavel que fala que é pra ir pro 3D, liga a gravidade, e desativa a variavel que fala q ele ta na tela
					//SetAnimOffScreen();
					ActualDirection = 0;
					SetAnimOffScreen ();
					transform.SetParent (playerRoot);
					direcoes.transform.SetParent (playerRoot);
					toWorld = true;
					reachScreen = false;
					landOnScreen = false;
					onScreen = false;
				}
			}
        }
    }
    
    //funçao que tira ele da tela
	float contLiz;
    void exitScreen()
    {
        //se a variavel q fala pra ele sair da tela tiver ligada ele deve consertar o tamanho e a rotação do personagem
		if (toWorld && !PauseMenu.gamePaused)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(DollyCam.target.position.x,
				transform.position.y, DollyCam.target.position.z), velTransicao);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), velTransicao / 4);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(0, Vector3.right), velTransicao * 5);
            model.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,0,0), velTransicao * 5);
			model.localPosition = Vector3.Lerp (model.localPosition, new Vector3 (WorldX, WorldY, WorldZ), velTransicao * 5);
            direcoes.transform.localRotation = Quaternion.AngleAxis(0, Vector3.right);
			if(Liz){
				contLiz+=Time.deltaTime;
				if(contLiz >= 2f){
					CorrectAnimatorLiz();
					contLiz = 0;
				}
			}
        }
        //uma vez que o tamanho esta ok a variavel pode ficar falsa.
        if(transform.localScale == new Vector3(1, 1, 1) /*&& transform.localRotation.x == 0f*/)
        {
			contLiz = 0;
            rb.useGravity = true;
            //model.localRotation = Quaternion.Euler(0, 0, 0);
            toWorld = false;
        }
		//Debug.Log(contLiz);
		
    }
	public void CorrectAnimatorLiz(){
		transform.localScale = new Vector3(1, 1, 1);

		onScreen = false;
		toWorld = false;
		rb.useGravity = true;
		CanMove = true;
		InGround = true;
		InMovement = true;
		Jumping = false;
	}


    //Ver o metodo no CameraControl
	void TestPosition(){
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		if(pos.x <= 0.1f){
			allowLeft = false;
		}
		else{
			allowLeft = true;
		}
		if(0.9f <= pos.x){
			allowRight = false;
		}
		else{
			allowRight = true;
		}
		if(pos.y <= 0.1f){
			allowDown = false;
		} 
		else{
			allowDown = true;
		}
		if(0.9f <= pos.y){
			allowUp = false;
		}
		else{
			allowUp = true;
		}
	}

	void CastShadowOnJump(){
		if(!onScreen){
			Ray ray = new Ray(this.transform.position,Vector3.down);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 10f,SombraIgnorePlayer)){
				Vector3 correctedPoint = new Vector3(hit.point.x,hit.point.y+0.1f,hit.point.z);
				Sombra.transform.position = correctedPoint;
			}
		}
	}

	void SetAnimations(){
		if (Anim == null) {
			return;
		}
		if (rb.velocity.y > 0)
			Anim.SetBool ("Jumping", true);
		else
			Anim.SetBool ("Jumping", false);
		Anim.SetBool ("InAir", !InGround);
		Anim.SetBool ("InMovement", InMovement);
		Anim.SetBool ("ReachScreen", landOnScreen);
		Anim.SetBool ("Stuned", Stunned);
	}

	void SetJumpAnim(){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("Jump");
	}

	public void SetAttackAnim(int AttackNumber){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("Attack");
		Anim.SetInteger ("AttackNumber", AttackNumber);
	}
	public void SetStrongAttackAnim(string Part){
		if (Anim == null) {
			return;
		}
		if (Part == "Charging") {
			Anim.SetBool ("ReleaseStrongAttack", false);
			Anim.SetBool ("ChargeStrongAttack", true);
		} else if (Part == "Release") {
			Anim.SetBool ("ChargeStrongAttack", false);
			Anim.SetBool ("ReleaseStrongAttack", true);
		}
	}
	void SetAnimOnScreen(){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("JumpScreen");
	}

	void SetAnimOffScreen(){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("JumpOffScreen");
	}

	public void SetGrabAnim(){
		if(Anim == null){
			return;
		}
		Anim.SetTrigger ("Grab");
	}

	public void SetGrabbedAnim(bool Grabbed){
		if (Anim == null) {
			return;
		}
		Anim.SetBool ("Grabbed", Grabbed);
	}

	public void SetGrabbingAnim(bool Grabbing){
		if (Anim == null) {
			return;
		}
		Anim.SetBool ("Grabbing", Grabbing);
	}

	public void SetGrabbedFalse(){
		if (Anim == null) {
			return;
		}
		Anim.SetBool ("Grabbed", false);
	}

	public void SetTakeDamageAnim(){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("TakeDamage");
	}

	public void SetSpecialAnim(){
		if (Anim == null) {
			return;
		}
		Anim.SetTrigger ("UseSpecial");
	}

	public void CanMoveFalse(){
		CanMove = false;
	}

	public void CanMoveTrue(){
		CanMove = true;
	}

	public IEnumerator SetDialogueFalse(){
		yield return new WaitForSeconds (0.1f);
		InDialogue = false;
	}
}

