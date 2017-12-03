using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBus : MonoBehaviour {


    string masterBusString = "Bus:/";
    FMOD.Studio.Bus Trilha, Sfx , Falas , Master;
    
    //turmina dps regula os volumes maximos de cada um aqui
    // \/ pq ta ficando muito alto... dps eu arrumo as contas no outro script
    [Range(0,1)]
    public float VolMaster , VolTrilhas ,VolFalas , VolSFX;
    [Range(-0.2f,0.2f)]
    public float BrightnessNivel;

    public int MasterVolume, MusicVolume, SFXVolume, DubsVolume, Brightness;

    private GameObject[] Controls;
    private OLDTVFilter3 TV;

    private void Awake()
    {
        //procura todas os objetos de Dados do jogo e coloca no Array.
        Controls = GameObject.FindGameObjectsWithTag("VolumeControl");
        TV = Camera.main.GetComponent<OLDTVFilter3>();
        //se tiver mais de um objeto de dados, ele destroi o objeto mais recente, mantendo apenas 1.
        if (Controls.Length >= 2)
        {
            Destroy(Controls[1]);
        }

        DontDestroyOnLoad(this);
    }

    void Start () {
        Master = FMODUnity.RuntimeManager.GetBus(masterBusString);
        Sfx = FMODUnity.RuntimeManager.GetBus("{474187bf-c89c-4ab4-8eb0-272027b22f62}");
        Falas = FMODUnity.RuntimeManager.GetBus("{c4aad773-f1a6-4700-b518-16f78b7bd0a2}");
        Trilha = FMODUnity.RuntimeManager.GetBus("{d11da0bc-1acc-43ea-b60b-f8d20eaf00ce}");

        SetMasterVolume();
        SetSfxVolume();
        SetDubsVolume();
        SetMusicVolume();
        SetBrightness();
    }

    public void SetMasterVolume()
    {
        Master.setVolume(VolMaster);
    }

    public void SetSfxVolume()
    {
        Sfx.setVolume(VolSFX);
    }

    public void SetDubsVolume()
    {
        Falas.setVolume(VolFalas);
    }

    public void SetMusicVolume()
    {
        Trilha.setVolume(VolTrilhas);
    }

    public void SetBrightness()
    {
        TV.preset.televisionFilter.brightness = BrightnessNivel;
    }
}
