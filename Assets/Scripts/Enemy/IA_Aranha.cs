using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Aranha : EnemyIA {

	#region Aranha variáveis
	public GameObject Muzle;
	public GameObject Shot;
	public float force;
	#endregion

	#region Override EnemyIA
	public override void Attack ()
	{
		_anim.SetBool("IsIdle", true);

		if (hitted)
			ActualState = State.TakeDamage;

		//rotaciona o Npc apontando para o alvo
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotationSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		attackTimer += Time.deltaTime;
		if (targetDistance <= EnemyDist && attackTimer >= attackDelay)
		{
			_anim.SetBool("IsIdle", false);
            _anim.SetTrigger("ATK");
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
