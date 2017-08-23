using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float TimeToDestruct;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, TimeToDestruct);
	}
}
