using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {

    public GameObject FalaHorn, FalaLiz;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Horn")
        {
            FalaHorn.SetActive(true);
            gameObject.GetComponent<Collider>().enabled = false;

        }
        else if(other.gameObject.name == "Liz")
        {
            FalaLiz.SetActive(true);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
