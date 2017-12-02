using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGravity : MonoBehaviour {


	[SerializeField] private float FallMultiplier = 2.5f;
	[SerializeField] private float LowJumpMultiplayer = 2;
	[SerializeField] private HornControl Moviment;
	public int PlayerNumber;
	[SerializeField] private float MaxJump, JumpForce;



	private AnimationControl AnimCTRL;
	private bool Jumping;
	LayerMask NoIgnoredLayers = -1;
	private bool InGround;
	Rigidbody Rb;

	void Awake () {
		AnimCTRL = GetComponent<AnimationControl> ();
		Rb = GetComponent<Rigidbody> ();
		Moviment = GetComponent<HornControl> ();
        PlayerNumber = GetComponent<PlayerNumb>().PlayerNumber;
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (Moviment != null) {
			if (!Moviment.natela) {
				/*
				if (Rb.velocity.y < 0 && !InGround) {
					Vector3 V3 = Rb.velocity;
					V3.y += Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
					Rb.velocity = V3;
				} else if (Rb.velocity.y > 0 && !Input.GetButton ("A P" + PlayerNumber)) {
					Vector3 V3 = Rb.velocity;
					V3.y += Physics.gravity.y * (LowJumpMultiplayer - 1) * Time.deltaTime;
					Rb.velocity = V3;
				}
				*/
			}
		}
		Jump ();
	}

	void Jump(){
		//verifica se o player esta encostando no chão
		InGround = Physics.Linecast (transform.position, transform.position - Vector3.up * 1.1f, NoIgnoredLayers);
		Debug.DrawLine (transform.position,transform.position - Vector3.up);
		//se estiver no chao, pula, apertando A no controle.
		AnimCTRL.SetGrounded(InGround);
		if (Input.GetButtonDown ("A P" + PlayerNumber) && InGround && !Moviment.natela || Input.GetButtonDown("Jump") && InGround && !Moviment.natela) {

			Jumping = true;
			AnimCTRL.SetJumpAnim ();
			Vector3 V3 = Rb.velocity;
			V3.y = JumpForce;
			Rb.velocity = V3;
		}
		if (Input.GetButtonUp ("A P" + PlayerNumber) || Input.GetButtonUp("Jump")) {
			Jumping = false;
		}
//			}

		if (Jumping && !Moviment.natela) {
			Vector3 V3 = Rb.velocity;
			V3.y += JumpForce;
			Rb.velocity = V3;


		}
		if (Rb.velocity.y > MaxJump && !Moviment.natela) {
			Jumping = false;
		}
	}
}
