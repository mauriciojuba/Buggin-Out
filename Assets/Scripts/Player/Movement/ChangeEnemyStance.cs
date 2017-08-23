using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyStance : MonoBehaviour {

	public int PlayerNumber;

	public GameObject Model3D, Model2D;
	public GameObject Model;

	private StyleControl Style;
	public GameObject Target;
	public ChangeStance TargetScript;
	public bool In3D;


	void Start () {
		Target = GameObject.FindWithTag ("Enemy");
		Style = GameObject.FindWithTag ("StyleControl").GetComponent<StyleControl> ();
		TargetScript = Target.GetComponent<ChangeStance> ();
	}
	
	void Update () {
		if (PlayerNumber == 1) {
			In3D = Style.In3D_P1;
		} else if (PlayerNumber == 2) {
			In3D = Style.In3D_P2;
		}

		if (In3D) {
			Model = Model3D;
		} else {
			Model = Model2D;
		}

		if (Input.GetButtonDown ("A P" + PlayerNumber) && Vector3.Distance(Model.transform.position,Target.transform.position) <= 10 && In3D == TargetScript.In3D) {
			TargetScript.ChangeS();
			if (TargetScript.In3D)
				Target = TargetScript.Model3D;
			else
				Target = TargetScript.Spriter;
		}
	}
}
