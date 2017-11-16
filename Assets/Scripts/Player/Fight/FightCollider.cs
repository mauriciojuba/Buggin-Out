using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCollider : MonoBehaviour {

	public float Damage;
	public GameObject particula;
	public string CharacterName;
	public GameObject Player;
	public GameObject EnemyHit;
	public string Style;
	[SerializeField] private bool Horn,Liz,Juan;


	void OnTriggerEnter (Collider col){
		//aplicar a função de causar dano.
		if (Player != null) {
			if (Player.CompareTag ("Player1_3D") || Player.CompareTag ("Player2_3D") ||
				Player.CompareTag ("Player3_3D") || Player.CompareTag ("Player4_3D")) {
				if (col.tag != "Player1_3D" && col.tag != "Player2_3D" && col.tag != "Player3_3D" && col.tag != "Player4_3D") {
					if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Destructible") || col.CompareTag("Box")) {
						//Aqui deve ser chamado o método(função) que substituirá o Update do script Life.
						EnemyHit = col.gameObject;
						//Aplica o Dano.
						ApplyDamage ();

						//Som de Ataque
						PlaySound(Style);

						//coloca particula de Ataque
						InstantiateParticle();

						//ativa a animação do inimigo
						ApplyEnemyHitAnim ();


					}
				}
			}
		} else {

			//coloca particula de Ataque
			InstantiateParticle ();

			if (col.gameObject.GetComponent<PlayerLife> () != null) {
				EnemyHit = col.gameObject;

				//Aplica o Dano.
				ApplyDamage ();

				if (col.gameObject.GetComponent<PlayerLife> ().LifeAtual >= 0) {
					if (col.CompareTag ("Player1_3D") || col.CompareTag ("Player2_3D") ||
						col.CompareTag ("Player3_3D") || col.CompareTag ("Player4_3D")) {
						col.GetComponent<AnimationControl> ().SetTakeDamageAnim ();
					}
				}
			}
			if(GetComponent<BoxCollider>() != null)
				GetComponent<BoxCollider> ().enabled = false;
		}
	}

    void ApplyDamage()
    {
        if (EnemyHit.gameObject.GetComponent<PlayerLife>() != null)
        {
            EnemyHit.GetComponent<PlayerLife>().LifeAtual -= (int)Damage;
        }
        if (EnemyHit.gameObject.GetComponent<Life>() != null)
        {
            EnemyHit.GetComponent<Life>().LifeQuant -= (int)Damage;

        }
    }

	void ApplyEnemyHitAnim(){
		//ja podemos tirar fsm daqui?
		if (EnemyHit.GetComponent<FSMMosquito> () != null) {
			EnemyHit.GetComponent<FSMMosquito> ().state = FSMMosquito.FSMStates.Damage;
			EnemyHit.GetComponent<FSMMosquito> ().SetTakeDamageAnim ();
		}
		if (EnemyHit.GetComponent<IA_Mosquito> () != null) {
            EnemyHit.GetComponent<IA_Mosquito>().playerStr = Damage;
            EnemyHit.GetComponent<IA_Mosquito>()._anim.SetTrigger("TakeDamage");
            EnemyHit.GetComponent<IA_Mosquito> ().hitted = true;

        }
		if (EnemyHit.GetComponent<IA_Aranha> () != null) {
			EnemyHit.GetComponent<IA_Aranha>().playerStr = Damage;
			EnemyHit.GetComponent<IA_Aranha>()._anim.SetTrigger("TakeDamage");
			EnemyHit.GetComponent<IA_Aranha> ().hitted = true;

		}
		//ja podemos tirar fsm daqui?
		if (EnemyHit.GetComponent<FSMAranha> () != null) {
            EnemyHit.GetComponent<FSMAranha>().Life -= Damage;
			EnemyHit.GetComponent<FSMAranha> ().state = FSMAranha.FSMStates.Damage;
			EnemyHit.GetComponent<FSMAranha> ().SetTakeDamageAnim ();
		}
	}

	void PlaySound(string TypeofAtk){
		if (Horn) {
			if (TypeofAtk == "Strong") {
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Player/Horn/Ataque_Forte_Horn", transform.position);
			} else if (TypeofAtk == "Basic") {
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Player/Horn/Ataque_Basico_Horn", transform.position);
			}
		} else if (Liz) {
			if (TypeofAtk == "Strong") {
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Player/Liz/Ataque_Basico_Liz", transform.position);
			} else if (TypeofAtk == "Basic") {
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Player/Liz/Ataque_Basico_Liz", transform.position);
			}
		} else if (Juan) {
			if (TypeofAtk == "Strong") {
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Player/Horn/Ataque_Forte_Horn", transform.position);
			} else if (TypeofAtk == "Basic") {
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Player/Horn/Ataque_Basico_Horn", transform.position);
			}
		}
	}

	public void InstantiateParticle(){
		if (particula != null)
		{
			//ScreenShake.Instance.Shake(0.05f, 0.05f);
			GameObject part = Instantiate (particula, transform.position, Quaternion.identity) as GameObject;
            part.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			float timePart = part.GetComponent<ParticleSystem> ().main.duration;
			if (Player != null) {
				if (Player.GetComponent<HornControl> ().natela) {
					part.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				}
			}
			GameObject.Destroy (part, timePart);
		}
	}
}
