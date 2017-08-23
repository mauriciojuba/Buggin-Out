using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPlayer : MonoBehaviour {


	[SerializeField] private bool Menus;

	//variaveis do menu inicial/opçoes
	public bool InStairs;
	public bool UsingStairs;
	public GameObject StairsObj;
	public bool InLever;
	public bool UsingLever;
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
	[SerializeField] private bool InGround;

	public Camera CameraMain;


	public LayerMask NoIgnoredLayers = -1;
	[SerializeField] private float MaxJump;
	private bool Jumping;
	public bool CanMove;

	[SerializeField] private Animator Anim;
	[SerializeField] private GameObject Model;
	[SerializeField] private bool KeyboardCanControl;
	bool allowUp,allowDown,allowRight,allowLeft;
	void Start () {
		CameraMain = Camera.main;
		ActualDirection = 3;
		if (PlayerNumber == 1) {
			StartCoroutine (StarMove ());
			KeyboardCanControl = true;
		}
	}

	void FixedUpdate () {
		SetAnimations ();
		if (Menus) {
			if (UsingStairs) {
				Model.transform.localEulerAngles = new Vector3 (-90, 0, 0);
				Model.transform.localPosition = new Vector3 (0, 0, 1.36f);
			} else {
				Model.transform.localEulerAngles = new Vector3 (0, 0, 0);
				Model.transform.localPosition = new Vector3 (0, -1.01f, 0);
			}

			if (!UsingLever) {
				DirectionDefinition ();
				Jump ();
			}
//			if (CameraMain.gameObject.GetComponent<CameraMenu> ().InOptions) {
//				//TestPosition ();
//			}
			if (InStairs) {
				if (Input.GetButtonDown ("X P1")) {
					UsingStairs = true;
					ActualDirection = 0;
				}
			}
			//quando o player estiver na escada, desliga a gravidade e altera apenas a velocidade Y do player.
			if (UsingStairs) {
				DirectionDefinition ();
				GetComponent<Rigidbody> ().useGravity = false;
				if (Input.GetAxis ("Vertical P1") > 0.2f || Input.GetAxis ("Vertical P1") < -0.2f) {
					Vector3 V3 = GetComponent<Rigidbody> ().velocity;
					V3.y = StairsObj.transform.up.y * Speed * Input.GetAxis ("Vertical P1");
					GetComponent<Rigidbody> ().velocity = V3;
				} else {
					Vector3 V3 = GetComponent<Rigidbody> ().velocity;
					V3.y = 0;
					GetComponent<Rigidbody> ().velocity = V3;
				}if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < -0.2f ||Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0.2f) {
					Vector3 V3 = GetComponent<Rigidbody> ().velocity;
					V3.x = StairsObj.transform.right.x * Speed * Input.GetAxis ("Horizontal P" + PlayerNumber);
					GetComponent<Rigidbody> ().velocity = V3;
				} 
				else{
					Vector3 V3 = GetComponent<Rigidbody> ().velocity;
					V3.x = 0;
					GetComponent<Rigidbody> ().velocity = V3;
				}
			} else {
				GetComponent<Rigidbody> ().useGravity = true;
			}
		} else {
			if (CanMove) {
				DirectionDefinition ();
			}
			Jump ();
		}
	}


	//Define a direção que o player olha de acordo com os botoes que ele aperta.
	void DirectionDefinition(){
		if (KeyboardCanControl) {
			if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) > 0 || Input.GetKey(KeyCode.W)) {
				InMovement = true;
				//Define a direção para Cima-Esquerda.
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 && !UsingStairs || Input.GetKey(KeyCode.A) && !UsingStairs) {
					ActualDirection = 4;
				} 
				//Define a direção para Cima-Direita.
				else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 && !UsingStairs || Input.GetKey(KeyCode.D) && !UsingStairs) {
					ActualDirection = 5;
				} 
				//Define a direção para Cima.
				else {
					ActualDirection = 0;
				}
			} else if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) < 0 && !UsingStairs || Input.GetKey(KeyCode.S) && !UsingStairs) {
				InMovement = true;
				//Define a direção para Baixo-Esquerda
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 || Input.GetKey(KeyCode.A)) {
					ActualDirection = 6;
				} 
				//Define a direção para Baixo-Direita
				else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 || Input.GetKey(KeyCode.W)) {
					ActualDirection = 7;
				} 
				//Define a direção para Baixo.
				else {
					ActualDirection = 1;
				}
			} 
			//Define a direção para Esquerda
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 && !UsingStairs || Input.GetKey(KeyCode.A) && !UsingStairs) {
				InMovement = true;
				ActualDirection = 2;
			} 
			//Define a direção para Direita
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 && !UsingStairs || Input.GetKey(KeyCode.D) && !UsingStairs) {
				InMovement = true;
				ActualDirection = 3;
			} else {
				InMovement = false;
			}
		} else {
			if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) > 0) {
				InMovement = true;
				//Define a direção para Cima-Esquerda.
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 && !UsingStairs) {
					ActualDirection = 4;
				} 
			//Define a direção para Cima-Direita.
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 && !UsingStairs) {
					ActualDirection = 5;
				} 
			//Define a direção para Cima.
			else {
					ActualDirection = 0;
				}
			} else if (Input.GetAxisRaw ("Vertical P" + PlayerNumber) < 0 && !UsingStairs) {
				InMovement = true;
				//Define a direção para Baixo-Esquerda
				if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0) {
					ActualDirection = 6;
				} 
			//Define a direção para Baixo-Direita
			else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0) {
					ActualDirection = 7;
				} 
			//Define a direção para Baixo.
			else {
					ActualDirection = 1;
				}
			} 
		//Define a direção para Esquerda
		else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) < 0 && !UsingStairs) {
				InMovement = true;
				ActualDirection = 2;
			} 
		//Define a direção para Direita
		else if (Input.GetAxisRaw ("Horizontal P" + PlayerNumber) > 0 && !UsingStairs) {
				InMovement = true;
				ActualDirection = 3;
			} else {
				InMovement = false;
			}
		}
		transform.LookAt (new Vector3 (Directions [ActualDirection].position.x, transform.position.y, Directions [ActualDirection].position.z));

		//Define se o Player esta em movimento ou não, alterando apenas a velocidade em x e em z, para nao alterar a gravidade.
		if (InMovement) {
			Vector3 V3 = Player.GetComponent<Rigidbody> ().velocity;
			V3.x = transform.forward.x * Speed;
			V3.z = transform.forward.z * Speed;
			Player.GetComponent<Rigidbody> ().velocity = V3;
		} else if (!InMovement) {
			Vector3 V3 = Player.GetComponent<Rigidbody> ().velocity;
			V3.x = transform.forward.x * 0;
			V3.z = transform.forward.z * 0;
			Player.GetComponent<Rigidbody> ().velocity = V3;
		}

	}

	//função para pulo
	void Jump(){
		//verifica se o player esta encostando no chão
		InGround = Physics.Linecast (Player.transform.position - new Vector3(0,0.5f,0), Player.transform.position - Vector3.up * 1.1f, NoIgnoredLayers);
		Debug.DrawLine (Player.transform.position - new Vector3(0,0.5f,0) ,Player.transform.position - Vector3.up * 1.1f);

		//se estiver no chao, pula, apertando A no controle.
		if (KeyboardCanControl) {
			if (Input.GetButtonDown ("A P" + PlayerNumber) && InGround || Input.GetKeyDown(KeyCode.K) && InGround) {
				Jumping = true;
				Vector3 V3 = Player.GetComponent<Rigidbody> ().velocity;
				V3.y = JumpForce;
				Player.GetComponent<Rigidbody> ().velocity = V3;
				Anim.SetTrigger ("Jump");
			}
			if (Input.GetButtonUp ("A P" + PlayerNumber) || Input.GetKeyUp(KeyCode.K)) {
				Jumping = false;
			}
		} else {
			if (Input.GetButtonDown ("A P" + PlayerNumber) && InGround) {
				Jumping = true;
				Vector3 V3 = Player.GetComponent<Rigidbody> ().velocity;
				V3.y = JumpForce;
				Player.GetComponent<Rigidbody> ().velocity = V3;
				Anim.SetTrigger ("Jump");
			}
			if (Input.GetButtonUp ("A P" + PlayerNumber)) {
				Jumping = false;
			}
		}

		if(Jumping){
			Vector3 V3 = Player.GetComponent<Rigidbody> ().velocity;
			V3.y += JumpForce;
			Player.GetComponent<Rigidbody> ().velocity = V3;
		}
		if(Player.GetComponent<Rigidbody> ().velocity.y > MaxJump){
			Jumping = false;
		}
	}

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

	void SetAnimations(){
		if (Anim != null) {
			Anim.SetBool ("InMovement", InMovement);
		}
	}

	public IEnumerator StarMove(){
		yield return new WaitForSeconds (1);
		CanMove = true;
	}
}
