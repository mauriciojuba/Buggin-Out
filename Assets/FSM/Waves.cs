using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    public List<GameObject> AtualEnemy = new List<GameObject>();

    public int QuantInimigo;
    public GameObject inimigo;
    public Transform[] Spawn;

    public Transform[] Caminho_1;
    public Transform[] Caminho_2;

    public bool SpawnSimut = false;

    // Use this for initialization
    void Start()
    {
        CriarInimigo();
    }

    private void FixedUpdate()
    {
        if (AtualEnemy.Contains(null))
        {
            AtualEnemy.Remove(null);
        }

        if (AtualEnemy.Count <= 0)
            CriarInimigo();
    }

    public void CriarInimigo()
    {
        if (SpawnSimut && QuantInimigo > Spawn.Length && QuantInimigo > 0)
        {
            for (int i = 0; i < Spawn.Length; i++)
            {
                QuantInimigo -= 1;
                GameObject instance = (GameObject)Instantiate(inimigo, Spawn[i].position, Spawn[i].rotation);

                int Rand = Mathf.RoundToInt(Random.Range(0, 1));

                if (Rand == 0)
                {
                    instance.GetComponent<EnemyIA>().waypoints = new Transform[Caminho_1.Length];
                    for (int o = 0; o < Caminho_1.Length; o++)
                    {
                        instance.GetComponent<EnemyIA>().waypoints[o] = Caminho_1[o];
                    }
                }

                if (Rand == 1)
                {
                    instance.GetComponent<EnemyIA>().waypoints = new Transform[Caminho_2.Length];

                    for (int o = 0; o < Caminho_2.Length; o++)
                    {
                        instance.GetComponent<EnemyIA>().waypoints[o] = Caminho_2[o];
                    }
                }

                AtualEnemy.Add(instance);
            }
        }

        else if (QuantInimigo > 0 && SpawnSimut == false)
        {
            QuantInimigo -= 1;
            int Rand = Mathf.RoundToInt(Random.Range(0, Spawn.Length));
            print(Rand);
            GameObject instance = (GameObject)Instantiate(inimigo, Spawn[Rand].position, Spawn[Rand].rotation);

            

            if (Rand == 0)
            {

                instance.GetComponent<EnemyIA>().waypoints = new Transform[Caminho_1.Length];

                for (int o = 0; o < Caminho_1.Length; o++)
                {
                    instance.GetComponent<EnemyIA>().waypoints[o] = Caminho_1[o];
                }
            }

            if (Rand == 1)
            {
                instance.GetComponent<EnemyIA>().waypoints = new Transform[Caminho_2.Length];

                for (int o = 0; o < Caminho_2.Length; o++)
                {
                    instance.GetComponent<EnemyIA>().waypoints[o] = Caminho_2[o];
                }
            }

            AtualEnemy.Add(instance);
        }


    }
}
