using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEspecial : MonoBehaviour {

    public bool Horn, Liz;
    [SerializeField] bool UseForce;
    [SerializeField] float TimeToStop;
    [SerializeField] float TimeToReduce;
    [SerializeField] float Force;

    float Timer;
    void Start () {
		
	}
	
	void Update () {
		
	}


    public bool UsingSpecial()
    {
        Timer += Time.deltaTime;
        if (UseForce)
        {
            if (Timer <= 3)
            {
                Force += Time.deltaTime * 5;
            }
            if (Timer >= TimeToReduce && Force > 0)
            {
                Force -= Time.deltaTime * 7;
            }
            if (Force < 0)
            {
                Force = 0;
                Timer = 0;
                return false;
            }
        }
        if(Timer >= TimeToStop)
        {
            Timer = 0;
            return false;
        }else
        {
            return true;
        }
    }

    public void SpecialHorn()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.VelocityChange);
    }
}
