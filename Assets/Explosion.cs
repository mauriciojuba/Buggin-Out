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

        if (other.GetComponent<EnemyIA>() != null)
        {
            other.GetComponent<EnemyIA>().playerStr = Damage;
            other.GetComponent<EnemyIA>()._anim.SetTrigger("TakeDamage");
            other.GetComponent<EnemyIA>().hitted = true;

        }

        if (other.GetComponent<IA_Boss>() != null)
        {
            if (other.GetComponent<IA_Boss>().CanHit && !other.GetComponent<IA_Boss>().hitted)
            {
                other.GetComponent<IA_Boss>().playerStr = Damage;
                other.GetComponent<IA_Boss>().hitted = true;
                other.GetComponent<IA_Boss>().ActualState = IA_Boss.State.LevaDano;
            }
        }
    }

}
