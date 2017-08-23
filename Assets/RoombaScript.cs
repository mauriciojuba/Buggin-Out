using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaScript : MonoBehaviour {

	void OnCollisionEnter(Collision Col){
		if (Col.collider.gameObject.GetComponent<Life> () != null) {
			if (Col.collider.gameObject.GetComponent<Life> ().LifeQuant > 0) {
				Col.collider.gameObject.GetComponent<Life> ().LifeQuant -= 999;
				Col.collider.gameObject.GetComponent<Life> ().UpdateLife ();
			}
			if (Col.collider.gameObject.GetComponent<FSMMosquito> () != null) {
				Col.collider.gameObject.GetComponent<FSMMosquito> ().state = FSMMosquito.FSMStates.Die;
			}
		}


	}
}
