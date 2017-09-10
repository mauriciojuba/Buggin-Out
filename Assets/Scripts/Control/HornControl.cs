using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornControl : MonoBehaviour {
    Vector3 mov;
    Rigidbody rdb;
	AnimationControl AnimCTRL;
    public GameObject cameragame;
    public Animator anim;
	public bool natela = false;
	public bool Going = false;
    public Transform telapos;
    int i=0;

	[SerializeField] GOToScreen Screen;
	[SerializeField] CameraControl DollyCam;
	// Use this for initialization
	void Start () {
		AnimCTRL = GetComponent<AnimationControl> ();
        rdb=GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(cameragame!=null)
        mov = cameragame.transform.TransformVector(mov);
        Vector3 Direction = new Vector3(rdb.velocity.x, 0, rdb.velocity.z);


		if (mov.magnitude > 0) {
			anim.SetLayerWeight (1, 1);
			anim.SetLayerWeight (2, 0);
		} else if (mov.magnitude <= 0) {
			anim.SetLayerWeight (2, 1);
			anim.SetLayerWeight (1, 0);
		}

        if (Direction.magnitude > 0.1f) {
			
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (Direction), Time.deltaTime * 5);
		}

//		if (Input.GetButtonDown ("X P1") || Input.GetKeyDown (KeyCode.LeftControl)) {
//			
//			anim.SetTrigger ("Attack");
//		}

		if (Input.GetButtonDown ("Jump") && !Going) {
			Going = true;
			anim.SetBool ("tocam", true);
			natela = !natela;
			rdb.isKinematic = true;
			DollyCam.playerOnScreen = natela;
		}

		if (natela)
        {
			Screen.GoToScreen (telapos,transform.gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector3 nvel = new Vector3(mov.x*5, rdb.velocity.y, mov.z*5);
        rdb.velocity = nvel ;
		AnimCTRL.SetMovimentAnimation(rdb.velocity.magnitude);

    }


//
//	public void SetAttackAnim(int AttackNumber){
//		if (anim == null) {
//			return;
//		}
//		anim.SetTrigger ("Attack");
//		anim.SetInteger ("AttackNumber", AttackNumber);
//	}
}
