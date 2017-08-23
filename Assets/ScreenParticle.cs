using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenParticle : MonoBehaviour {

	[SerializeField] private GameObject Partic;
	[SerializeField] private Transform LeftFoot, RightFoot, LeftArm, RightArm;
	void Start(){
	}

	void EmitParticleLeftFoot(){
		GameObject GB =	GameObject.Instantiate (Partic, LeftFoot.position, LeftFoot.rotation) as GameObject;
		GB.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB, 6);
	}

	void EmitParticleRightFoot(){
		GameObject GB =	GameObject.Instantiate (Partic, RightFoot.position, RightFoot.rotation) as GameObject;
		GB.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB, 6);
	}

	void EmitParticleLeftArm(){
		GameObject GB =	GameObject.Instantiate (Partic, LeftArm.position, LeftArm.rotation) as GameObject;
		GB.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB, 6);
	}

	void EmitParticleRightArm(){
		GameObject GB =	GameObject.Instantiate (Partic, RightArm.position, RightArm.rotation) as GameObject;
		GB.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB, 6);
	}

	void EmitAllParticle(){
		GameObject GB =	GameObject.Instantiate (Partic, RightArm.position , RightArm.rotation) as GameObject;
		GB.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB, 6);
		GameObject GB2 = GameObject.Instantiate (Partic, LeftArm.position , LeftArm.rotation) as GameObject;
		GB2.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB2, 6);
		GameObject GB3 = GameObject.Instantiate (Partic, RightFoot.position , RightFoot.rotation) as GameObject;
		GB3.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB3, 6);
		GameObject GB4 = GameObject.Instantiate (Partic, LeftFoot.position , LeftFoot.rotation) as GameObject;
		GB4.transform.SetParent (transform.parent.GetComponent<Movimentacao3D> ().camScreen);
		Destroy (GB4, 6);

	}

}
