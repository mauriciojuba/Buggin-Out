using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Lagarta : EnemyIA {

    public Transform Target_On_Screen;
    public float Target_dist;

    public GameObject Casulo;

    public override void OnScreenChase()
    {
        RB.isKinematic = true;
        Target_dist = Vector3.Distance(transform.position, Target_On_Screen.position);
        Debug.Log(Target_dist);

        if (Target_dist > 0.2f)
        {
            LifeEmblemChase();
        }
        else
        {
            ActualState = State.OnScreenAttack;
        }

    }

    public override void OnScreenAttack()
    {

        GameObject.Instantiate(Casulo, transform.position, transform.rotation);
        Destroy(gameObject);
    }


public void LifeEmblemChase()
    {
        Vector3 mov;
        mov = new Vector3(Target_On_Screen.position.x - transform.position.x, Target_On_Screen.position.y - transform.position.y, 0);
        mov = Camera.main.transform.TransformVector(mov);
        transform.Translate(mov * transform.localScale.magnitude * Time.deltaTime * screenSpeed, Space.World);
        transform.LookAt(Camera.main.transform, transform.up + mov);
    }

}
