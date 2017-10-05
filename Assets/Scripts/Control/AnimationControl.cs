using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    [SerializeField] Animator Anim;

    [SerializeField] GameObject RightFightCol, LeftFightCol, HeadFightCol, RightFootFightCol;

    [SerializeField] GameObject SpecialPrefab;


    public void SetAttackAnim(int AttackNumber) {
        if (Anim == null) {
            return;
        }
        Anim.SetTrigger("Attack");
        Anim.SetTrigger("Attack 2");
        Anim.SetInteger("AttackNumber", AttackNumber);
    }

    public void SetMovimentAnimation(float Velocity) {
        Anim.SetFloat("Mov", Velocity);
    }

    public void SetJumpAnim() {
        if (Anim == null) {
            return;
        }
        Anim.SetTrigger("Jump");
    }
    public void SetGrounded(bool Grounded) {
        Anim.SetBool("grounded", Grounded);
    }

    public void SetTakeDamageAnim() {
        Anim.SetTrigger("TakeDamage");
    }

    #region Functions Active/Desactive Fight Colliders
    public void ActiveRightFightCollider(float Damage) {
        RightFightCol.GetComponent<FightCollider>().Damage = Damage;
        RightFightCol.SetActive(true);
    }

    public void DesactiveRightFightCollider() {
        RightFightCol.SetActive(false);
    }

    public void ActiveLeftFightCollider(float Damage) {
        LeftFightCol.GetComponent<FightCollider>().Damage = Damage;
        LeftFightCol.SetActive(true);
    }

    public void DesactiveLeftFightCollider() {
        LeftFightCol.SetActive(false);
    }

    public void ActiveHeadFightCollider(float Damage) {
        HeadFightCol.GetComponent<FightCollider>().Damage = Damage;
        HeadFightCol.SetActive(true);
    }

    public void DesactiveHeadFightCollider() {
        HeadFightCol.SetActive(false);
    }

    public void ActiveRightFootFightCollider(float Damage) {
        RightFootFightCol.GetComponent<FightCollider>().Damage = Damage;
        RightFootFightCol.SetActive(true);
    }

    public void DesactiveRightFootFightCollider() {
        RightFootFightCol.SetActive(false);
    }
    #endregion

    public void PlaySound(string Event) {
        FMODUnity.RuntimeManager.PlayOneShot(Event, transform.position);
    }

    public void SetRightFootTypeOfAttack(string Event) {
        RightFootFightCol.GetComponent<FightCollider>().Style = Event;
    }

    public void SetHeadTypeOfAttack(string Event) {
        HeadFightCol.GetComponent<FightCollider>().Style = Event;
    }

    public void SetLeftTypeOfAttack(string Event) {
        LeftFightCol.GetComponent<FightCollider>().Style = Event;
    }

    public void SetRightTypeOfAttack(string Event) {
        RightFightCol.GetComponent<FightCollider>().Style = Event;
    }

    public void PickObjAnimation() {
        Anim.SetTrigger("Grab");
    }

    public void ActiveGrabbingAnim() {
        Anim.SetBool("Grabbing", true);
    }

    public void DesactiveGrabbinAnim() {
        Anim.SetBool("Grabbing", false);
    }

    public void ThrowObjAnim() {
        Anim.SetTrigger("Throw");
    }

    public void SetUsingSpecial(bool Set)
    {
        Anim.SetBool("UsingSpecial", Set);
    }

    public void SetSpecial()
    {
        Anim.SetTrigger("Special");
    }

    public void InstantiateSpecialLiz(string Side) {
        GameObject GO = null;
        if (Side == "Left") {
            GO = GameObject.Instantiate(SpecialPrefab, LeftFightCol.transform.position, transform.rotation);
        } else if (Side == "Right") {
            GO = GameObject.Instantiate(SpecialPrefab, RightFightCol.transform.position, transform.rotation);
        }
        Vector3 dir = Quaternion.AngleAxis(Random.Range(-25, 25), transform.up) * transform.forward;
        GO.GetComponent<Rigidbody>().AddForce(dir * 500);
        GO.transform.rotation = Quaternion.LookRotation(dir);
    }
}
