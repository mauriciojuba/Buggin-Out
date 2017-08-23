using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzQuebrando : MonoBehaviour {

	public bool Quebrou;
	bool blinking, end;

    public Light Luz;
	public GameObject lamp;
	public GameObject particle;

	public int longBlink,shortBlink;
	public float longBlinkDuration = 0.1f;
	public float shortBlinkDuration = 0.05f;

	Rigidbody rb;

	void Update(){
		if (Quebrou && !blinking && !end) {
			StartCoroutine (Pisca ());
			if (gameObject.GetComponent<Rigidbody> () != null) {
				rb = gameObject.GetComponent<Rigidbody> ();
				rb.useGravity = true;
			}
			GameObject part = Instantiate (particle, lamp.transform.position, lamp.transform.rotation) as GameObject;
			part.transform.parent = this.gameObject.transform;
			ParticleSystem emit = part.GetComponent<ParticleSystem> ();
			GameObject.Destroy (part, emit.duration);
			blinking = true;
		}
	}

	public IEnumerator LampFade(){
        float counter = 0;
		float duration = (longBlink * ((longBlinkDuration * 2) + longBlinkDuration) + shortBlink * ((shortBlinkDuration * 2) + shortBlinkDuration) + shortBlinkDuration)*15;
		Renderer rend = lamp.GetComponent<Renderer> ();
		Material mat = rend.material;
		Color init = Color.white;
		Color end = Color.white;
		end = init * Mathf.LinearToGammaSpace (0);
		Color current = Color.white;

		while (counter < duration) {
			counter += Time.deltaTime;
			current = Color.Lerp (init, end, counter);
			mat.SetColor ("_EmissionColor", current);
			yield return null;
		}
		yield return null;
	}

    public IEnumerator Pisca()
    {
		for (int i = 0; i < longBlink; i++)
        {
            Luz.enabled = false;

			yield return new WaitForSeconds(longBlinkDuration*2);

            Luz.enabled = true;

			yield return new WaitForSeconds(longBlinkDuration);

            Luz.enabled = false;
        }

		for (int i = 0; i < shortBlink; i++)
        {
            Luz.enabled = false;

			yield return new WaitForSeconds(shortBlinkDuration*2+Random.Range(-shortBlinkDuration,shortBlinkDuration));

            Luz.enabled = true;

			yield return new WaitForSeconds(shortBlinkDuration+Random.Range(-shortBlinkDuration,shortBlinkDuration));

            Luz.enabled = false;
        }
		end = true;
		blinking = false;
		StartCoroutine (LampFade ());
    }
}
