using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirTiro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
