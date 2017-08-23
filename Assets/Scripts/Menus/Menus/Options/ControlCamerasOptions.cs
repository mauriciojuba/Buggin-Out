using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamerasOptions : MonoBehaviour {

	public GameObject ligaCamera, desligaCamera;

	void OnTriggerEnter(Collider hit){
		if(hit.CompareTag("Player1_3D")){
			ligaCamera.SetActive(true);
			desligaCamera.SetActive(false);
		}

	}
}
