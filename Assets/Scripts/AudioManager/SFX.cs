using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour {


    public string Objeto;
    public AudioMixerGroup Mixer;
	[SerializeField] private AudioSource Audio;
	public GameObject luz;

    public void PlaySoundSFX(string Name)
    {
        SoundManager.PlaySFX(gameObject, Name);
		Audio = GetComponent<AudioSource> ();
		Audio.outputAudioMixerGroup = Mixer;
    }

    public void AplicaMixer()
    {
		if (GetComponent<AudioSource>() != null) {
			Audio = GetComponent<AudioSource> ();
			Audio.outputAudioMixerGroup = Mixer;
		}
    }

    public void PlaySoundSfxGrupo(string Grupo)
    {
        SoundManager.PlaySFX(SoundManager.LoadFromGroup(Grupo));
        AplicaMixer();
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.name == "Chao")
        {
            if (Objeto == "Metal")
                SoundManager.PlaySFX(gameObject, SoundManager.LoadFromGroup("Tampinhas"));
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.name == "Chao")
        {
            if(Objeto == "Vidro")
            {
                SoundManager.PlaySFX(gameObject,SoundManager.LoadFromGroup("Objetos quebrando vidro"));
                Audio = GetComponent<AudioSource>();
                Audio.outputAudioMixerGroup = Mixer;
            }

			if (Objeto == "Destruct") {
				if (luz != null) {
					LuzQuebrando lq = luz.GetComponent<LuzQuebrando> ();
					if (lq != null) {
						lq.Quebrou = true;
                        SoundManager.PlaySFX(gameObject, "holofote_falhando_01");
					}
				}
			}
        }

        if (hit.gameObject.name == "Roomba")
        {
            if (Objeto == "Destruct")
            {

				SoundManager.PlayCappedSFX(SoundManager.LoadFromGroup("Objetos Quebrando"), "1");
            }
        }


       

    }
}

