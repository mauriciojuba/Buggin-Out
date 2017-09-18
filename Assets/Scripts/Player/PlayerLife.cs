using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {

	public float Life;
	float AntLife;
	public float MaxLife;
	public Image LifeMask;

	void Start () {
		Life = MaxLife;
	}
	
	// Update is called once per frame
	void Update () {
		LifeMask.fillAmount = Life / MaxLife;
	}
}
