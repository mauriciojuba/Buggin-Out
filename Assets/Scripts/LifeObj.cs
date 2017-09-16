using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeObj : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string Evento;

    public enum ObjectType { Box, Barricade, Luz };

    public ObjectType TypeOfObject;
    public float TotalLife;
    public GameObject ObjDestruido;

    public GameObject[] Loot;
    [Range(0, 100)]

    public int LootChance;


    void ObjectLife()
    {
        if (TotalLife <= 0)
        {
            switch (TypeOfObject)
            {
                case ObjectType.Box:
                    GameObject GB = GameObject.Instantiate(ObjDestruido, transform.position, Quaternion.identity) as GameObject;
                    Component[] RBGB;
                    RBGB = GB.transform.GetComponentsInChildren<Rigidbody>();
                    foreach (Rigidbody rb in RBGB)
                    {
                        rb.velocity = gameObject.GetComponent<Rigidbody>().velocity;
                    }

                    FMODUnity.RuntimeManager.PlayOneShot(Evento, transform.position);
                    Destroy(gameObject);
                    break;

                case ObjectType.Barricade:
                    Instantiate(ObjDestruido, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    break;

                case ObjectType.Luz:
                    if (gameObject.GetComponent<LuzQuebrando>() != null)
                    {
                        gameObject.GetComponent<LuzQuebrando>().Quebrou = true;
                        FMODUnity.RuntimeManager.PlayOneShot(Evento, transform.position);

                        Destroy(this);
                    }
                    break;
            }

            DropLoot();

        }
    }

    void DropLoot()
    {
        float random = Random.Range(0, 100);
        if (random <= LootChance)
        {
            if (Loot[0] != null)
            {
                int drop = Random.Range(0, Loot.Length);
                Debug.Log(drop);
                GameObject gb = GameObject.Instantiate(Loot[drop], transform.position, Quaternion.identity) as GameObject;
                gb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gb.GetComponent<Rigidbody>().AddForce(gb.transform.up * 3500);
            }
        }
    }


}
