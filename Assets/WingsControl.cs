using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsControl : MonoBehaviour {

	public float Rand;

	void Start () {
		Rand = (float)Random.Range (1f, 3f);
		GetComponent<Animator> ().SetFloat ("WingsSpeed", Rand);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
