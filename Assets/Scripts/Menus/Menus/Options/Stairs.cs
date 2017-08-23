using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

	//esta no range da escada
	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player1_3D")) {
			col.GetComponent<OptionsPlayer> ().InStairs = true;
			col.GetComponent<OptionsPlayer> ().StairsObj = gameObject;
		}
	}

	//sai do range da escada
	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Player1_3D")) {
			col.GetComponent<OptionsPlayer> ().StairsObj = null;
			col.GetComponent<OptionsPlayer> ().InStairs = false;
			col.GetComponent<OptionsPlayer> ().UsingStairs = false;
		}
	}
}
