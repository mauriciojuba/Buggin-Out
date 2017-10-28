using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {

    [SerializeField] Animator _Anim;
    [SerializeField] float Timer;
    [SerializeField] float CooldownToReturn;
    [SerializeField] float TimerToLaunch;
    [SerializeField] float CooldownToLaunch;
    [SerializeField] bool Activated;
    [SerializeField] bool Launched;

    [Header("Configurações de lançamento")]
    [SerializeField]
    GameObject Prefab;
    [SerializeField]
    float MinForce,MaxForce;
    [SerializeField]
    Transform Muzzle;

    GameObject BombIdle;

    // Update is called once per frame
    void Update() {
        if (Activated)
        {
            Timer += Time.deltaTime;
            if (Timer > CooldownToReturn)
            {
                Timer = 0;
                Launched = false;
                _Anim.SetTrigger("Return");
            }
        }
        else if(!Activated && !Launched)
        {
            TimerToLaunch += Time.deltaTime;
            if (TimerToLaunch > CooldownToLaunch)
            {
                TimerToLaunch = 0;
                Launched = true;
                _Anim.SetTrigger("Launch");
            }
        }
	}

    public void ActiveCatapult()
    {
        Activated = true;
    }

    public void DeactiveCatapult()
    {
        Activated = false;
    }

    public void Launch()
    {
        GameObject GB = GameObject.Instantiate(Prefab, Muzzle.position, Quaternion.Euler(Muzzle.rotation.x, Muzzle.rotation.y + 180, Muzzle.rotation.z));
        Destroy(BombIdle);
        GB.GetComponent<Rigidbody>().AddForce(Muzzle.forward * Random.Range(MinForce, MaxForce));
        GB.GetComponent<DestruirObjeto>().Throwed = true;
    }

    public void InstantiateBombIdle()
    {
        BombIdle = GameObject.Instantiate(Prefab, Muzzle.position, Quaternion.Euler(Muzzle.rotation.x, Muzzle.rotation.y + 180, Muzzle.rotation.z));
        BombIdle.transform.SetParent(Muzzle);
        BombIdle.GetComponent<Rigidbody>().isKinematic = true;
    }
}
