using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGroundEnemy
{
	void CheckAllTargets();
	void CalculaDistancia();
	void Idle();
	void Patrol();
	void Chase();
	void Flee();
	void Attack();
}
public interface IKillable
{
	void TakeDamage();
	void Grabbed();
	void Die();
}
public interface IGoToScreen
{
	void UpToScreen();
	void DownToGround();
}
public interface IScreenEntity{
	void OnScreenChase();
	void OnScreenAttack();
	void OnScreenIdle ();
	void OnScreenDamage ();
	void OnScreenFall ();
}