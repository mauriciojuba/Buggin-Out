using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Lagarta : EnemyIA  {

    public Transform Target_On_Screen;
    public float Target_dist;
    public GameObject Model_Lagarta;
    public GameObject Casulo;

    

    public override void OnScreenChase()
    {
        Model_Lagarta.SetActive(true);
        RB.isKinematic = true;
        Target_dist = Vector3.Distance(transform.position, Target_On_Screen.position);
        

        if (Target_dist > 0.3f)
        {
            LifeEmblemChase();
        }
        else
        {
            ActualState = State.OnScreenAttack;
        }

    }

    public override void OnScreenIdle()
    {
        if (Target_On_Screen == null)
            Target_On_Screen = GameObject.Find("Target-1").transform;

        Model_Lagarta.SetActive(false);
        
        Screen.GoToScreen(gameObject, -50f, 0 , 0f);
        RB.useGravity = false;
        screenSpeed = 0.5f;
        onScreen = true;

        if (Screen.GoToScreen(gameObject, randomOffsetOnScreen))
        {
            ActualState = State.OnScreenChase;
        }
    }

    /*
    public override void UpToScreen()
    {
        if (!onScreen)
        {

            worldPos = new GameObject("World Pos Mosquito").transform;
            worldPos.position = transform.position;
            RB.useGravity = false;
            screenSpeed = 0.5f;
            onScreen = true;
        }
        if (Screen.GoToScreen(gameObject, randomOffsetOnScreen))
        {
            ActualState = State.OnScreenChase;
        }
    }
    */



    public override void OnScreenAttack()
    {
        GameObject.Instantiate(Casulo, transform.position, transform.rotation,transform.parent);
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