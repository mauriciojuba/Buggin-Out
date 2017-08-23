using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHole : MonoBehaviour {

	[SerializeField] private CameraMenu MenuRef;
	[SerializeField] private Transform OptionPos, MainMenuPos;
	[SerializeField] private bool Used;

	//verifica quando o player entra no buraco, e faz as modificações.
	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player1_3D") && !Used) {
			Used = true;
			GoToSelection ();
		}

		/*if (col.CompareTag ("Player1_3D") && !Used) {
			Used = true;
			if (!MenuRef.InOptions) {
				MenuRef.InOptions = true;
				MenuRef.InMainMenu = false;
				MenuRef.Player.transform.position = OptionPos.position;
			} else {
				MenuRef.InOptions = false;
				MenuRef.InMainMenu = true;
				MenuRef.Player.transform.position = MainMenuPos.position;
			}
		}*/
	}

	//o player só consegue entrar no buraco de novo dps q sair do collider.
	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Player1_3D")) {
			Used = false;
		}
	}


	void GoToSelection(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Selecao Personagens 3D");
	}
}
