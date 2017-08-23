using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Trilha : MonoBehaviour {

    public AudioMixerGroup Mixer,SFXMixer;
	[SerializeField] private AudioSource[] Audio;
	[SerializeField] private List<GameObject> AudioSFX;
	[SerializeField] private int SFXC;
	void Start(){
		Audio = new AudioSource[2];
	}

    public void Update()
    {
		SFXC = AudioSFX.Count;
		for (int i = 0; i < Audio.Length; i++) {
			if (Audio[i] == null) {
				Audio = GetComponents<AudioSource> ();
			}


			if (Audio [i].outputAudioMixerGroup != Mixer) {
				Audio [i].outputAudioMixerGroup = Mixer;
			}
		}

		if(transform.childCount > SFXC){
			GetSFX ();
		}
		for (int i = 0; i < AudioSFX.Count; i++) {
			if (!AudioSFX [i].activeInHierarchy) {
				AudioSFX.Remove (AudioSFX [i]);
			}
			if (AudioSFX.Count != 0) {
				if (AudioSFX [i] == null) {
					AudioSFX.Capacity--;
				}
			}
		}

    }


	void GetSFX(){
		if (transform.childCount > 0) {
			for (int a = 0; a < transform.childCount; a++) {
				if (!AudioSFX.Contains (transform.GetChild (a).gameObject) && transform.GetChild (a).gameObject.activeInHierarchy)
					AudioSFX.Add (transform.GetChild (a).gameObject);
			}
		}
		if (transform.childCount > SFXC) {
			for (int o = 0; o < AudioSFX.Count; o++) {
				if (AudioSFX [o].GetComponent<AudioSource>().outputAudioMixerGroup != SFXMixer) {
					AudioSFX [o].GetComponent<AudioSource>().outputAudioMixerGroup = SFXMixer;
				}
			}
		}
	}

}
