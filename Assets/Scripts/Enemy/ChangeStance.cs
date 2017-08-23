using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStance : MonoBehaviour {

	EnemyIA IA3D;
	public GameObject Spriter;
	public GameObject Model3D;
	public bool In3D,ChangePos;


	void Start () {
		IA3D = gameObject.GetComponent<EnemyIA> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (ChangePos) {
			if (In3D) {
				ComponentsHandler_3D (true);
				ComponentsHandler_2D (false);

				Model3D.transform.position = new Vector3 (Spriter.transform.position.x, Model3D.transform.position.y, Spriter.transform.position.z);
			} else {
				ComponentsHandler_3D (false);
				ComponentsHandler_2D (true);

				Spriter.transform.position = new Vector3 (Model3D.transform.position.x, 0, Model3D.transform.position.z);
			}
			ChangePos = false;
		}
	}


	//Desativa/Ativa o codigo de IA 3D.
	//Desativa/Ativa as Renders do 3D, o collider e a gravidade.
	void ComponentsHandler_3D(bool active)
	{
		if(active == false)
			Model3D.GetComponent<Rigidbody>().velocity = transform.forward * 0;

		IA3D.enabled = active;
		Model3D.GetComponent<MeshRenderer>().enabled = active;
		Model3D.GetComponent<SphereCollider>().enabled = active;
		Model3D.GetComponent<Rigidbody>().useGravity = active;
	}
		
	//Desativa/Ativa a Sprite 2D
	void ComponentsHandler_2D(bool active)
	{
		//Move2D_P2.enabled = active;
		Spriter.GetComponent<SpriteRenderer>().enabled = active;
	}

	public void ChangeS(){
			In3D = !In3D;
			ChangePos = true;
	}
}
