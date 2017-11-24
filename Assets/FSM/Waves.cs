using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;



public class Waves : MonoBehaviour
{

    [System.Serializable]
    public class Caminhos
    {
        public Transform[] Waypoints;
    }
    public List<GameObject> AtualEnemy = new List<GameObject>();

    public int QuantInimigo;
    [SerializeField] int QuantWave;
    [SerializeField] GameObject inimigo;
    [SerializeField] Transform[] Spawn;

    [SerializeField] Transform[] Caminho_1;
    [SerializeField] Transform[] Caminho_2;
    [SerializeField] float DelayOfSpawn = 1.5f;

    public Caminhos[] Ways;

    public bool SpawnSimut = false;
    bool Spawning;
    // Use this for initialization
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (AtualEnemy.Contains(null))
        {
            AtualEnemy.Remove(null);
        }

        if (AtualEnemy.Count < QuantWave && QuantInimigo > 0 && !Spawning)
            StartCoroutine(CriarInimigo());
    }

    IEnumerator CriarInimigo()
    {
        Spawning = true;
        yield return new WaitForSeconds(DelayOfSpawn);
        if (SpawnSimut && QuantInimigo > Spawn.Length && QuantInimigo > 0)
        {
            for (int i = 0; i < Spawn.Length; i++)
            {
                QuantInimigo -= 1;
                GameObject instance = (GameObject)Instantiate(inimigo, Spawn[i].position, Spawn[i].rotation);
                instance.GetComponent<NavMeshAgent>().Warp(Spawn[i].position);
                int Rand = Mathf.RoundToInt(Random.Range(0, 2));

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
            int Rand = Mathf.RoundToInt(Random.Range(0, Ways.Length));
            int SpawnRand = Mathf.RoundToInt(Random.Range(0, Spawn.Length));
            print(Rand);
            GameObject instance = (GameObject)Instantiate(inimigo, Spawn[SpawnRand].position, Spawn[SpawnRand].rotation);

            instance.GetComponent<EnemyIA>().waypoints = new Transform[Ways[Rand].Waypoints.Length];
            for(int o = 0; o < Ways[Rand].Waypoints.Length; o++)
            {
                instance.GetComponent<EnemyIA>().waypoints[o] = Ways[Rand].Waypoints[o];
            }
            instance.transform.SetParent(transform);
            //if (Rand == 0)
            //{

            //    instance.GetComponent<EnemyIA>().waypoints = new Transform[Caminho_1.Length];

            //    for (int o = 0; o < Caminho_1.Length; o++)
            //    {
            //        instance.GetComponent<EnemyIA>().waypoints[o] = Caminho_1[o];
            //    }
            //}

            //if (Rand == 1)
            //{
            //    instance.GetComponent<EnemyIA>().waypoints = new Transform[Caminho_2.Length];

            //    for (int o = 0; o < Caminho_2.Length; o++)
            //    {
            //        instance.GetComponent<EnemyIA>().waypoints[o] = Caminho_2[o];
            //    }
            //}

            AtualEnemy.Add(instance);
        }

        Spawning = false;
    }
}
