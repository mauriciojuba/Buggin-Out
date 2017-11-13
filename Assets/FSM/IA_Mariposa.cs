using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Mariposa : EnemyIA
{

    public GameObject HitBox;

    public override void Attack()
    {
        _anim.SetTrigger("ATK1");

        ActualState = State.Chase;

        if (hitted) ActualState = State.TakeDamage;
    }

    public override void OnScreenIdle()
    {

    }
   
    public override void OnScreenAttack()
    {
       
    }

    public override void DownToGround()
    {
       
    }

    public void OnTriggerExit(Collider hit){
    	if (hit.CompareTag ("playerHitCollider")) {
    		playerStr = hit.GetComponent<FightCollider> ().Damage;
    		if(!hitted) hitted = true;
    	}
    }

    public void HitBoxOn()
    {
        HitBox.GetComponent<Collider>().enabled = true;
    }

    public void HitBoxOff()
    {
        HitBox.GetComponent<Collider>().enabled = false;
    }

        }
