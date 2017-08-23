using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class ColliderDano : MonoBehaviour {

    public AIRig ai;
    BoxCollider bc;
    public int Damage;
    private void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (ai.AI.WorkingMemory.GetItem<bool>("attack"))
        {
            bc.enabled = true;
            ai.AI.WorkingMemory.SetItem("attack", false);
        }
        else
        {
            bc.enabled = false;
        }
        
    }
    // Use this for initialization
    void OnTriggerEnter(Collider col)
    {
        //aplicar a função de causar dano.
        if (col.gameObject.GetComponent<Life>() != null && (col.CompareTag("Player1_3D")|| col.CompareTag("Player2_3D")|| col.CompareTag("Player3_3D")|| col.CompareTag("Player4_3D")))
        {
            Debug.Log("work");
            //Aqui deve ser chamado o método(função) que substituirá o Update do script Life.
            col.gameObject.GetComponent<Life>().LifeQuant -= Damage;
            col.gameObject.GetComponent<Life>().UpdateLife();
            bc.enabled = false;
        }
        
    }
}
