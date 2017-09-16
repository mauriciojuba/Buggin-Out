using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour {


    [FMODUnity.EventRef]
    public string Evento;

    public string Objeto;
	[SerializeField] private AudioSource Audio;
	public GameObject luz;

    public void PlaySoundSFX(string Name)
    {

        FMODUnity.RuntimeManager.PlayOneShot(Name, transform.position);

    }

    public void PlaySoundSfxGrupo()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Evento, transform.position);
    }

    /* 
     * public void AplicaMixer()
       {
           if (GetComponent<AudioSource>() != null) {
               Audio = GetComponent<AudioSource> ();
               Audio.outputAudioMixerGroup = Mixer;
           }
       }
       */



    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.name == "Chao")
        {
            PlaySoundSfxGrupo();
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.name == "Chao")
        {
            if(Objeto == "Vidro")
            {
                PlaySoundSfxGrupo();
            }

			if (Objeto == "Destruct") {
				if (luz != null) {
					LuzQuebrando lq = luz.GetComponent<LuzQuebrando> ();
					if (lq != null) {
						lq.Quebrou = true;
                        PlaySoundSfxGrupo();
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

