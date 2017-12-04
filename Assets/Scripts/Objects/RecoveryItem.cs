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
                if (other.gameObject.GetComponent<PlayerLife>() != null)
                {
                    if (other.gameObject.GetComponent<PlayerLife>().LifeAtual < other.gameObject.GetComponent<PlayerLife>().MaxLife)
                    {
                        if (emitter != null)
                        { //desabilita efeitos graficos
                            Renderer rend = model.GetComponent<Renderer>(); // remove a emissao
                            Material mat = rend.material;
                            float emission = 0;
                            Color white = Color.white;
                            Color attrib = white * Mathf.LinearToGammaSpace(emission);
                            mat.SetColor("_EmissionColor", attrib); // emissao end

                            ParticleSystem particleemitter = emitter.GetComponent<ParticleSystem>();
                            if (particleemitter != null)
                            {
                                ParticleSystem.EmissionModule emit = particleemitter.emission;
                                emit.enabled = false;
                            }

                            Light lightemitter = emitter.GetComponent<Light>();
                            if (lightemitter != null)
                            {
                                lightemitter.enabled = false;
                            }
                        } //efeitos graficos end

                        if (effect != null)
                        { //particula
                            partTime = effect.GetComponent<ParticleSystem>().main.duration;
                            GameObject part = Instantiate(effect, transform.position, Quaternion.identity) as GameObject;
                            GameObject.Destroy(part, partTime);
                        } //particula end
                        Player = other.gameObject;
                        PlusLife();
                        Destroy(GetComponent<Collider>());
                        Destroy(GetComponent<Rigidbody>());
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

        FMODUnity.RuntimeManager.PlayOneShot("event:/Coletaveis/Vida/Int_Vida", transform.position);

        if (Player.gameObject.GetComponent<PlayerLife>().LifeAtual < Player.gameObject.GetComponent<PlayerLife>().MaxLife)
        {
            Player.gameObject.GetComponent<PlayerLife>().LifeAtual += valorRecuperacao;
            Destroy(gameObject);
        }
	}
}
