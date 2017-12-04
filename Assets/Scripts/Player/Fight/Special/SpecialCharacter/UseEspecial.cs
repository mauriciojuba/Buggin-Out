using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEspecial : MonoBehaviour {

    public bool Horn, Liz;
    [SerializeField] bool UseForce;
    [SerializeField] float TimeToStop;
    [SerializeField] float TimeToReduce;
    [SerializeField] float Force;
    [SerializeField] GameObject SpecialCollider;
    [SerializeField] GameObject SpecialEffect;

    float Timer;
    void Start () {
		
	}
	
	void Update () {
		
	}


    public bool UsingSpecial()
    {
        if(Horn)
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Horn/Falas/Uiltimate_Horn", transform.position);
        else
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Liz/Falas/Ultimate_Liz", transform.position);

        Timer += Time.deltaTime;
        if (UseForce)
        {
            ParticleSystem particleemitter = SpecialEffect.GetComponent<ParticleSystem>();
            if (particleemitter != null)
            {
                ParticleSystem.EmissionModule emit = particleemitter.emission;
                emit.enabled = true;
                if (particleemitter.isStopped)
                {
                    particleemitter.Play(true);
                }
            }
            SpecialCollider.SetActive(true);
            if (Timer <= 3)
            {
                Force += Time.deltaTime * 5;
            }
            if (Timer >= TimeToReduce && Force > 0)
            {
                Force -= Time.deltaTime * 7;
            }
            if (Force < 0)
            {
                if (particleemitter != null)
                {
                    ParticleSystem.EmissionModule emit = particleemitter.emission;
                    emit.enabled = false;
                    StopParticle();
                }
                SpecialCollider.SetActive(false);
                Force = 0;
                Timer = 0;
                return false;
            }
        }
        if(Timer >= TimeToStop)
        {
            Timer = 0;
            return false;
        }else
        {
            return true;
        }
    }

    public void SpecialHorn()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.VelocityChange);
    }

  

    public void StopParticle()
    {
        ParticleSystem particleemitter = SpecialEffect.GetComponent<ParticleSystem>();
        if (particleemitter.isPlaying)
        {
            particleemitter.Stop(true);
        }
    }
}
