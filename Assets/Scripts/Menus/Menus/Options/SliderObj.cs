using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderObj : MonoBehaviour {

	[SerializeField] private Slider SliderToReference;
	[SerializeField] private AudioMixerGroup GroupToChange;
	[SerializeField] private float Test;
	[SerializeField] private float Divisor;
	private float XPos;
	void Start () {
		XPos = transform.localPosition.x;
		Test = SliderToReference.value;
		UpdateGroup ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (XPos + (SliderToReference.value / Divisor), transform.localPosition.y, transform.localPosition.z);
		Test = SliderToReference.value;
	}

	public void UpdateGroup(){
			GroupToChange.audioMixer.SetFloat (GroupToChange.name, Test);
	}
}
