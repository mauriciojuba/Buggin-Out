using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centro : MonoBehaviour {


	public Transform Player;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Mantem As direções ligadas ao player.
		transform.position = Player.transform.position;
	}
}
