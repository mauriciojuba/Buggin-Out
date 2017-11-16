using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Mariposa : EnemyIA
{

    public GameObject HitBox;
    public GameObject TornadoPrefab;
    public float Time_atk_Area = 5F;

    public override void Attack()
    {
        _anim.SetTrigger("ATK2");

        if (hitted) ActualState = State.TakeDamage;

        ActualState = State.Chase;



    }

    public override void Chase()
    {

        Time_atk_Area -= Time.deltaTime;
        if (Time_atk_Area < 0)
        {
            ActualState = State.OnScreenAttack;
        }

        if (_anim != null)
        {
            _anim.SetBool("IsParolling", false);
            _anim.SetBool("FightingWalk", true);
        }
        Vector3 dir = Target.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (targetDistance > EnemyDist && Vector3.Distance(Target.transform.position, gameObject.transform.position) < SafeDist)
        {
            RB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        attackTimer += Time.deltaTime;
        if (targetDistance <= EnemyDist && attackTimer >= attackDelay)
        {
            ActualState = State.Attack;
            attackTimer = 0;
        }
        if (targetDistance > SafeDist + 1)
        {
            if (_anim != null)
                _anim.SetBool("FightingWalk", false);
            ActualState = State.Patrol;
        }
        if (hitted)
            ActualState = State.TakeDamage;
    }

    public override void OnScreenIdle()
    {

    }

    public override void OnScreenAttack()
    {
        _anim.SetTrigger("ATK1");

        Time_atk_Area = 5F;

        if (hitted) ActualState = State.TakeDamage;

        ActualState = State.Chase;
    }

    public override void DownToGround()
    {

    }

    public override void Die()
    {
        RB.isKinematic = true;
        _anim.SetTrigger("Death");

    }


    public void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("playerHitCollider"))
        {
            playerStr = hit.GetComponent<FightCollider>().Damage;
            if (!hitted) hitted = true;
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

    public void SpawnTornado()
    {
        GameObject Torn = GameObject.Instantiate(TornadoPrefab, transform.position, transform.rotation);
        Torn.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }
}
