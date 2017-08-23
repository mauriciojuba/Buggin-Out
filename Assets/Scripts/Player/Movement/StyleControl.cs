using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleControl : MonoBehaviour {

	[Header("PLAYER 1 CONFIGS")]
	private Movimentacao3D Move3D_P1;
	private Movimentacao2D Move2D_P1;
	private GameObject Player3D_P1;
	private GameObject Player2D_P1;
	public bool In3D_P1;
	[Space(20)]
	[Header("PLAYER 2 CONFIGS")]
	private Movimentacao3D Move3D_P2;
	private Movimentacao2D Move2D_P2;
	private GameObject Player3D_P2;
	private GameObject Player2D_P2;
	public bool In3D_P2;

	void Start(){
		if (GameObject.FindWithTag ("Player1_3D") != null) {
			Player3D_P1 = GameObject.FindWithTag ("Player1_3D");
			Move3D_P1 = Player3D_P1.GetComponent<Movimentacao3D> ();
		}
		if (GameObject.FindWithTag ("Player2_3D") != null) {
			Player3D_P2 = GameObject.FindWithTag ("Player2_3D");
			Move3D_P2 = Player3D_P2.GetComponent<Movimentacao3D> ();
		}
		if (GameObject.FindWithTag ("Player1_2D") != null) {
			Player2D_P1 = GameObject.FindWithTag ("Player1_2D");
			Move2D_P1 = Player2D_P1.GetComponent<Movimentacao2D> ();
		}
		if (GameObject.FindWithTag ("Player2_2D") != null) {
			Player2D_P2 = GameObject.FindWithTag ("Player2_2D");
			Move2D_P2 = Player2D_P2.GetComponent<Movimentacao2D> ();
		}

	}


	void Update () {
		if (Input.GetButtonDown ("B P1")) {
			ChangeStyle (1);
		}
		if (Input.GetButtonDown ("B P2")) {
			ChangeStyle (2);
		}
    }

	void ChangeStyle(int P){
		//Muda o estilo de 3D pra 2D e vice-versa
		if (P == 1) {
			In3D_P1 = !In3D_P1;

			//Quando esta em 3D, desativa o 2D.
			if (In3D_P1) {
				ComponentsHandler_3DP1 (true);
				ComponentsHandler_2DP1 (false);
            
				//Mantem o personagem 3D na mesma posição visual que o personagem 2D está.
				Player3D_P1.transform.position = new Vector3 (Player2D_P1.transform.position.x, Player3D_P1.transform.position.y, Player2D_P1.transform.position.z);
			} else {
				ComponentsHandler_3DP1 (false);
				ComponentsHandler_2DP1 (true);

				//Mantem o personagem 2D na mesma posição visual que o personagem 3D está.
				Player2D_P1.transform.position = new Vector3 (Player3D_P1.transform.position.x,0, Player3D_P1.transform.position.z);
			}
		}
		else if (P == 2) {
			In3D_P2 = !In3D_P2;

			//Quando esta em 3D, desativa o 2D.
			if (In3D_P2) {
				ComponentsHandler_3DP2 (true);
				ComponentsHandler_2DP2 (false);

				//Mantem o personagem 3D na mesma posição visual que o personagem 2D está.
				Player3D_P2.transform.position = new Vector3 (Player2D_P2.transform.position.x, Player3D_P2.transform.position.y, Player2D_P2.transform.position.z);
			} else {
				ComponentsHandler_3DP2 (false);
				ComponentsHandler_2DP2 (true);

				//Mantem o personagem 2D na mesma posição visual que o personagem 3D está.
				Player2D_P2.transform.position = new Vector3 (Player3D_P2.transform.position.x,0, Player3D_P2.transform.position.z);
			}
		}
	}

    //Desativa/Ativa o codigo de movimentação 3D.
    //Desativa/Ativa as Renders do 3D, o collider e a gravidade.
    void ComponentsHandler_3DP1(bool active)
    {
		if(active == false)
			Player3D_P1.GetComponent<Rigidbody>().velocity = transform.forward * 0;
		
        Move3D_P1.enabled = active;
        Player3D_P1.GetComponent<MeshRenderer>().enabled = active;
        Player3D_P1.GetComponent<CapsuleCollider>().enabled = active;
        Player3D_P1.GetComponent<Rigidbody>().useGravity = active;
    }

    //Destiva/Ativa o codigo de movimentação 2D.
    //Desativa/Ativa a Sprite 2D
    void ComponentsHandler_2DP1(bool active)
	{
        Move2D_P1.enabled = active;
        Player2D_P1.GetComponent<SpriteRenderer>().enabled = active;
    }

	//Desativa/Ativa o codigo de movimentação 3D.
	//Desativa/Ativa as Renders do 3D, o collider e a gravidade.
	void ComponentsHandler_3DP2(bool active)
	{
		if(active == false)
			Player3D_P2.GetComponent<Rigidbody>().velocity = transform.forward * 0;

		Move3D_P2.enabled = active;
		Player3D_P2.GetComponent<MeshRenderer>().enabled = active;
		Player3D_P2.GetComponent<CapsuleCollider>().enabled = active;
		Player3D_P2.GetComponent<Rigidbody>().useGravity = active;
	}

	//Destiva/Ativa o codigo de movimentação 2D.
	//Desativa/Ativa a Sprite 2D
	void ComponentsHandler_2DP2(bool active)
	{
		Move2D_P2.enabled = active;
		Player2D_P2.GetComponent<SpriteRenderer>().enabled = active;
	}
}
