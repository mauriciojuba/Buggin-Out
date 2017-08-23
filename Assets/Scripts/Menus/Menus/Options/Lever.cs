using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Lever : MonoBehaviour {

	public Slider OptionSlider;
	public GameObject OptionObj;
	public GameObject LeverObj;
	private GameObject Player;
	[SerializeField] private Transform PosToUse;
	[SerializeField] private Options OptionsReference;

	void Start(){
		Player = GameObject.FindWithTag ("Player1_3D");
	}

	void Update(){

		//se esta no alcance e apertar X, trava a posião do player e começa a movimentar a alavanca.
		if (Player.GetComponent<OptionsPlayer> ().InLever) {
			if (Input.GetButtonDown ("X P1") || Input.GetKeyDown (KeyCode.J)) {
				Player.GetComponent<OptionsPlayer> ().UsingLever = true;
				Player.transform.position = PosToUse.position;
				Player.transform.rotation = PosToUse.rotation;
				OptionsReference.SelectLever (LeverObj, OptionObj, OptionSlider);
				OptionsReference.SetInitialRot ();
			}
		}

		//pra sair da alavanca aperta B.
		if (Player.GetComponent<OptionsPlayer> ().UsingLever) {
			if (Input.GetButtonDown ("B P1") || Input.GetKeyDown(KeyCode.L)) {
				Player.GetComponent<OptionsPlayer> ().UsingLever = false;
				//fazer o apply do options direto aqui
			}
		}
	}

	// Quando o player esta no alcance da alavanca
	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player1_3D")) {
			col.GetComponent<OptionsPlayer> ().InLever = true;
		}
	}

	// Quando o player sai do alcance da alavanca
	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Player1_3D")) {
			col.GetComponent<OptionsPlayer> ().InLever = false;
		}
	}
}
