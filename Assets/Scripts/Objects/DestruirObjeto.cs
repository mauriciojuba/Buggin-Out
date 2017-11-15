﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjeto : MonoBehaviour {

	public bool Throwed;
	[SerializeField] private float Damage = 50;

	void OnCollisionEnter(Collision hit){
       
		if (hit.gameObject.CompareTag ("Roomba")) {
			if (this.GetComponent<Life> () != null) {
				this.GetComponent<Life> ().LifeQuant = 0;
			}
		} else if (Throwed) {
			if (this.GetComponent<Life> () != null) {
                this.GetComponent<Life>().Contact = hit.contacts[0].point;
                if (this.GetComponent<Life>().Poison)
                {
                    if (hit.gameObject.CompareTag("Chão"))
                    {
                        this.GetComponent<Life>().LifeQuant = 0;
                        gameObject.GetComponent<Collider>().enabled = false;
                    }
                }
                else
                {
                    this.GetComponent<Life>().LifeQuant = 0;
                    gameObject.GetComponent<Collider>().enabled = false;
                }
			}
			if (hit.gameObject.tag != "Player1_3D" && hit.gameObject.tag != "Player2_3D") {
				if (hit.gameObject.GetComponent<Life> () != null) {
					hit.gameObject.GetComponent<Life> ().LifeQuant -= Damage;
					hit.gameObject.GetComponent<Life> ().UpdateLife ();
				}
			}
		
		}
	}

	public IEnumerator ActiveCol(){
		yield return new WaitForSeconds (0.1f);
		gameObject.GetComponent<Collider> ().isTrigger = false;

	}
}
