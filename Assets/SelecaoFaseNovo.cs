using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecaoFaseNovo : MonoBehaviour {

    [Header("Configuração seleção de fases")]
    [SerializeField] GameObject[] AllObjs;
    [SerializeField] GameObject[] AllObjsPos;

    [SerializeField] List<GameObject> UnlockedObjs;
    [SerializeField] List<GameObject> UnlockedObjsPos;
    Data DataS;
    bool Selected;





    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
