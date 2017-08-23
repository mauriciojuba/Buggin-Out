using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Options : MonoBehaviour {

	[SerializeField] private Slider SliderSelected;
	[SerializeField] private GameObject OptionObj;
	[SerializeField] private GameObject Player;
	public GameObject Lever;

	[SerializeField] private float Speed;
	[Range(1.5f,25)]
	[SerializeField] private float SliderSpeed;

	[HideInInspector]
	public float InitRot , AngleToGo, PosToGo, InitPos;

	[HideInInspector]
	public float rotX;
	public float rotY;
	public float rotZ;
	public float Direction;
	void Start(){
		Player = GameObject.FindWithTag ("Player1_3D");
	}


	void Update(){
		//controla a alavanca, mexendo ela de acordo com o joystick
		if (Lever != null) {
			if (Player.GetComponent<OptionsPlayer> ().UsingLever) {
				if (Input.GetKey (KeyCode.D)) {
					Direction = 1;
				} else if (Input.GetKey (KeyCode.A)) {
					Direction = -1;
				}else
				Direction = Input.GetAxis ("Horizontal P1");
			} else {
				Direction = 0;
			}
			AngleToGo = Mathf.MoveTowards (AngleToGo, Direction, Time.deltaTime * Speed);
			PosToGo = Mathf.MoveTowards (PosToGo, Direction, Time.deltaTime * Speed);

			Lever.transform.localEulerAngles = new Vector3 (rotX, rotY, InitRot + (AngleToGo * 45f * -1));
			if (Direction > 0.2f) {
				Plus ();
			} else if (Direction < 0.2f) {
				Minus ();
			}
		}
	}

	/// Função para selecionar a alavanca
	public void SelectLever(GameObject lever, GameObject optionObj, Slider slider){
		Lever = lever;
		OptionObj = optionObj;
		SliderSelected = slider;
	}
		
	/// Função para Deselecionar a alavanca
	public void DeselectLever(){
		Lever = null;
		OptionObj = null;
		SliderSelected = null;
	}

	/// Sets the initial rot.
	public void SetInitialRot(){
		InitRot = Lever.transform.localEulerAngles.z;
		InitPos = OptionObj.transform.position.x;

		rotX = Lever.transform.localEulerAngles.x;
		rotY = Lever.transform.localEulerAngles.y;
		rotZ = Lever.transform.localEulerAngles.z;
	}

	//opção + de volume
	public void Plus(){
		if(SliderSelected.value < SliderSelected.maxValue)
			SliderSelected.value += Time.deltaTime * Mathf.Abs(Direction) * SliderSpeed;
	}

	//opção - de volume
	public void Minus(){
		if (SliderSelected.value > SliderSelected.minValue)
			SliderSelected.value -= Time.deltaTime * Mathf.Abs(Direction) * SliderSpeed;
	}
}
