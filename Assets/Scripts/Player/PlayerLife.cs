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
    public bool Stunned;
    [SerializeField] CameraControl _Cam;
    [SerializeField] GameObject ParticlePoison;

    void Start () {
        LifeInGame = Instantiate(LifeEmblem, Camera.main.transform);
        LifeInGame.name = "LifeEmblem";
        Mask = LifeInGame.GetComponent<Animator>();
		MaxLife = 300f;
		LifeAtual = MaxLife;
        Mask.SetFloat("Life", (LifeAtual / MaxLife));
        _Cam = GameObject.FindWithTag("DollyCam").GetComponent<CameraControl>();
    }
	
	// Update is called once per frame
	void Update () {
		atualizaVida ();
        if(LifeAtual <= 0)
        {
            Stunned = true;
            GetComponent<HornControl>().mov = Vector3.zero;
            GetComponent<HornControl>().CanMove = false;
            if (!_Cam.StunnedPlayers.Contains(gameObject))
            {
                _Cam.StunnedPlayers.Add(gameObject);
            }
        }
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
            if (ParticlePoison != null)
            {
                ParticlePoison.GetComponent<PoisonParticle>().Activate();
            }
            GetComponent<AnimationControl>().SetTakeDamageAnim();
            TimeInPoison = 0;
        }
    }
}
