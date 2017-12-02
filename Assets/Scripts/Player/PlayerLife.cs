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
    public Animator SpecialMask;
    public float TimeInPoison;
    public bool Stunned;
    private float TimerToFala = 0;
    [SerializeField] CameraControl _Cam;
    [SerializeField] GameObject ParticlePoison;

    void Start () {
        LifeInGame = Instantiate(LifeEmblem, Camera.main.transform);
        LifeInGame.GetComponent<PlayerLifePos>().PlayerNumb = GetComponent<PlayerNumb>().PlayerNumber;
        LifeInGame.name = "LifeEmblem";
        Mask = LifeInGame.GetComponent<Animator>();
        SpecialMask = LifeInGame.transform.Find("Special").GetComponent<Animator>();
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

         bool dublagem = false;

        if (LifeAtual <= MaxLife * 0.5)
        {
            dublagem = true;
            TimerToFala -= Time.deltaTime;
        } 
        if (dublagem && TimerToFala <=0)
        {
            if(gameObject.name == "Horn")
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Horn/Falas/Pouca_Vida_Horn", transform.position);
            if (gameObject.name == "Liz")
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Liz/Falas/Pouca_Vida", transform.position);
            TimerToFala = 300;
        }

        if (Mask != null)
        {
            Mask.SetFloat("Life", (LifeAtual / MaxLife));
            SpecialMask.SetFloat("Special", (GetComponent<HornControl>().NumberOfSpecial / 100));
        }
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
