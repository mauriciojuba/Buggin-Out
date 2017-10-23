using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteTV : MonoBehaviour {

    public OLDTVFilter3 tv;

    [Range(0, 1)]
    public float Magnitude;

	// Use this for initialization
	void Start () {
        tv.preset.noiseFilter.magnetude = Magnitude;

    }
	
	// Update is called once per frame
	void Update () {
        tv.preset.noiseFilter.magnetude = Magnitude;
    }
}
