using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
using UnityEngine.UI;

public class Life : MonoBehaviour {


    [FMODUnity.EventRef]
    public string Evento;

    public enum LifeType {Player, Enemy, Boss, Object};
    public enum EnemyType {Mosquito, Aranha};
	public enum ObjectType {Box, Barricade, Luz, Bomb};
    public LifeType LifeOF;
    public EnemyType TypeEnemy;
	public ObjectType TypeOfObject;
	public float LifeQuant;
    public GameObject[] Loot;
    [Range(0,100)]
    public int LootChance;


    #region Variables For Enemys Life



    #endregion

    #region Variables For Objects Life
    public GameObject ObjDestruido;
    #endregion

    #region Variables For Players Life
    public List<GameObject> ListOfImg;
	[SerializeField] private int PlayerNumber;
	public GameObject LifeOBJ;
	private GameObject Container;
	public int Division;
	public int QuantImgInScene;
	[SerializeField] private int QuantImg;
	public GameObject LifeSpritePrefab;
	[SerializeField] private GameObject ScreenGlass;
	[SerializeField] private CameraControl DollyCam;
	public float MaxLife;
	public float X, InitialX;
    #endregion
	[SerializeField] private bool UpdateL;
    private SFX sfx;

    public bool UpdateL1
    {
        get
        {
            return UpdateL;
        }

        set
        {
            UpdateL = value;
        }
    }

    void Start()
    {
        if(GetComponent<SFX>() != null)
        {
           sfx = GetComponent<SFX>();
        }
		if (LifeOF == LifeType.Player)
		{
			MaxLife = LifeQuant;
			PlayerNumber = GetComponent<PlayerNumb>().PlayerNumber;
			//mudar a quantidade de vida para imagem aqui
			Division = 100;
			if (PlayerNumber == 1) {
				InitialX = 0.06f;
				X = InitialX;
			} else if (PlayerNumber == 2) {
				InitialX = 0.06f;
				X = 0.70f;
			}
		}
		ScreenGlass = GameObject.FindWithTag ("ScreenGlass");
		DollyCam = GameObject.FindWithTag ("DollyCam").GetComponent<CameraControl>();
		UpdateLife();
    }

    void Awake () {

        

     
    }

	void Update(){
		if (UpdateL1) {
			UpdateLife ();
			UpdateL1 = false;
		}

		if (QuantImgInScene < QuantImg && LifeOF == LifeType.Player) {
			UpdateLife ();
		}

		if (LifeOF == LifeType.Object) {
			UpdateLife ();
		}
	}

    
	public void UpdateLife () {
		switch (LifeOF) {
            case LifeType.Object:
                ObjectLife();
                break;
            case LifeType.Player:
			    PlayerLife ();
			    break;
		    case LifeType.Enemy:
			    EnemyLife();
			    break;
		    case LifeType.Boss:
			    BossLife();
			    break;
		}
	}

    void ObjectLife()
    {
        if (LifeQuant <= 0)
        {
			switch(TypeOfObject){
			case ObjectType.Box:
				GameObject GB = GameObject.Instantiate (ObjDestruido, transform.position, Quaternion.identity) as GameObject;
                Component[] RBGB;
                RBGB = GB.transform.GetComponentsInChildren<Rigidbody> ();
				foreach (Rigidbody rb in RBGB) {
					rb.velocity = gameObject.GetComponent<Rigidbody> ().velocity;
				}

                FMODUnity.RuntimeManager.PlayOneShot(Evento, transform.position);
                //if (sfx != null) {
                //sfx.PlaySoundSfxGrupo ("Caixa Quebrando");
                //}
                DropLoot();
                Destroy(gameObject);
                break;
			case ObjectType.Barricade:
				Instantiate (ObjDestruido, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;

            case ObjectType.Luz:
                if(gameObject.GetComponent<LuzQuebrando>() != null)
                {
                   gameObject.GetComponent<LuzQuebrando>().Quebrou = true;
                   FMODUnity.RuntimeManager.PlayOneShot(Evento, transform.position);
                   Destroy(this);
                }
                break;

             case ObjectType.Bomb:
                    GameObject Bomb = GameObject.Instantiate(ObjDestruido, transform.position, Quaternion.identity) as GameObject;
                    if (Evento != null)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(Evento, transform.position);
                    }
                    //if (sfx != null) {
                    //	sfx.PlaySoundSfxGrupo ("Caixa Quebrando");
                    //}
                    Destroy(gameObject);
                    break;
            }

            
            
        }
    }
    void DestroyObject()
    {
        
    }

	void PlayerLife(){
		if (LifeQuant <= Division) {
			//GetComponent<Movimentacao3D> ().CanMove = false;
			//GetComponent<Movimentacao3D> ().Stunned = true;
			if (!DollyCam.StunnedPlayers.Contains (gameObject))
				DollyCam.StunnedPlayers.Add (gameObject);
		}
		QuantImg = (int)LifeQuant / Division;
		if (QuantImgInScene < QuantImg) {
			GameObject gb = GameObject.Instantiate (LifeSpritePrefab,Camera.main.ViewportToWorldPoint(new Vector3(X,1.5f,1)),ScreenGlass.transform.rotation);
			gb.transform.SetParent (ScreenGlass.transform);
			QuantImgInScene++;
			gb.GetComponent<Rigidbody> ().useGravity = false;
			gb.GetComponent<Rigidbody> ().isKinematic = true;
			gb.gameObject.GetComponent<LifePos> ().PlayerNumber = PlayerNumber;
			gb.gameObject.GetComponent<LifePos> ().X = X;
			gb.gameObject.GetComponent<LifePos> ().Player = gameObject;
			gb.gameObject.GetComponent<ScaleLife> ().TotalTatuLife = Division;
			gb.gameObject.GetComponent<ScaleLife> ().TatuLife = Division;
			X += InitialX;
			ListOfImg.Add (gb);
		} else if (QuantImgInScene > QuantImg) {
			Destroy (ListOfImg [QuantImgInScene - 1],15);
			ListOfImg [QuantImgInScene - 1].transform.SetParent(null);
			ListOfImg [QuantImgInScene - 1].GetComponent<LifePos> ().enabled = false;
			ListOfImg [QuantImgInScene - 1].GetComponent<ScaleLife> ().dead = true;
			ListOfImg [QuantImgInScene - 1].GetComponent<Animator>().enabled = false;
			ListOfImg [QuantImgInScene - 1].GetComponent<Rigidbody> ().freezeRotation = false;
			ListOfImg [QuantImgInScene - 1].GetComponent<Rigidbody> ().isKinematic = false;
			ListOfImg [QuantImgInScene - 1].GetComponent<Rigidbody> ().useGravity = true;
			ListOfImg.RemoveAt (QuantImgInScene - 1);
			QuantImgInScene--;
			X -= InitialX;
		}

	}

	void EnemyLife(){
        switch (TypeEnemy)
        {
            case EnemyType.Mosquito:
               gameObject.GetComponent<FSMMosquito>().Life = LifeQuant;
                break;

            case EnemyType.Aranha:
                gameObject.GetComponent<FSMAranha>().Life = LifeQuant;
                break;

        }
        //ai.AI.WorkingMemory.SetItem("takingDamage", true);
        //ai.AI.WorkingMemory.SetItem("vida", LifeQuant);
    }

	void BossLife(){
	
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
				GameObject gb = GameObject.Instantiate(Loot[drop],transform.position,Quaternion.identity) as GameObject;
				gb.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				gb.GetComponent<Rigidbody> ().AddForce (gb.transform.up * 3500);
            }
        }
    }
}
