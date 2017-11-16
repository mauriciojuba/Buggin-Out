using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspecialLiz : MonoBehaviour {

    public float Damage = 25;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter(Collider hit){
        if (hit.CompareTag("Enemy"))
        {
            if (hit.GetComponent<IA_Mosquito>() != null)
            {
                hit.GetComponent<IA_Mosquito>().playerStr = Damage;
                hit.GetComponent<IA_Mosquito>().hitted = true;
                hit.GetComponent<IA_Mosquito>().TakeDamage();
            }

            if (hit.GetComponent<IA_Aranha>() != null)
            {
                hit.GetComponent<IA_Aranha>().Life -= Damage;
                hit.GetComponent<IA_Aranha>().TakeDamage();
            }

            if (hit.GetComponent<IA_Mariposa>() != null)
            {
                hit.GetComponent<IA_Mariposa>().Life -= Damage;
                hit.GetComponent<IA_Mariposa>().TakeDamage();
            }
        }
	}
}
