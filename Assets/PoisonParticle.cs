using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonParticle : MonoBehaviour {

    ParticleSystem particleemitter;
    void Start () {
       particleemitter = GetComponent<ParticleSystem>();
    }
	
	public void Activate()
    {
        ParticleSystem.EmissionModule emit = particleemitter.emission;
        emit.enabled = true;
        if (particleemitter.isStopped)
        {
            particleemitter.Play(true);
        }
    }
}
