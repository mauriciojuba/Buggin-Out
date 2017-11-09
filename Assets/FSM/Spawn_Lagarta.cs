using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Lagarta : MonoBehaviour {

    public GameObject Lagarta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1_3D")
        {
            Instantiate(Lagarta, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

}
