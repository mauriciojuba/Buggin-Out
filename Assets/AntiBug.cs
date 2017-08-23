using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBug : MonoBehaviour {

	[SerializeField] private GameObject[] Enemys;

	void Start () {
		Enemys = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Right Analog Button P1")) {
			for (int i = 0; i < Enemys.Length; i++) {
				if (Enemys [i].GetComponent<FSMMosquito> ().state == FSMMosquito.FSMStates.OnScreen) {
					Enemys [i].GetComponent<FSMMosquito> ().state = FSMMosquito.FSMStates.DrainLife;
				}
			}
		}
	}
}
