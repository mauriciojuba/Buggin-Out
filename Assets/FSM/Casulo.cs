using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casulo : MonoBehaviour {


    public GameObject Cas;

    Vector3 target_Chao =  new Vector3(0,0,0);
    public Transform Target;

    public GameObject Teste;

    public bool Caindo;

    RaycastHit hit;
    private void FixedUpdate()
    {
        shoot();
    }

    public void shoot()
    {
        bool Cai = false;

        if (Target == null)
            Target = GameObject.Find("RaycastTarget-1").transform;
        transform.LookAt(Target.position);

        if (!Caindo)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                //Instantiate(Teste, hit.point, Quaternion.LookRotation(hit.normal));
                target_Chao = hit.point;


            }
        }
        else
        {
            if (!Cai)
            {
                Cas.transform.parent = null;

                Cas.transform.position = Vector3.Lerp(transform.position, target_Chao, 0.02F);
                Cas.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), 0.1F);
                Cai = true;

                Destroy(Cas, 3F);
            }
        }
    }

}
