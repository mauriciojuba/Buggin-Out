using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumb : MonoBehaviour {

	public int PlayerNumber;

    bool Point;
    [SerializeField] GameObject Seta;
    [SerializeField] GameObject EnemyProximity;
    [SerializeField] GameObject[] Enemies;
	void Start () {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Back P" + PlayerNumber))
        {
            CheckAndPointEnemy();
        }

        if (Point)
        {
            ShowEnemy();
        }
        else
        {
            Seta.SetActive(false);
        }
	}

    void CheckAndPointEnemy()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemies.Length > 0)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (EnemyProximity == null)
                {
                    EnemyProximity = Enemies[i];
                }
                if (Vector3.Distance(transform.position, Enemies[i].transform.position) < Vector3.Distance(transform.position, EnemyProximity.transform.position))
                {
                    EnemyProximity = Enemies[i];
                }
            }
        }
        else
            EnemyProximity = null;

        Point = true;
        StartCoroutine(SetOffPoint());
    }

    void ShowEnemy()
    {
        Seta.SetActive(true);
        Vector3 dir = EnemyProximity.transform.position;
        Seta.transform.rotation = Quaternion.Slerp(Seta.transform.rotation, Quaternion.LookRotation(EnemyProximity.transform.position - Seta.transform.position), Time.deltaTime * 25);
        Seta.transform.eulerAngles = new Vector3(0, Seta.transform.eulerAngles.y, 0);
    }

    public IEnumerator SetOffPoint()
    {
        yield return new WaitForSeconds(3);
        Point = false;
    }
}
