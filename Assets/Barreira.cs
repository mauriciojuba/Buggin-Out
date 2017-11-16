using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira : MonoBehaviour {

	[SerializeField] List<GameObject> AreaEnemys;
	[SerializeField] Rigidbody dominoCenterLeft,dominoCenterRight;
	[SerializeField] Vector3 forceDir;
	[SerializeField] List<GameObject> DeadEnemys;
    [SerializeField] GameObject[] Dominos;
    [SerializeField] bool Ajust;

    [FMODUnity.EventRef]
    public string Evento_Liz;
    [FMODUnity.EventRef]
    public string Evento_Horn;

    [SerializeField] bool Domino, Door;

    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (AreaEnemys.Count == 0) {

            //FMODUnity.RuntimeManager.PlayOneShot(Evento_Horn, transform.position);
            if (Domino)
            {
                dominoCenterLeft.AddForce(forceDir * 2000f);
                dominoCenterRight.AddForce(-forceDir * 2000f);
                this.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(DestroyThisOBJ());
            }else if (Door)
            {
                GetComponent<Animator>().SetTrigger("Open");
                Destroy(this,3);
            }
		}

		for (int i = 0; i < AreaEnemys.Count; i++) {
            if (AreaEnemys[i] == null)
            {
                AreaEnemys.RemoveAt(i);
            }
		}

        if (Ajust)
        {
            AjustSize();
        }
	}

    IEnumerator DestroyThisOBJ()
    {
        yield return new WaitForSeconds(3f);
        
        Ajust = true;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < Dominos.Length; i++)
        {
            Destroy(Dominos[i]);
        }
        Destroy(gameObject);
    }

    void AjustSize()
    {
        for (int i = 0; i < Dominos.Length; i++)
        {
            Dominos[i].transform.localScale = Vector3.Lerp(Dominos[i].transform.localScale, new Vector3(0, 0, 0), Time.deltaTime);
        }
    }
}
