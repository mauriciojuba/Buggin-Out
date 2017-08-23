using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRB : MonoBehaviour {

	Collider[] objetos;
	public GameObject ParentObject;
	void OnTriggerEnter(Collider hit){
		if(hit.CompareTag("Roomba")){
			objetos = ParentObject.GetComponentsInChildren<Collider>();
			foreach(Collider rb in objetos){
				if(rb.GetComponent<Rigidbody>() == null){
					rb.gameObject.AddComponent<Rigidbody>();
				}
			}
		}
	}
}
