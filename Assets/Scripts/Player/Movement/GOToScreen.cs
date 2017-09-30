using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOToScreen : MonoBehaviour {

	public CameraControl Cam;
    float time=0;

	public bool GoToScreen(Transform telapos, GameObject ObjectThatGoes){
        
        ObjectThatGoes.transform.localScale = Vector3.Lerp (ObjectThatGoes.transform.localScale, telapos.localScale, time);
        ObjectThatGoes.transform.position = Vector3.Lerp (ObjectThatGoes.transform.position, telapos.position,time);
        ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, telapos.rotation, time);
        time += Time.deltaTime*0.1f;

		if (ObjectThatGoes.transform.position == telapos.position && ObjectThatGoes.transform.localScale == telapos.localScale) {
			ObjectThatGoes.transform.rotation = telapos.rotation;
			ObjectThatGoes.transform.parent = telapos;
	//		if (ObjectThatGoes.GetComponent<HornControl> () != null) {
	//			ObjectThatGoes.GetComponent<HornControl> ().Going = false;
	//			ObjectThatGoes.GetComponent<HornControl> ().natela = true;
	//			Cam.ChecarNaTela ();
	//		}
			return true;
		} else {
			return false;
		}
	}

	public bool GoOffScreen(Transform Pos, GameObject ObjectThatGoes){
        time = 0;
        ObjectThatGoes.transform.parent = null;
		ObjectThatGoes.transform.localScale = Vector3.MoveTowards (ObjectThatGoes.transform.localScale, new Vector3(1,1,1), Time.deltaTime);
		ObjectThatGoes.transform.position = Vector3.MoveTowards (ObjectThatGoes.transform.position, new Vector3(Pos.position.x,transform.position.y,Pos.position.z) , Time.deltaTime * 5);
		ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, Pos.rotation, Time.deltaTime * 2);
		if (ObjectThatGoes.transform.position == new Vector3(Pos.position.x, transform.position.y, Pos.position.z) && ObjectThatGoes.transform.localScale == new Vector3(1,1,1)) {
			ObjectThatGoes.transform.rotation = Pos.rotation;
			if (ObjectThatGoes.GetComponent<Rigidbody> () != null) {
				ObjectThatGoes.GetComponent<Rigidbody> ().isKinematic = false;
			}
			if (ObjectThatGoes.GetComponent<HornControl> () != null) {
				ObjectThatGoes.GetComponent<HornControl> ().Going = false;
				ObjectThatGoes.GetComponent<HornControl> ().natela = false;
				Cam.ChecarNaTela ();
			}
            return true;
		} else
        {
            return false;
        }
	}
}
