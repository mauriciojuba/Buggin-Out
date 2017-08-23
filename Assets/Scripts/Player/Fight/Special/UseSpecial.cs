using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSpecial : MonoBehaviour {

	public Transform ScreenGlass;

	public SpecialSkill SpecialRef;
	public int PlayerNumber;
	public bool Use;
	Vector3 InitPos;
	public Rigidbody RB;

	public int SpecialItens;
	public List<GameObject> SpecialInScreen;

	[SerializeField] private PickThrow CheckGrab;
	[SerializeField] private float forwardX,forwardY,forwardZ;
	[SerializeField] private bool AnimatorControl;

	void Start(){
		PlayerNumber = GetComponent<PlayerNumb> ().PlayerNumber;
		RB = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (SpecialRef == null) {
			return;
		}

		forwardX = transform.forward.x;
		forwardY = transform.forward.y;
		forwardZ = transform.forward.z;
//		if (!GetComponent<Movimentacao3D> ().onScreen && GetComponent<Movimentacao3D> ().InGround && !CheckGrab.Grabbing) {
//			if (Input.GetButtonDown (SpecialRef.ButtonToUse + PlayerNumber) && !Use && SpecialItens > 0) {
//				if (SpecialRef.Rush) {
//					Use = true;
//					InitPos = transform.position;
//				} else {
//					gameObject.GetComponent<Movimentacao3D> ().SetSpecialAnim ();
//				}
//				gameObject.GetComponent<Movimentacao3D> ().CanMove = false;
//				SpecialItens--;
//				SpecialInScreen [SpecialInScreen.Count - 1].GetComponent<SpecialPos> ().GoOffScreen = true;
//				SpecialInScreen [SpecialInScreen.Count - 1].GetComponent<SpecialPos> ().SetPos = false;
//				SpecialInScreen.Remove (SpecialInScreen [SpecialInScreen.Count - 1]);
//				Vector3 v3 = RB.velocity;
//				v3.x = 0;
//				v3.z = 0;
//				RB.velocity = v3;
//			}
//		}
		if (Use)
			UseTheSpecial ();
	}

	public void UpdateBar(){
		
	}

	public void UseTheSpecial(){
		if (SpecialRef.Rush) {
			RB.AddForce (transform.forward * SpecialRef.RushForce);
			if (Vector3.Distance (transform.position, InitPos) > SpecialRef.RushDistance) {
				RB.velocity = Vector3.zero;
				gameObject.GetComponent<Movimentacao3D> ().CanMove = true;
				Use = false;
			}
		} else if (SpecialRef.Magic) {
			switch (SpecialRef.MyStyle) {
			case SpecialSkill.MagicStyle.AOE:
				GameObject obj = GameObject.Instantiate (SpecialRef.MagicPrefab, transform.position, transform.rotation) as GameObject;
				Special SP = obj.GetComponent<Special> ();
				obj.GetComponent<Rigidbody> ().AddForce (obj.transform.forward * SpecialRef.AOEForce);
				SP.Damage = SpecialRef.AOEDamage;
				SP.Distance = SpecialRef.AOEDistance;
				SP.PushForce = SpecialRef.AOEPushForce;
				SP.Pull = SpecialRef.Pull;
				SP.Push = SpecialRef.Push;
				SP.InitPos = obj.transform.position;
				if(!AnimatorControl)
					//gameObject.GetComponent<Movimentacao3D> ().CanMove = true;
				Use = false;
				break;
			}
		}
	}

//	void OnCollisionEnter(Collision Col){
//		if (SpecialRef.Rush && Use) {
//			foreach (ContactPoint contact in Col.contacts) {
//				if (Col.collider.CompareTag ("Enemy")) {
//					Rigidbody RBE;
//					RBE = Col.collider.gameObject.GetComponent<Rigidbody> ();
//					if (RBE.mass < RB.mass) {
//						RBE.AddExplosionForce (SpecialRef.RushPushForce, Col.collider.transform.position, 10.0f, 3.0f);
//						//aplicar o dano no collider.
//					} else {
//						RB.velocity = Vector3.zero;
//						gameObject.GetComponent<Movimentacao3D> ().CanMove = true;
//						Use = false;
//					}
//				}
//			}
//		}
//	}
}
