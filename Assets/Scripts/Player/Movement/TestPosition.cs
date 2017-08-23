using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPosition : MonoBehaviour {

	public Transform P1, P2;

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		if (P1 == null) {
			if (GameObject.FindWithTag ("Player1_3D") != null)
				P1 = GameObject.FindWithTag ("Player1_3D").transform;
		}
		if (P2 == null) {
			if (GameObject.FindWithTag ("Player2_3D") != null)
				P2 = GameObject.FindWithTag ("Player2_3D").transform;
		}

		if (P2 != null) {
			//armazena a distancia do eixo X
			var posx = calcDistMid (P1, P2).x;
			//armazena a distancia do eixo Z
			var posz = calcDistMid (P1, P2).z;
			//posiciona o objeto entre os 2 pontos
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (posx, 0, posz), 0.5f);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (P1.position.x, 0, P1.position.z), 0.5f);
		}
	}

	//Calcula a distancia entre 2 pontos.
	Vector3 calcDistMid(Transform ObA, Transform ObB){
		
		//calcula a distancia entre 2 pontos no eixo X, e divide por 2 para achar o meio entre eles.
		float posX = ObA.position.x + (ObB.position.x - ObA.position.x) / 2;
		//calcula a distancia entre 2 pontos no eixo Z, e divide por 2 para achar o meio entre eles.
		float posZ = ObA.position.z + (ObB.position.z - ObA.position.z) / 2;

		//retorna o resultado para a variavel.
		Vector3 Result = new Vector3 (posX, 0, posZ);
		return Result;
	}
}
