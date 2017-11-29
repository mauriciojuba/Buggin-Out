using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Aranha : EnemyIA {

	#region Aranha variáveis
	public GameObject Muzle;
	public GameObject Shot;
    public bool Attacking;
	public float force;
    #endregion

    #region Override EnemyIA
    public override void Chase()
    {
        base.Chase();
        _navMeshAgent.stoppingDistance = EnemyDist;
    }

    public override void Attack ()
	{
        //_navMeshAgent.enabled = false;

       

        _anim.SetBool("IsIdle", true);
        _anim.SetBool("FightingWalk", false);


        if (hitted)
			ActualState = State.TakeDamage;

        //rotaciona o Npc apontando para o alvo
        if (!Attacking)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            if(_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Ranged") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                Attacking = false;
            }
        }

		attackTimer += Time.deltaTime;
		if (targetDistance <= EnemyDist && attackTimer >= attackDelay)
		{
			_anim.SetBool("IsIdle", false);
            _anim.SetTrigger("ATK");
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Inimigos/Aranha/Ataque_Basico_Aranha", transform.position);
            Attacking = true;
			attackTimer = 0;
		}
		if (targetDistance > EnemyDist)
		{
			_anim.SetBool("IsIdle", false);
			ActualState = State.Patrol;
		}
		if (hitted)
		{
			_anim.SetBool("IsIdle", false);
			ActualState = State.TakeDamage;
		}
	}
	public override void TakeDamage ()
	{
		_anim.SetBool("TakeDamage", true);
        if (hitted)
        {
            if (playerStr == 0)
                playerStr = 50;

            Life -= playerStr;
            hitted = false;
        }
        if (Life <= 0)
		{
            float random = Random.Range(0, 100);
            if (random <= VoiceChance)
            {
                if (Target.name == "Horn")
                    FMODUnity.RuntimeManager.PlayOneShot(Evento_Horn, transform.position);
                if (Target.name == "Liz")
                    FMODUnity.RuntimeManager.PlayOneShot(Evento_Liz, transform.position);
            }
            _anim.SetBool("TakeDamage", false);
			ActualState = State.Dead;
		}
		else
		{
			_anim.SetBool("TakeDamage", false);
			ActualState = State.Chase;
		}
	}
	public override void Die ()
	{

		_anim.SetTrigger("Die");
		Destroy (gameObject, 4f);
	}
	#endregion
	
	#region Ataques Aranha
	public void WebBall () {
		GameObject part = Instantiate(Shot, Muzle.transform.position, Quaternion.identity) as GameObject;
		part.GetComponent<Rigidbody>().AddForce(transform.forward * force);
		Destroy(part, 5);
	}
	#endregion
}
