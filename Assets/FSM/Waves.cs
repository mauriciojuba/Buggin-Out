using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

    public List<GameObject> AtualEnemy = new List<GameObject>();

    public int QuantInimigo;
    public GameObject inimigo;
    public Transform[] Spawn;
    public Transform[] Waypoint;

    public bool SpawnSimut = false;

	// Use this for initialization
	void Start () {
        CriarInimigo();
	}

    private void FixedUpdate()
    {
        if (AtualEnemy.Contains(null))
        {
            AtualEnemy.Remove(null);
        }

        if (AtualEnemy.Count <=0) 
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
                instance.GetComponent<EnemyIA>().waypoints[0] = Waypoint[0];
                instance.GetComponent<EnemyIA>().waypoints[1] = Waypoint[1];

                AtualEnemy.Add(instance);
            }
        }

        else if (QuantInimigo > 0 && SpawnSimut == false)
        {
            QuantInimigo -= 1;
            int Rand = Mathf.RoundToInt(Random.Range(0, Spawn.Length));
            print(Rand);
            GameObject instance = (GameObject)Instantiate(inimigo, Spawn[Rand].position, Spawn[Rand].rotation);
            instance.GetComponent<EnemyIA>().waypoints[0] = Waypoint[0];
            instance.GetComponent<EnemyIA>().waypoints[1] = Waypoint[1];

            AtualEnemy.Add(instance);
        }
        

    }
}
