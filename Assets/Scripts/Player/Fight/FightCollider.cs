using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCollider : MonoBehaviour {

	public float Damage;
    public GameObject particula;
	public string CharacterName;
	public GameObject Player;
    


	void OnTriggerEnter (Collider col){
		//aplicar a função de causar dano.
		if (Player != null) {
			if (Player.CompareTag ("Player1_3D") || Player.CompareTag ("Player2_3D") ||
			    Player.CompareTag ("Player3_3D") || Player.CompareTag ("Player4_3D")) {
				if (col.tag != "Player1_3D" && col.tag != "Player2_3D" && col.tag != "Player3_3D" && col.tag != "Player4_3D") {
					if (col.gameObject.GetComponent<Life> () != null) {
						//Aqui deve ser chamado o método(função) que substituirá o Update do script Life.
						col.gameObject.GetComponent<Life> ().LifeQuant -= (int)Damage;

                        //Som de Ataque do Horn
                        if (CharacterName == "Horn")
							SoundManager.PlaySFX (gameObject, "Horn_Atk_02");

						if (CharacterName == "Liz")
							SoundManager.PlaySFX (gameObject, "Liz_Atk-02");

						//coloca particula de Ataque
						if (particula != null)
                        {
                            ScreenShake.Instance.Shake(0.05f, 0.05f);
                            GameObject part = Instantiate (particula, transform.position, Quaternion.identity) as GameObject;
							float timePart = part.GetComponent<ParticleSystem> ().duration;
							if (Player.GetComponent<Movimentacao3D> ().onScreen) {
								part.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
							}
							GameObject.Destroy (part, timePart);

                            

                        }

                       

                        col.gameObject.GetComponent<Life> ().UpdateLife ();
						if (col.gameObject.GetComponent<FSMMosquito> () != null) {
							col.gameObject.GetComponent<FSMMosquito> ().state = FSMMosquito.FSMStates.Damage;
							col.gameObject.GetComponent<FSMMosquito> ().SetTakeDamageAnim ();
						}
                        if (col.gameObject.GetComponent<FSMAranha> () != null) {
							col.gameObject.GetComponent<FSMAranha> ().state = FSMAranha.FSMStates.Damage;
							col.gameObject.GetComponent<FSMAranha> ().SetTakeDamageAnim ();
						}

					}
				}
			}
		} else {
			
			if (col.gameObject.GetComponent<Life> () != null) {
				if (particula != null) {
                    ScreenShake.Instance.Shake(0.05f, 0.05f);
                    GameObject part = Instantiate (particula, transform.position, Quaternion.identity) as GameObject;
					float timePart = part.GetComponent<ParticleSystem> ().duration;
					GameObject.Destroy (part, timePart);

				}
				if (col.gameObject.GetComponent<Life> ().LifeOF == Life.LifeType.Player) {
					if (col.gameObject.GetComponent<Life> ().LifeQuant >= col.gameObject.GetComponent<Life> ().Division) {
						if (col.CompareTag ("Player1_3D") || col.CompareTag ("Player2_3D") ||
							col.CompareTag ("Player3_3D") || col.CompareTag ("Player4_3D")) {
							col.GetComponent<Movimentacao3D> ().SetTakeDamageAnim ();
						}
						col.gameObject.GetComponent<Life> ().LifeQuant -= (int)Damage;
						col.gameObject.GetComponent<Life> ().ListOfImg [col.gameObject.GetComponent<Life> ().QuantImgInScene - 1].GetComponent<ScaleLife> ().TatuLife -= (int)Damage;
						col.gameObject.GetComponent<Life> ().ListOfImg [col.gameObject.GetComponent<Life> ().QuantImgInScene - 1].GetComponent<ScaleLife> ().UpdateScaleLife ();
						col.gameObject.GetComponent<Life> ().UpdateLife ();
					}
				}
			
			}
            if(GetComponent<BoxCollider>() != null)
			GetComponent<BoxCollider> ().enabled = false;
		}
    }
}
