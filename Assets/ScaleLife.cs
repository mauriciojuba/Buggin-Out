using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLife : MonoBehaviour {

	public float TatuLife,TotalTatuLife;
	public float ScaleSpeed;
	private bool SetScale;
	private float ScaleToSet;

	public bool dead;

	void Start () {
		
	}
	
	// Update is called once per frame

	void Update(){
		if (SetScale) {
			transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (ScaleToSet, ScaleToSet, ScaleToSet), ScaleSpeed * Time.deltaTime);
			if (transform.localScale == new Vector3 (ScaleToSet, ScaleToSet, ScaleToSet)) {
				SetScale = false;
			}
		}
		if(dead){
			SetScale = false;
			transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(6,6,6), 2f*Time.deltaTime);
		}
	}

	public void UpdateScaleLife () {
		if (TatuLife / TotalTatuLife <= 0.1f) {
			ScaleToSet = 1.4f;
			SetScale = true;		
		} else if (TatuLife / TotalTatuLife <= 0.2f) {
			ScaleToSet = 1.46f;
			SetScale = true;		
		} else if (TatuLife / TotalTatuLife <= 0.3f) {
			ScaleToSet = 1.52f;
			SetScale = true;		
		} else if (TatuLife / TotalTatuLife <= 0.4f) {
			ScaleToSet = 1.58f;
			SetScale = true;
		} else if (TatuLife / TotalTatuLife <= 0.5f) {
			ScaleToSet = 1.64f;
			SetScale = true;		
		} else if (TatuLife / TotalTatuLife <= 0.6f) {
			ScaleToSet = 1.70f;
			SetScale = true;
		} else if (TatuLife / TotalTatuLife <= 0.7f) {
			ScaleToSet = 1.76f;
			SetScale = true;
		} else if (TatuLife / TotalTatuLife <= 0.8f) {
			ScaleToSet = 1.82f;
			SetScale = true;
		} else if (TatuLife / TotalTatuLife <= 0.9f) {
			ScaleToSet = 1.9f;
			SetScale = true;
		}
	}
}
