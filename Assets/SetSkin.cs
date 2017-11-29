using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkin : MonoBehaviour {

    public Material Skin;


    void Start()
    {
        if (gameObject.name == "Liz")
        {
            for (int i = 0; i < gameObject.transform.Find("Liz:Liz_ALL").Find("Liz:MALHA").childCount; i++)
            {
                gameObject.transform.Find("Liz:Liz_ALL").Find("Liz:MALHA").GetChild(i).GetComponent<SkinnedMeshRenderer>().material = Skin;
            }
        }

        if (gameObject.name == "Horn")
        {
            for (int i = 0; i < gameObject.transform.Find("Horn:Horn_ALL").Find("Horn:MALHA").childCount; i++)
            {
                gameObject.transform.Find("Horn:Horn_ALL").Find("Horn:MALHA").GetChild(i).GetComponent<SkinnedMeshRenderer>().material = Skin;
            }
        }

        Destroy(this, 3);
    }
	
}
