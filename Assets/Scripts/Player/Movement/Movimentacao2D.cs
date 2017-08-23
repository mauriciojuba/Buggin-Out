using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao2D : MonoBehaviour {

	//Define a velocidade de movimento
	public float Speed = 5;

	//GO do personagem que vai ser movimentado.
	public GameObject Spriter;

	//Define o numero do player.
	public int PlayerNumber;

	public Camera CameraMain;

	private Vector3 Movement;

	private Rigidbody Rb;
	void Start () {
		Rb = gameObject.GetComponent<Rigidbody> ();
		CameraMain = Camera.main;
	}

	void FixedUpdate () {
		Movementation ();
		TestPosition ();
	}

	void Movementation() {

		//Define a direção no eixo Y
		float InputY = Input.GetAxisRaw("Vertical P" + PlayerNumber);
		//Define a direção no eixo X
		float InputX = Input.GetAxisRaw ("Horizontal P" + PlayerNumber);

		//Define pra qual lado o sprite vai estar virado
		if(InputX > 0)
			Spriter.GetComponent<SpriteRenderer> ().flipX = false;
		else if (InputX < 0)
			Spriter.GetComponent<SpriteRenderer> ().flipX = true;

		//Registra a direção que o personagem vai se mover
		Movement = new Vector3 (InputX * Speed,0, InputY * Speed);

		//Move o personagem na direção indicada.
		Rb.velocity = Movement;

	}

	//testa a posição do personagem em relação a camera
	void TestPosition(){
		//Calcula a distancia entre o personagem e a camera.
		var DistanceZ = (transform.position - CameraMain.transform.position).z;

		//Calcula o ponto maximo de movimentação pra esquerda.
		var Leftborder = CameraMain.ViewportToWorldPoint (new Vector3 (0.01f, 0, DistanceZ)).x;

		//Calcula o ponto maximo de movimentação pra direita.
		var Rightborder = CameraMain.ViewportToWorldPoint (new Vector3 (0.99f, 0, DistanceZ)).x;

		//Calcula o ponto maximo de movimentação pra Baixo.
		var Bottomborder = CameraMain.ViewportToWorldPoint (new Vector3 (0, 0, 7)).z;

		//Calcula o ponto maximo de movimentação pra Cima.
		var Topborder = CameraMain.ViewportToWorldPoint (new Vector3 (0, 0, 40)).z;

		//Mantem o personagem sempre dentro do espaço da camera.
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, Leftborder, Rightborder),
			/*Mathf.Clamp (*/transform.position.y/*, Bottomborder, Topborder)*/,
			Mathf.Clamp (transform.position.z,Bottomborder,Topborder ));

	}
}
