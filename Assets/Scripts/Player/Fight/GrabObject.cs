using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

	[SerializeField] float MinDist;
	[SerializeField] Transform PointPick;
	[SerializeField] float Force;
	GameObject BoxProximity;
	bool PickUp, PickedObj;
    [SerializeField] GameObject[] Boxs;
  
	AnimationControl AnimCTRL;
	Quaternion targetRotation;
    [Space(20)]
    [Header("Mira")]
    [SerializeField] List<GameObject> Enemies;
    [SerializeField] List<GameObject> ProximityEnemies;
    [SerializeField] List<GameObject> EnemiesInVision;
    [SerializeField] GameObject TargetEnemy;
    [SerializeField] bool Aim;
    [SerializeField] int AtualTarget;
    [SerializeField] Transform Muzzle;
    [SerializeField] int PlayerNumber;
	void Start () {
		Boxs = GameObject.FindGameObjectsWithTag ("Box");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            if (!Enemies.Contains(GameObject.FindGameObjectsWithTag("Enemy")[i]))
                Enemies.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);
        }
        PlayerNumber = GetComponent<PlayerNumb>().PlayerNumber;
		AnimCTRL = gameObject.GetComponent<AnimationControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("B P" + PlayerNumber) && !PickUp && !PickedObj || Input.GetKeyDown(KeyCode.L) && !PickUp && !PickedObj) {
			CheckAllDistance ();
			CheckDistance ();
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                if(!Enemies.Contains(GameObject.FindGameObjectsWithTag("Enemy")[i]))
                Enemies.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);
            }
            ProximityEnemies.Clear();
            EnemiesInVision.Clear();
        }

        if (Input.GetButtonDown ("B P" + PlayerNumber) && PickedObj || Input.GetKeyDown(KeyCode.L) && PickedObj) {
			AnimCTRL.ThrowObjAnim ();
		}

		if (PickUp) {
			RotateToObject ();
		}

        if (PickedObj)
        {
            CheckEnemies();
            if(EnemiesInVision.Count > 0)
            {
                if(Input.GetAxis("Right Analog Horizontal P" + PlayerNumber) > 0.5f)
                {
                    
                    if (AtualTarget < EnemiesInVision.Count)
                    {
                        AtualTarget++;
                    }
                    else
                        AtualTarget = 0;
                }

                if (Input.GetAxis("Right Analog Horizontal P" + PlayerNumber) < -0.5f)
                {
                    if (AtualTarget > 0)
                    {
                        AtualTarget--;
                    }
                    else
                        AtualTarget = EnemiesInVision.Count;
                }
            }
        }

        if (Aim)
        {
            if(TargetEnemy != null)
            Muzzle.LookAt(TargetEnemy.transform.position);
        }

    }

    private void FixedUpdate()
    {
        if (PickedObj)
        {
            if (EnemiesInVision.Count > 0)
            {
                TargetEnemy = EnemiesInVision[AtualTarget];
            }
            else
            {
                TargetEnemy = null;
            }
        }

        if (TargetEnemy != null)
        {
            Aim = true;
        }
        else
        {
            Aim = false;
        }
    }
    void CheckAllDistance(){
		Boxs = GameObject.FindGameObjectsWithTag ("Box");
		if (Boxs.Length > 0) {
			for (int i = 0; i < Boxs.Length; i++) {
				if (BoxProximity == null) {
					BoxProximity = Boxs [i];
				}
				if (Vector3.Distance (transform.position, Boxs [i].transform.position) < Vector3.Distance (transform.position, BoxProximity.transform.position)) {
					BoxProximity = Boxs [i];
				}
			}
		} else
			BoxProximity = null;
	}

	void CheckDistance(){
		if (BoxProximity != null) {
			if (Vector3.Distance (transform.position, BoxProximity.transform.position) < MinDist) {
				targetRotation = Quaternion.LookRotation (new Vector3 (BoxProximity.transform.position.x, BoxProximity.transform.position.y - 0.668f, BoxProximity.transform.position.z) - transform.position);
				PickUp = true;
				gameObject.GetComponent<HornControl> ().CanMove = false;
                gameObject.GetComponent<HornControl>().mov = Vector3.zero;
                gameObject.GetComponent<AnimationControl>().DesactiveRightFightCollider();
                gameObject.GetComponent<AnimationControl>().DesactiveLeftFightCollider();
                Debug.LogWarning ("Pode Pegar");
			}
		}
	}

	void RotateToObject(){
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
		if(Mathf.Abs(transform.rotation.y) >= Mathf.Abs(targetRotation.y) - 0.001f && Mathf.Abs(transform.rotation.y) <= Mathf.Abs(targetRotation.y) + 0.001f) {
			PickUp = false;
			PickedObj = true;
			//Ativa Animação de pegarObjeto
			AnimCTRL.PickObjAnimation();
		}
	}

	void PickUpObj(){
		BoxProximity.GetComponent<Rigidbody> ().isKinematic = true;
		BoxProximity.GetComponent<Collider> ().isTrigger = true;
		BoxProximity.transform.position = PointPick.position;
		gameObject.GetComponent<HornControl> ().CanMove = true;
		BoxProximity.transform.SetParent (PointPick);
	}

	void ThrowObj(){
		BoxProximity.transform.SetParent (null);
		StartCoroutine (BoxProximity.GetComponent<DestruirObjeto> ().ActiveCol ());
		BoxProximity.GetComponent<Rigidbody> ().isKinematic = false;
        if (Aim)
        {
            BoxProximity.GetComponent<Rigidbody>().useGravity = false;
            BoxProximity.transform.position = Muzzle.position;
            BoxProximity.transform.rotation = Muzzle.rotation;
            BoxProximity.GetComponent<Rigidbody>().AddForce(Muzzle.forward * Force);
        }
        else
        {
            BoxProximity.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * Force);
        }
        PickedObj = false;
        BoxProximity.GetComponent<DestruirObjeto> ().Throwed = true;
        TargetEnemy = null;
    }

    void CheckEnemies()
    {

        if (Enemies.Count > 0)
        {
            for(int i = 0; i < Enemies.Count; i++)
            {
                if(Enemies[i] == null)
                {
                    Enemies.RemoveAt(i);
                }

                if (Vector3.Distance(transform.position, Enemies[i].transform.position) < 20)
                {
                    if (!ProximityEnemies.Contains(Enemies[i]))
                    {
                        ProximityEnemies.Add(Enemies[i]);
                    }
                }
            }

            for (int i = 0; i < EnemiesInVision.Count; i++)
            {
                if (EnemiesInVision[i] == null)
                {
                    EnemiesInVision.RemoveAt(i);
                }
            }

            for (int i = 0; i < ProximityEnemies.Count; i++)
            {
                if (ProximityEnemies[i] == null)
                {
                    ProximityEnemies.RemoveAt(i);
                }

                Vector3 Dir = ProximityEnemies[i].transform.position - transform.position;
                float angle = Vector3.Angle(Dir, transform.forward);

                if (angle < 40)
                {
                    if (!EnemiesInVision.Contains(ProximityEnemies[i]))
                        EnemiesInVision.Add(ProximityEnemies[i]);
                }
                else
                {
                    if (EnemiesInVision.Contains(ProximityEnemies[i]))
                    {
                        EnemiesInVision.Remove(ProximityEnemies[i]);
                    }
                }

                
            }

            
        }

    }

}
