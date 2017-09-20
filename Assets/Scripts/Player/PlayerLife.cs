using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {

	public float MaxLife, LifeAtual;
	float maskOver;
	public Animator Mask;


	void Start () {
		MaxLife = 300f;
		LifeAtual = MaxLife;
	}
	
	// Update is called once per frame
	void Update () {
		atualizaVida ();
	}

	void atualizaVida(){
		Mask.SetFloat ("Life", (LifeAtual / MaxLife));
	}
}
