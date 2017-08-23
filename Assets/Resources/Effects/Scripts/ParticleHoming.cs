using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHoming : MonoBehaviour {

	ParticleSystem emitter;
	public Transform target;
	ParticleSystem.Particle[] particles;
	float speed;
	public float multiplier = 1.0f;


	void Start () {
		emitter = gameObject.GetComponent<ParticleSystem>();
		speed = emitter.main.startSpeed.constant;
		if (particles == null || particles.Length < emitter.maxParticles) {
			particles = new ParticleSystem.Particle[emitter.maxParticles];
		}
	}
	

	void LateUpdate () {
		int particlesAlive = emitter.GetParticles (particles);

		for(int i = 0; i < particlesAlive ; i++)
		{
			particles[i].position = Vector3.Slerp (particles[i].position, target.position, Time.deltaTime*speed*multiplier);
		}

		emitter.SetParticles(particles,particlesAlive);
	}
}
