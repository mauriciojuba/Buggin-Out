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
    public float TimeInPoison;

    void Start () {
        LifeInGame = Instantiate(LifeEmblem, Camera.main.transform);
        LifeInGame.name = "LifeEmblem";
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
        if(Mask != null)
		Mask.SetFloat ("Life", (LifeAtual / MaxLife));
	}

    public void DamagePerSecond(float Damage)
    {
        TimeInPoison += Time.deltaTime;
        if(TimeInPoison >= 1.5f)
        {
            LifeAtual -= Damage;
            TimeInPoison = 0;
        }
    }
}
