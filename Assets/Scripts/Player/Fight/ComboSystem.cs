﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour {

	[SerializeField] float TimeBetweenButtons = 0.4f;
	float TimeLastButtonPressed;
	int PlayerNumber;
	bool Attacking;
	[SerializeField] Animator Anim;
	[SerializeField] HornControl SetAnim;

	void Start () {
		PlayerNumber = GetComponent<PlayerNumb> ().PlayerNumber;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > TimeLastButtonPressed + TimeBetweenButtons) {
			Attacking = false;
		}
		if (Input.GetButtonDown ("X P" + PlayerNumber)) {
			ButtonPressed (1);
		}
		if (Input.GetButtonDown ("Y P" + PlayerNumber)) {
			ButtonPressed (2);
		}
		Anim.SetBool ("Attacking", Attacking);

	}


	void ButtonPressed(int IndexCombo){
		Attacking = true;
		TimeLastButtonPressed = Time.time;
		SetAnim.SetAttackAnim (IndexCombo);
	}
}