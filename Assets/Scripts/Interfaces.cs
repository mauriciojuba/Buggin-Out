using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGroundEnemy
{
	void CheckAllTargets();
	void Idle(Transform[] players);
	void Patrol(float speed, Transform[] waypoints);
	void Chase(float speed, Transform target);
	void Flee(float speed, Transform nextPos);
	void Attack(float hitPoints, GameObject player);
}
public interface IKillable
{
	void TakeDamage(float hitPoints);
	void Die();
}
public interface IGoToScreen
{
	void UpToScreen();
	void DownToGround();
}
public interface IScreenEntity{
	void WalkOnScreen();
	void AttackOnScreen();
}
public interface IAnimated<T>
{
	void Animate(T anim, string variable, float value);
}