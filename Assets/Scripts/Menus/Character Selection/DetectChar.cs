using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectChar : MonoBehaviour {

	public bool CanSelect;
	private bool Selected;
	public GameObject CharacterPreSelected;
	private int PlayerNumber;

	[SerializeField] private DetectJoysticks DetectS;
	[SerializeField] private bool KeyboardCancontrol;

	void Start () {
		PlayerNumber = GetComponent<PlayerNumb> ().PlayerNumber;
		if (PlayerNumber == 1) {
			KeyboardCancontrol = true;
		}

		if (GameObject.FindWithTag ("Detect") != null)
			DetectS = GameObject.FindWithTag ("Detect").GetComponent<DetectJoysticks> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CanSelect) {
			if (KeyboardCancontrol) {
				if (Input.GetButtonDown ("X P" + PlayerNumber) && !Selected || Input.GetKeyDown (KeyCode.J) && !Selected) {
					CharacterPreSelected.GetComponent<SelectChar3D> ().OnSelectCharacter (PlayerNumber);
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					//GetComponent<OptionsPlayer> ().enabled = false;

					DetectS.QuantSelected++;
					Selected = true;

				}
				if (Selected) {
					if (Input.GetButtonDown ("B P" + PlayerNumber) || Input.GetKeyDown (KeyCode.L)) {
						CharacterPreSelected.GetComponent<SelectChar3D> ().OnDeselectCharacter (PlayerNumber);
					//	GetComponent<OptionsPlayer> ().enabled = true;
						Selected = false;
						DetectS.QuantSelected--;
					}
				}
			} else {
				if (Input.GetButtonDown ("X P" + PlayerNumber) && !Selected) {
					CharacterPreSelected.GetComponent<SelectChar3D> ().OnSelectCharacter (PlayerNumber);
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					//GetComponent<OptionsPlayer> ().enabled = false;

					DetectS.QuantSelected++;
					Selected = true;

				}
				if (Selected) {
					if (Input.GetButtonDown ("B P" + PlayerNumber)) {
						CharacterPreSelected.GetComponent<SelectChar3D> ().OnDeselectCharacter (PlayerNumber);
						//GetComponent<OptionsPlayer> ().enabled = true;
						Selected = false;
						DetectS.QuantSelected--;
					}
				}
			}
		}
	}
}
