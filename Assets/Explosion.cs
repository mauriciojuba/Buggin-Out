using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    [SerializeField] float Force;
    [SerializeField] float Radius;
    [SerializeField] float Damage;

    private void Start()
    {
        Destroy(gameObject, 2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, Radius,1, ForceMode.Impulse);
        }

        //fazer o mesmo pra tirar a vida do mosquito
        if(other.GetComponent<PlayerLife>() != null)
        {
            other.GetComponent<PlayerLife>().LifeAtual -= Damage;
        }
    }

}
