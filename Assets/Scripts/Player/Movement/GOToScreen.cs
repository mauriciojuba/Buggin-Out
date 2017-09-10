using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOToScreen : MonoBehaviour {

	public CameraControl Cam;

	public void GoToScreen(Transform telapos, GameObject ObjectThatGoes){
		ObjectThatGoes.transform.localScale = Vector3.MoveTowards (ObjectThatGoes.transform.localScale, telapos.localScale, Time.deltaTime);
		ObjectThatGoes.transform.position = Vector3.MoveTowards (ObjectThatGoes.transform.position, telapos.position, Time.deltaTime * 5);
		ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, telapos.rotation, Time.deltaTime * 2);

		if (ObjectThatGoes.transform.position == telapos.position && ObjectThatGoes.transform.localScale == telapos.localScale) {
			ObjectThatGoes.transform.rotation = telapos.rotation;
			if (ObjectThatGoes.GetComponent<HornControl> () != null) {
				ObjectThatGoes.GetComponent<HornControl> ().Going = false;
				ObjectThatGoes.GetComponent<HornControl> ().natela = true;
				Cam.ChecarNaTela ();
			}
		}
	}

	public void GoOffScreen(Transform Pos, GameObject ObjectThatGoes){
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

		}
	}
}
