using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspecialHorn : MonoBehaviour {

    [SerializeField] float Damage;
    [SerializeField] float Force;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyIA>() != null)
            {
                other.GetComponent<EnemyIA>().playerStr = Damage;
                other.GetComponent<EnemyIA>()._anim.SetTrigger("TakeDamage");
                other.GetComponent<EnemyIA>().hitted = true;
                other.GetComponent<Rigidbody>().AddForce(-other.transform.forward * Force);
            }
            if (other.GetComponent<IA_Aranha>() != null)
            {
                other.GetComponent<IA_Aranha>().playerStr = Damage;
                other.GetComponent<IA_Aranha>()._anim.SetTrigger("TakeDamage");
                other.GetComponent<IA_Aranha>().hitted = true;
                other.GetComponent<Rigidbody>().AddForce(-other.transform.forward * Force);
            }
        }
    }
}
