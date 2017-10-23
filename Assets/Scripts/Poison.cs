using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {

    [SerializeField] bool IsAnimated,DecreaseSize;
    [SerializeField] float Timer;
    [SerializeField] float DamagePerSecond;
    [SerializeField] float Damp;
    [SerializeField] Vector3 LastSize;

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (IsAnimated)
        {
            if (!DecreaseSize) {
                transform.localScale = Vector3.Lerp(transform.localScale, LastSize, Time.deltaTime * Damp);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * Damp);
            }

            if(transform.localScale.x <= 0.01f)
            {
                Destroy(gameObject);
            }
            if (transform.localScale.x >= LastSize.x -0.01 && transform.localScale.x <= LastSize.x) {
                Timer += Time.deltaTime;
                if(Timer >= 3)
                {
                    DecreaseSize = true;
                }
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerLife>() != null)
        {
            other.GetComponent<PlayerLife>().DamagePerSecond(DamagePerSecond);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerLife>() != null)
        {
            other.GetComponent<PlayerLife>().TimeInPoison = 0;
        }
    }
}
