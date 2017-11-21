using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour {

    public Character ThisCharacter;

    [FMODUnity.EventRef]
    public string Evento_Liz;
    [FMODUnity.EventRef]
    public string Evento_Horn;

    [SerializeField] bool Liz, Horn;

    public void Sound()
    {
        if (Liz)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Evento_Liz, transform.position);
        }else if (Horn)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Evento_Horn, transform.position);
        }
    }

}
