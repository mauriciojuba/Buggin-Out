using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar3D : MonoBehaviour {

	[SerializeField] private Character Char;
	[SerializeField] private Data DataS;
	[SerializeField] private GameObject LightMaterial;
	[SerializeField] private Animator Anim;
	private GameObject Player;

	void Start () {
		DataS = GameObject.FindGameObjectWithTag ("DATA").GetComponent<Data> ();
	}

	void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Player1_3D") || col.CompareTag ("Player2_3D") ||
			col.CompareTag ("Player3_3D") || col.CompareTag ("Player4_3D"))
			//////////////////////////////////////////////////////////////
		{
			Material mat = LightMaterial.GetComponent<Renderer> ().material;
			mat.EnableKeyword ("_EMISSION");
			if (Anim != null)
				Anim.SetBool ("CanSelect", true);
			col.GetComponent<DetectChar> ().CanSelect = true;
			col.GetComponent<DetectChar> ().CharacterPreSelected = gameObject;
		}
	}

	void OnTriggerExit (Collider col){
		if (col.CompareTag ("Player1_3D") || col.CompareTag ("Player2_3D") ||
			col.CompareTag ("Player3_3D") || col.CompareTag ("Player4_3D"))
			//////////////////////////////////////////////////////////////
		{
			Material mat = LightMaterial.GetComponent<Renderer> ().material;
			mat.DisableKeyword ("_EMISSION");
			if (Anim != null)
				Anim.SetBool ("CanSelect", false);
			col.GetComponent<DetectChar> ().CanSelect = false;
			col.GetComponent<DetectChar> ().CharacterPreSelected = null;
		}
	}


	public void OnSelectCharacter(int PlayerNumb){
		StartCoroutine (StartPisca ());
		if (Anim != null) {
			Anim.SetBool ("Selected", true);
			Anim.SetTrigger ("Select");
		}
		if (PlayerNumb == 1) {
			DataS.P1SelectedCharacter = Char;
			DataS.P1SelectedCharacter.PlayerNumber = PlayerNumb;
		}
		else if(PlayerNumb == 2){
			DataS.P2SelectedCharacter = Char;
			DataS.P2SelectedCharacter.PlayerNumber = PlayerNumb;

		}
		else if (PlayerNumb == 3) {
			DataS.P3SelectedCharacter = Char;
			DataS.P3SelectedCharacter.PlayerNumber = PlayerNumb;
		}
		else if(PlayerNumb == 4){
			DataS.P4SelectedCharacter = Char;
			DataS.P4SelectedCharacter.PlayerNumber = PlayerNumb;

		}
	}

	public void OnDeselectCharacter(int PlayerNumb){
		StopAllCoroutines ();
		Material mat = LightMaterial.GetComponent<Renderer> ().material;
		mat.DisableKeyword ("_EMISSION");
		if (Anim != null) {
			Anim.SetBool ("Selected", false);
		}
		if (PlayerNumb == 1)
			DataS.P1SelectedCharacter = null;
		else if(PlayerNumb == 2)
			DataS.P2SelectedCharacter = null;
		else if (PlayerNumb == 3)
			DataS.P3SelectedCharacter = null;
		else if(PlayerNumb == 4)
			DataS.P4SelectedCharacter = null;
	}

	IEnumerator StartPisca(){
		Material mat = LightMaterial.GetComponent<Renderer> ().material;
		yield return new WaitForSeconds (0.1f);
		mat.EnableKeyword ("_EMISSION");
		yield return new WaitForSeconds (0.1f);
		mat.DisableKeyword ("_EMISSION");
		StartCoroutine (StartPisca ());
	}
}
