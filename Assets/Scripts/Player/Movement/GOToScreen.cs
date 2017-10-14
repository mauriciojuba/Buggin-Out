using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOToScreen : MonoBehaviour {

	public CameraControl Cam;
    public Transform CamTransform;
	public Transform telapos;
    float time=0;

	void Start(){
	}

	public bool GoToScreen(GameObject ObjectThatGoes, float Range){
		float offset = Range;
		Vector3 randomTelaPos = new Vector3(telapos.position.x+offset*10f,
			telapos.position.y+offset,
			telapos.position.z+offset);
		CameraControl.someObjGoingToScreen = true;
		ObjectThatGoes.transform.localScale = Vector3.Lerp (ObjectThatGoes.transform.localScale, telapos.localScale, time);
		ObjectThatGoes.transform.position = Vector3.Lerp (ObjectThatGoes.transform.position, randomTelaPos,time);
		ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, telapos.rotation, time);
		time += Time.deltaTime*0.1f;

		if (Mathf.Abs(ObjectThatGoes.transform.position.y) >= Mathf.Abs(randomTelaPos.y) - 0.01f &&
			Mathf.Abs(ObjectThatGoes.transform.position.y) <= Mathf.Abs(randomTelaPos.y) + 0.01f &&
			ObjectThatGoes.transform.localScale == telapos.localScale) {
			ObjectThatGoes.transform.rotation = telapos.rotation;
			ObjectThatGoes.transform.parent = CamTransform;
			CameraControl.someObjGoingToScreen = false;
			return true;
		} else {
			return false;
		}
	}

	public bool GoToScreen(GameObject ObjectThatGoes, Vector3 tamanhoNaTela, float Range){
		float offset = Range;
		Vector3 randomTelaPos = new Vector3(telapos.position.x+offset,
											telapos.position.y+offset,
											telapos.position.z+offset);
		CameraControl.someObjGoingToScreen = true;
		ObjectThatGoes.transform.localScale = Vector3.Lerp (ObjectThatGoes.transform.localScale, tamanhoNaTela, time);
		ObjectThatGoes.transform.position = Vector3.Lerp (ObjectThatGoes.transform.position, randomTelaPos,time);
		ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, telapos.rotation, time);
		time += Time.deltaTime*0.1f;

		if (Mathf.Abs(ObjectThatGoes.transform.position.y) >= Mathf.Abs(randomTelaPos.y) - 0.01f &&
			Mathf.Abs(ObjectThatGoes.transform.position.y) <= Mathf.Abs(randomTelaPos.y) + 0.01f &&
            ObjectThatGoes.transform.localScale == telapos.localScale) {
			ObjectThatGoes.transform.rotation = telapos.rotation;
			ObjectThatGoes.transform.parent = CamTransform;
			CameraControl.someObjGoingToScreen = false;
			return true;
		} else {
			return false;
		}
	}

	public bool GoOffScreen(Transform Pos, GameObject ObjectThatGoes){
        time = 0;
        ObjectThatGoes.transform.parent = null;
		ObjectThatGoes.transform.localScale = Vector3.MoveTowards (ObjectThatGoes.transform.localScale, new Vector3(1,1,1), Time.deltaTime);
		ObjectThatGoes.transform.position = Vector3.MoveTowards (ObjectThatGoes.transform.position, new Vector3(Pos.position.x, ObjectThatGoes.transform.position.y,Pos.position.z) , Time.deltaTime * 5);
		ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, Pos.rotation, Time.deltaTime * 2);
		if (ObjectThatGoes.transform.position == new Vector3(Pos.position.x, ObjectThatGoes.transform.position.y, Pos.position.z) && ObjectThatGoes.transform.localScale == new Vector3(1,1,1)) {
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
	public bool GoOffScreen(Transform Pos, GameObject ObjectThatGoes, Transform parent){
		time = 0;
		ObjectThatGoes.transform.parent = parent;
		ObjectThatGoes.transform.localScale = Vector3.MoveTowards (ObjectThatGoes.transform.localScale, new Vector3(1,1,1), Time.deltaTime);
		ObjectThatGoes.transform.position = Vector3.MoveTowards (ObjectThatGoes.transform.position, new Vector3(Pos.position.x, ObjectThatGoes.transform.position.y,Pos.position.z) , Time.deltaTime * 5);
		ObjectThatGoes.transform.rotation = Quaternion.Lerp (ObjectThatGoes.transform.rotation, Pos.rotation, Time.deltaTime * 2);
		if (ObjectThatGoes.transform.position == new Vector3(Pos.position.x, ObjectThatGoes.transform.position.y, Pos.position.z) && ObjectThatGoes.transform.localScale == new Vector3(1,1,1)) {
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
