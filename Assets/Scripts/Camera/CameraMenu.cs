using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour {

	[SerializeField] private Transform OptionsPos, MainMenuPos;
	[SerializeField] private float Speed;
	[SerializeField] private Transform PlayerDirections;
	public GameObject Player;

	public bool InOptions, InMainMenu;
	
	void Update () {
       
		PlayerDirections.localEulerAngles = new Vector3 (PlayerDirections.localEulerAngles.x, gameObject.transform.localEulerAngles.y, PlayerDirections.localEulerAngles.z);
		if (OptionsPos != null && MainMenuPos != null) {
			//mantem as direções do player de acordo com a camera.

			if (InOptions) {
				//move a camera pra posição certa do options.
				if (transform.position != OptionsPos.position) {
					transform.position = Vector3.MoveTowards (transform.position, OptionsPos.position, Speed * Time.deltaTime);
				}
				if (transform.rotation != OptionsPos.rotation) {
					transform.localEulerAngles = Vector3.MoveTowards (transform.localEulerAngles, OptionsPos.localEulerAngles, Speed * Time.deltaTime);
				}
				if (transform.position == OptionsPos.position && transform.rotation == OptionsPos.rotation) {
					Player.GetComponent<OptionsPlayer> ().enabled = true;
				} else {
					Player.GetComponent<OptionsPlayer> ().enabled = false;
				}

			}

			if (InMainMenu) {
				//move a camera pra posição certa do menu inicial.
				if (transform.position != MainMenuPos.position) {
					transform.position = Vector3.MoveTowards (transform.position, MainMenuPos.position, Speed * Time.deltaTime);
				}
				if (transform.rotation != MainMenuPos.rotation) {
					transform.localEulerAngles = Vector3.MoveTowards (transform.localEulerAngles, MainMenuPos.localEulerAngles, Speed * Time.deltaTime);
				}

				if (transform.position == MainMenuPos.position && transform.rotation == MainMenuPos.rotation) {
					Player.GetComponent<OptionsPlayer> ().enabled = true;
				} else {
					Player.GetComponent<OptionsPlayer> ().enabled = false;
				}
			}

		}
	}

}
