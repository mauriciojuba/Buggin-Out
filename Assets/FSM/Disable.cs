using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {

    public GameObject FalaHorn, FalaLiz;
    public bool activated;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Horn")
        {
            if (!activated)
                FalaHorn.SetActive(true);
            //gameObject.GetComponent<Collider>().enabled = false;

        }
        else if(other.gameObject.name == "Liz")
        {
            if (!activated)
                FalaLiz.SetActive(true);
            //gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
