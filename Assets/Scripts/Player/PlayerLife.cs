using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {

	public float MaxLife, LifeAtual;
    public GameObject LifeEmblem;
    public GameObject LifeInGame;
	float maskOver;
	public Animator Mask;

    private void Awake()
    {
       LifeInGame = Instantiate(LifeEmblem, Camera.main.transform);
       LifeInGame.name = "LifeEmblem";
    }

    void Start () {
        Mask = LifeInGame.GetComponent<Animator>();
		MaxLife = 300f;
		LifeAtual = MaxLife;
        Mask.SetFloat("Life", (LifeAtual / MaxLife));
    }
	
	// Update is called once per frame
	void Update () {
		atualizaVida ();
	}

	void atualizaVida(){
		Mask.SetFloat ("Life", (LifeAtual / MaxLife));
	}
}
