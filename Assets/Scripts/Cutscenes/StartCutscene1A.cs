using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene1A : MonoBehaviour {

	public Animator Roomba;
	public CameraControl dollycam;
	bool zoomOut;
	void OnTriggerEnter(Collider hit){
		if(hit.CompareTag("Player1_3D")){
			zoomOut = true;
			Roomba.enabled = true;
            SoundManager.PlaySFX("Roomba");
		}
	}
	void Update(){
		if(zoomOut){
			if(dollycam.distancia < 18){
				dollycam.distancia+=Time.deltaTime*2;
			}
			else{
				zoomOut = false;
			}
		}
	}
}
