using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour {
	[HideInInspector]
	public bool Pull,Push;
	[HideInInspector]
	public float Damage,PushForce;
	[HideInInspector]
	public Vector3 InitPos;
	[HideInInspector]
	public float Distance;

	[SerializeField] private Transform PosToPull;

	Rigidbody RB;
	void Start(){
		RB = GetComponent<Rigidbody> ();
	}
	void Update(){
		if (Vector3.Distance (transform.position, InitPos) > Distance) {
			RB.velocity = Vector3.zero;
		}
	}

	void OnTriggerStay(Collider Col){
		if (Col.CompareTag ("Enemy") || Col.CompareTag ("Interactable")) {
			if (Pull) {
				if (Col.GetComponent<FSMMosquito> () != null) {
					Col.GetComponent<FSMMosquito> ().state = FSMMosquito.FSMStates.Damage;
					Col.GetComponent<FSMMosquito> ().SetTakeDamageAnim ();
					Col.GetComponent<FSMMosquito> ().Life -= Time.deltaTime * Damage;
				}
				if (Col.GetComponent<Life> () != null) {
					Col.GetComponent<Life> ().LifeQuant -= Time.deltaTime * Damage;
				}
				Col.GetComponent<Rigidbody> ().AddExplosionForce (-PushForce, PosToPull.position, gameObject.GetComponent<SphereCollider> ().radius, 3.0f, ForceMode.VelocityChange);
				//aplicar dano de tempo em tempo
			} else if (Push) {
				Col.GetComponent<Rigidbody> ().AddExplosionForce (PushForce, PosToPull.position, gameObject.GetComponent<SphereCollider> ().radius, 3.0f, ForceMode.VelocityChange);
				//aplicar dano de tempo em tempo
			} else {
				//aplicar dano
				Destroy(gameObject);
			}
		}
	}
}
