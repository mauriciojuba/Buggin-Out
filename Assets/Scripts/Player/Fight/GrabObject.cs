using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

	[SerializeField] float MinDist;
	[SerializeField] Transform PointPick;
	[SerializeField] float Force;
	GameObject BoxProximity;
	bool PickUp, PickedObj;
	GameObject[] Boxs;
	AnimationControl AnimCTRL;
	Quaternion targetRotation;

	void Start () {
		Boxs = GameObject.FindGameObjectsWithTag ("Box");
		AnimCTRL = gameObject.GetComponent<AnimationControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("B P1") && !PickUp && !PickedObj) {
			CheckAllDistance ();
			CheckDistance ();
		}

		if (Input.GetButtonDown ("B P1") && PickedObj) {
			AnimCTRL.ThrowObjAnim ();
		}

		if (PickUp) {
			RotateToObject ();
		}
	}


	void CheckAllDistance(){
		Boxs = GameObject.FindGameObjectsWithTag ("Box");
		if (Boxs.Length > 0) {
			for (int i = 0; i < Boxs.Length; i++) {
				if (BoxProximity == null) {
					BoxProximity = Boxs [i];
				}
				if (Vector3.Distance (transform.position, Boxs [i].transform.position) < Vector3.Distance (transform.position, BoxProximity.transform.position)) {
					BoxProximity = Boxs [i];
				}
			}
		} else
			BoxProximity = null;
	}

	void CheckDistance(){
		if (BoxProximity != null) {
			if (Vector3.Distance (transform.position, BoxProximity.transform.position) < MinDist) {
				targetRotation = Quaternion.LookRotation (new Vector3 (BoxProximity.transform.position.x, BoxProximity.transform.position.y - 0.668f, BoxProximity.transform.position.z) - transform.position);
				PickUp = true;
				Debug.LogWarning ("Pode Pegar");
			}
		}
	}

	void RotateToObject(){
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
		if(Mathf.Abs(transform.rotation.y) >= Mathf.Abs(targetRotation.y) - 0.001f && Mathf.Abs(transform.rotation.y) <= Mathf.Abs(targetRotation.y) + 0.001f) {
			PickUp = false;
			PickedObj = true;
			//Ativa Animação de pegarObjeto
			AnimCTRL.PickObjAnimation();
		}
	}

	void PickUpObj(){
		BoxProximity.GetComponent<Rigidbody> ().isKinematic = true;
		BoxProximity.GetComponent<Collider> ().isTrigger = true;
		BoxProximity.transform.position = PointPick.position;
		BoxProximity.transform.SetParent (PointPick);
	}

	void ThrowObj(){
		PickedObj = false;
		BoxProximity.transform.SetParent (null);
		StartCoroutine (BoxProximity.GetComponent<DestruirObjeto> ().ActiveCol ());
		BoxProximity.GetComponent<Rigidbody> ().isKinematic = false;
		BoxProximity.GetComponent<Rigidbody> ().AddForce ((transform.forward + transform.up) * Force);
		BoxProximity.GetComponent<DestruirObjeto> ().Throwed = true;
	}
}
