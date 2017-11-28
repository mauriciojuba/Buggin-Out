using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : MonoBehaviour
{

    public GameObject On;
    public bool activated;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Horn"  || other.gameObject.name == "Liz")
        {
            On.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
