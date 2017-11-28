using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysRemain : MonoBehaviour {

    [SerializeField] Waves[] EnemyQuant;
    [SerializeField] int[] EnemysRemainsArray;
    [SerializeField] int[] EnemysInScene;
    [SerializeField] int EnemysRemains;

    void Start () {
        EnemysRemainsArray = new int[EnemyQuant.Length];
        for (int i = 0; i < EnemyQuant.Length; i++)
        {
            EnemysRemainsArray[i] = EnemyQuant[i].QuantInimigo;
        }
        for (int a = 0; a < EnemyQuant.Length; a++)
        {

        }
        for (int o = 0; o < EnemysRemainsArray.Length; o++)
        {
            EnemysRemains += EnemysRemainsArray[o];
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < EnemyQuant.Length; i++)
        {
            EnemysRemainsArray[i] = EnemyQuant[i].QuantInimigo;
        }
        
       

	}
}
