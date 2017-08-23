using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	[System.Serializable]
	public class CreditLine {
		public string   area;
		public string[] authors;
		public CreditLine() { }
	}

	public Texture		 BackgroundCredits;
	public GUISkin       areaSkin;
	public GUISkin       authorSkin;
	public CreditLine[]  creditsElements;
	private float        py;
	private float        timer = 0;
	private float        halfWidth;
	private List<string> labelTexts;
	private List<float>  labelWidth;
	private List<float>  labelHeights;

	private Dictionary<string, string[]> creditDictionary;


	public void Awake() {
		py        = Screen.height;
		halfWidth = Screen.width * 0.5F;

		if (areaSkin == null)
			areaSkin = ScriptableObject.CreateInstance<GUISkin> ();

		if (authorSkin == null)
			authorSkin = ScriptableObject.CreateInstance<GUISkin> ();


		creditDictionary = new Dictionary<string, string[]>();
		for (int j = 0; j < creditsElements.Length; j++)
			creditDictionary.Add(creditsElements[j].area, creditsElements[j].authors);

		labelTexts   = new List<string>();
		labelWidth   = new List<float>();
		labelHeights = new List<float>();

		foreach (KeyValuePair<string, string[]> pair in creditDictionary) {
			labelTexts.Add(pair.Key);
			labelWidth.Add(pair.Key.Length * 30);
			labelHeights.Add(50);
			labelTexts.Add(System.String.Join("\n", pair.Value)); // Make Multiple Lines;
			labelWidth.Add(BiggestString(pair.Value) * 30);
			labelHeights.Add(pair.Value.Length * 50);
		}
	}

	private float BiggestString(string[] authors) {
		float len = 0;
		for (int i = 0; i < authors.Length; i++) {
			if (authors[i].Length > len) len = authors[i].Length;
		}
		return len;
	}


	public void Update() {
		timer += Time.deltaTime;
		if (timer >= 0.008F) {
			timer = 0;
			py -= 2;
		}

		if (Input.anyKey) SceneManager.LoadScene (1);
	}

	public void OnGUI() {
		float deltaY = 0;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), BackgroundCredits);
		for (int i = 0; i < labelTexts.Count; i++) {
			// Area
			if (i % 2 == 0) { 
				GUI.Label(new Rect(halfWidth - labelWidth[i] * 0.5F, py + deltaY, labelWidth[i], labelHeights[i]), labelTexts[i], areaSkin.label);
				deltaY += labelHeights[i] + 5;
				// Author, maybe multiple lines
			} else {
				GUI.Label(new Rect(halfWidth - labelWidth[i] * 0.5F, py + deltaY, labelWidth[i], labelHeights[i]), labelTexts[i], authorSkin.label);
				deltaY += labelHeights[i] + 50;
			}
		}

		if (py + deltaY < 0) SceneManager.LoadScene(1);
	}
}
