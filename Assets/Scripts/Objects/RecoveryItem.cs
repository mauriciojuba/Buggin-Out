using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : MonoBehaviour {

    public bool RecuperaHP, RecuperaESP;
    public int valorRecuperacao;
	public GameObject Player;
	public GameObject emitter;
	public GameObject model;
	public GameObject effect;
	public float partTime;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1_3D") || other.CompareTag("Player2_3D") || other.CompareTag("Player3_3D") || other.CompareTag("Player4_3D")) {
            if (RecuperaHP)
            {
				if (other.gameObject.GetComponent<Life> () != null) {
					if (other.gameObject.GetComponent<Life> ().LifeQuant + 100 <= other.gameObject.GetComponent<Life> ().MaxLife) {
						SoundManager.PlaySFX ("ColetarVida");

						if (emitter != null) { //desabilita efeitos graficos

							Renderer rend = model.GetComponent<Renderer> (); // remove a emissao
							Material mat = rend.material;
							float emission = 0;
							Color white = Color.white;
							Color attrib = white * Mathf.LinearToGammaSpace (emission);
							mat.SetColor ("_EmissionColor", attrib); // emissao end

							ParticleSystem particleemitter = emitter.GetComponent<ParticleSystem> ();
							if (particleemitter != null) {
								ParticleSystem.EmissionModule emit = particleemitter.emission;
								emit.enabled = false;
							}

							Light lightemitter = emitter.GetComponent<Light> ();
							if (lightemitter != null) {
								lightemitter.enabled = false;
							}
						} //efeitos graficos end

						if (effect != null) { //particula
							partTime = effect.GetComponent<ParticleSystem> ().duration;
							GameObject part = Instantiate (effect, transform.position, Quaternion.identity) as GameObject;
							GameObject.Destroy (part, partTime);
						} //particula end

						Player = other.gameObject;
						GetComponent<LifePos> ().X = Player.GetComponent<Life> ().X + 0.007f;
						GetComponent<LifePos> ().PlayerNumber = Player.GetComponent<Movimentacao3D> ().PlayerNumber;
						GetComponent<LifePos> ().enabled = true;
						GetComponent<LifePos> ().Player = Player;
						transform.SetParent (Player.GetComponent<Movimentacao3D> ().camScreen);
						gameObject.GetComponent<Rigidbody> ().isKinematic = true;
//					Destroy (GetComponent<Collider> ());
//					Destroy (GetComponent<Rigidbody> ());
					}
				}
            }
            if (RecuperaESP)
            {
				Player = other.gameObject;
				transform.SetParent (Player.transform);
				transform.position = transform.parent.position;
				other.GetComponent<UseSpecial> ().SpecialInScreen.Add (gameObject);
				GetComponent<SpecialPos> ().XRef = other.GetComponent<UseSpecial> ();
				GetComponent<SpecialPos> ().PlayerNumber = other.GetComponent<UseSpecial> ().PlayerNumber;
				GetComponent<SpecialPos> ().enabled = true;
				SetCollectedAnimations ();

				Destroy (GetComponent<Collider> ());
				Destroy (GetComponent<Rigidbody> ());
            }
            
        }
    }

	public void SetCollectedAnimations(){
		if(GetComponent<Animator> () != null)
			GetComponent<Animator> ().SetBool ("Collected", true);
	}

	public void ADDSpecial(){
		if (Player.GetComponent<UseSpecial>() != null)
		{
			Player.GetComponent<UseSpecial> ().SpecialItens++;
			Player.GetComponent<UseSpecial> ().UpdateBar ();
		}
	}

	public void PlusLife(){
		Player.gameObject.GetComponent<Life>().LifeQuant += valorRecuperacao;
		Player.gameObject.GetComponent<Life> ().UpdateLife ();
		Destroy (this);
	}
}
