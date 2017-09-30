using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour,IGroundEnemy,IKillable, IAnimated<Animator> {

	private Rigidbody RB;
	public enum State {Idle, Patrol, Chase, Flee, Attack, TakeDamage, Dead, OnScreen};
	GameObject player1, player2;

	[Header("Configurações")]
	public float speed, chaseSpeed, fleeSpeed;
	public float attackDelay, attackStr, Visao;

	[Header("Estado atual")]
	public State MyActualState = State.Chase;

	[Header("Foco do inimigo")]
	public Transform Target;


	#region IGroundEnemy
	public void CheckAllTargets()
	{
	}
	public void Idle(Transform[] players)
	{
	}
	public void Patrol(float speed, Transform[] waypoints)
	{
	}
	public void Chase(float speed, Transform target)
	{
	}
	public void Flee(float speed, Transform nextPos)
	{
	}
	public void Attack(float hitPoints, GameObject player)
	{
	}
	#endregion

	#region IKillable
	public void TakeDamage (float hitPoints)
	{
	}
	public void Die ()
	{
	}
	#endregion

	#region IAnimated implementation
	public void Animate (Animator anim, string variable, float value)
	{
	}
	#endregion

}