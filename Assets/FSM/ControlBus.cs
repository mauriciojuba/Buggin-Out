using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBus : MonoBehaviour {


    string masterBusString = "Bus:/";
    FMOD.Studio.Bus Trilha, Sfx , Falas , Mater;
    [Range(0,1)]
    public float VolMaster , VolTrilhas ,VolFalas , VolSFX;
    
    // Use this for initialization
    void Start () {
        Mater = FMODUnity.RuntimeManager.GetBus(masterBusString);
        Sfx = FMODUnity.RuntimeManager.GetBus("{474187bf-c89c-4ab4-8eb0-272027b22f62}");
        Falas = FMODUnity.RuntimeManager.GetBus("{c4aad773-f1a6-4700-b518-16f78b7bd0a2}");
        Trilha = FMODUnity.RuntimeManager.GetBus("{d11da0bc-1acc-43ea-b60b-f8d20eaf00ce}");
    }
	
	// Update is called once per frame
	void Update () {
        Mater.setVolume(VolMaster);
        Sfx.setVolume(VolSFX);
        Falas.setVolume(VolFalas);
        Trilha.setVolume(VolTrilhas);
    }
}
