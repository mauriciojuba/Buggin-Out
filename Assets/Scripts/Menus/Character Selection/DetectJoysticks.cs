using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectJoysticks : MonoBehaviour {
	//Armazena os controles;
	public string[] Joysticks;

    public int ActiveJoy, QuantSelected;

    //Define se o controle esta conectado ou nao.
    public bool P1Connected, P2Connected;

    //Define se o controle esta Ativo, "Apertou A".
    public bool P1Active, P2Active;


    //[Tooltip("Coloque os ScrollRects de seleção de personagens de cada player, no seu devido lugar")]
    //[SerializeField]
    //private ScrollRect ScrollP1;
    //[Tooltip("Coloque os ScrollRects de seleção de personagens de cada player, no seu devido lugar")]
    //[SerializeField]
    //private ScrollRect ScrollP2;
    //[Tooltip("Coloque os ScrollRects de seleção de personagens de cada player, no seu devido lugar")]
    //[SerializeField]
    //private ScrollRect ScrollP3;
    //[Tooltip("Coloque os ScrollRects de seleção de personagens de cada player, no seu devido lugar")]
    //[SerializeField]
    //private ScrollRect ScrollP4;
    [Tooltip("Coloque aqui os objetos para indicar 'Aperte A' para ativar o joystick")]
    [SerializeField]
    private GameObject Press_A_P2;


	[SerializeField] private float Timer;
	[Range(1,5)]
	[SerializeField] private float MaxTimer = 3;

	[SerializeField] private GameObject Player2;
	[SerializeField] private Transform Point;
	[Range(650,1500)]
	[SerializeField] private float Force = 650;

	public Color P2Color = Color.red;

    private GameObject P2;

    Data DataS;
	void Start(){
        if (GameObject.FindWithTag("DATA") != null)
            DataS = GameObject.FindWithTag("DATA").GetComponent<Data>();
        P1Active = true;
	}

	void Update() {
		DetectJoysticksConnected ();
		ActiveJoysticks ();
		DetectActivity ();
		DetectJoy ();
		if (P1Connected) {
			if (QuantSelected == ActiveJoy) {
				Timer -= Time.deltaTime;
				if (Timer <= 0) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (DataS.PhaseName);
				}
			} else {
				Timer = MaxTimer;
			}
		} else {
			if (QuantSelected == 1) {
				Timer -= Time.deltaTime;
				if (Timer <= 0) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (DataS.PhaseName);
				}
			} else {
				Timer = MaxTimer;
			}
		}
	}

	//Ativa os controles quando apertar A.
	void ActiveJoysticks(){
		if (P2Connected) {
			if (!P2Active){
				Press_A_P2.SetActive (true);
				if (Input.GetButtonDown ("A P2")) {
					P2Active = true;
					SummonPlayer (2);
				}
			}
			
			if (P2Active) {
				if (Input.GetButtonDown ("B P2")) {
					P2Active = false;
					Destroy (P2);
				}
			}
		} else {
            if(Press_A_P2 != null)
			Press_A_P2.SetActive (false);

		}	
		
	}

	//detecta atividade do controle.
	//Vou mudar essa parte do codigo.
	void DetectActivity(){
		if (P2Active) {
			Press_A_P2.SetActive (false);

		} else {
			
		}
		
	}

	//detecta se os joyscticks estao conectados.
	void DetectJoysticksConnected(){
		Joysticks = new string[Input.GetJoystickNames ().Length];
		Joysticks = Input.GetJoystickNames ();
	
		if (Joysticks.Length > 0) {
			if (Joysticks [0] != "")
				P1Connected = true;
			else
				P1Connected = false;
			if (Joysticks.Length > 1 && Joysticks [1] != "")
				P2Connected = true;
			else
				P2Connected = false;
		}
	}

	void DetectJoy(){
		if(P1Active && P2Active){
			ActiveJoy = 2;
		}else if(P1Active){
			ActiveJoy = 1;
		}
	}

	void SummonPlayer(int Number){
		if (Number == 2) {
			P2 = GameObject.Instantiate (Player2, Point.position, Point.rotation);
			//P2.transform.Find("PlayerModel").GetComponent<OptionsPlayer> ().CanMove = false;
			//P2.transform.Find("PlayerModel").GetComponent<OptionsPlayer> ().PlayerNumber = Number;
			//StartCoroutine (P2.transform.Find ("PlayerModel").GetComponent<OptionsPlayer> ().StarMove ());
			//P2.transform.Find ("PlayerModel").GetComponent<Rigidbody> ().useGravity = false;
			//P2.transform.Find("PlayerModel").GetComponent<Rigidbody> ().AddForce (Point.forward * Force);
			//P2.transform.Find ("PlayerModel").GetComponent<Rigidbody> ().useGravity = true;
		}
	}
}
