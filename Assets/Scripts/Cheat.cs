using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

    [SerializeField] GameObject[] Barreiras;
    [SerializeField] GameObject Player1, Player2;
    [SerializeField] GameObject Boss;
    void Start () {
        Player1 = GameObject.FindWithTag("Player1_3D");
        if(GameObject.FindWithTag("Player2_3D") != null)
        {
            Player2 = GameObject.FindWithTag("Player2_3D");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < Barreiras.Length; i++)
            {
                Barreiras[i].GetComponent<Barreira>().AreaEnemys = new List<GameObject>(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(Player1 != null)
            {
                Player1.GetComponent<PlayerLife>().MaxLife = 99999;
                Player1.GetComponent<PlayerLife>().LifeAtual = 99999;
            }
            if (Player2 != null)
            {
                Player2.GetComponent<PlayerLife>().MaxLife = 99999;
                Player2.GetComponent<PlayerLife>().LifeAtual = 99999;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(Boss != null)
            {
                Boss.GetComponent<IA_Boss>().Life = 1;
            }
        }
	}
}
