using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

	bool Reduct;
	[SerializeField] private float speedReduct;
	void Update(){
		if (Reduct) {
			transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (0, 0, 0), speedReduct * Time.deltaTime);
			if (transform.localScale == new Vector3 (0, 0, 0)) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Life")) {
			Reduct = true;
		}
	}
}
