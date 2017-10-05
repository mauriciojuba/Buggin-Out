using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira : MonoBehaviour {

	[SerializeField] List<GameObject> AreaEnemys;
	[SerializeField] Rigidbody dominoCenterLeft,dominoCenterRight;
	[SerializeField] Vector3 forceDir;
	[SerializeField] List<GameObject> DeadEnemys;
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (AreaEnemys.Count == 0) {
			dominoCenterLeft.AddForce(forceDir*2000f);
			dominoCenterRight.AddForce(-forceDir*2000f);
			this.GetComponent<BoxCollider>().enabled = false;
		}

		for (int i = 0; i < AreaEnemys.Count; i++) {
			if (AreaEnemys [i] == null) {
                AreaEnemys.RemoveAt(i);
    }
		}

	}
}
