using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour {

    [FMODUnity.EventRef]
    public string Evento;
    public float TotalLife;

    [Header("Loot")]
    public GameObject[] Loot;
    [Range(0, 100)]
    public int LootChance;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
