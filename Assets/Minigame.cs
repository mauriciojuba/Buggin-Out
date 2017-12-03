using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Minigame : MonoBehaviour {

    [SerializeField] Collider _Col;
    [SerializeField] float Timer;
    [SerializeField] List<GameObject> AreaEnemies;
    [SerializeField] bool Started;
    [SerializeField] GameObject[] Prision;
    [SerializeField] Text _TextTimer;
	void Start () {
        for (int i = 0; i < AreaEnemies.Count; i++)
        {
            AreaEnemies[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Started)
        {

            if (_TextTimer != null)
            {
                int minutes = (int)(Timer / 60);
                int seconds = (int)(Timer % 60);
                _TextTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            if (AreaEnemies.Count > 0)
            {
                if(Timer > 0)
                Timer -= Time.deltaTime;
                else
                {
                    Timer = 0;
                }
            }
            for (int i = 0; i < AreaEnemies.Count; i++)
            {
                if (AreaEnemies[i].GetComponent<Waves>() != null)
                {
                    if (AreaEnemies[i].GetComponent<Waves>().AtualEnemy.Count == 0 && AreaEnemies[i].GetComponent<Waves>().QuantInimigo == 0)
                    {
                        AreaEnemies.RemoveAt(i);
                    }
                }
            }

            if(AreaEnemies.Count == 0 && Timer > 0)
            {
                for (int a = 0; a < Prision.Length; a++)
                {
                    Destroy(Prision[a]);
                }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player1_3D") || other.CompareTag("Player2_3D"))
        {
            for(int i = 0; i < AreaEnemies.Count; i++)
            {
                AreaEnemies[i].SetActive(true);
            }
            Started = true;
            _Col.enabled = false;
        }
    }
}
