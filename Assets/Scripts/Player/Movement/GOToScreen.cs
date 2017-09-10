using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOToScreen : MonoBehaviour {


	public void GoToScreen(Transform telapos, GameObject ObjectToGo){
		ObjectToGo.transform.localScale = Vector3.MoveTowards (ObjectToGo.transform.localScale, telapos.localScale, Time.deltaTime);
		ObjectToGo.transform.position = Vector3.MoveTowards (ObjectToGo.transform.position, telapos.position, Time.deltaTime * 10);
		ObjectToGo.transform.rotation = Quaternion.Lerp (ObjectToGo.transform.rotation, telapos.rotation, Time.deltaTime);
	}
}
