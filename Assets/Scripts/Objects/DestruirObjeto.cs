using System.Collections;
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
				this.GetComponent<Life> ().LifeQuant = 0;
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
		yield return new WaitForSeconds (0.5f);
		gameObject.GetComponent<Collider> ().isTrigger = false;

	}
}
