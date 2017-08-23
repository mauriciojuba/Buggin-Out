using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGravity : MonoBehaviour {


	[SerializeField] private float FallMultiplier = 2.5f;
	[SerializeField] private float LowJumpMultiplayer = 2;
	[SerializeField] private Movimentacao3D Moviment;
	Rigidbody Rb;

	void Awake () {
		Rb = GetComponent<Rigidbody> ();
		Moviment = GetComponent<Movimentacao3D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Moviment != null) {
			if (!Moviment.onScreen) {
				if (Moviment != null) {
					if (Rb.velocity.y < 0 && Moviment) {
						Vector3 V3 = Rb.velocity;
						V3.y += Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
						Rb.velocity = V3;
					} else if (Rb.velocity.y > 0 && !Input.GetButton ("A P" + Moviment.PlayerNumber)) {
						Vector3 V3 = Rb.velocity;
						V3.y += Physics.gravity.y * (LowJumpMultiplayer - 1) * Time.deltaTime;
						Rb.velocity = V3;
					}
				} else {
					if (Rb.velocity.y < 0) {
						Vector3 V3 = Rb.velocity;
						V3.y += Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
						Rb.velocity = V3;
					} else if (Rb.velocity.y > 0 && !Input.GetButton ("A P1")) {
						Vector3 V3 = Rb.velocity;
						V3.y += Physics.gravity.y * (LowJumpMultiplayer - 1) * Time.deltaTime;
						Rb.velocity = V3;
					}

				}
			}
		}
	}
}
