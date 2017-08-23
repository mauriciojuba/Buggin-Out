using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreditsTest : MonoBehaviour {

	[System.Serializable]
	public class CreditLine {
		public string   area;
		public string[] authors;
		public CreditLine() { }
	}

	public CreditLine[] creditsElements;

	public int AreaFontSize;
	public Color AreaFontColor;

	public int AuthorFontSize;
	public Color AuthorFontColor;

	public GameObject TextPrefab;

	public List<GameObject> AreaText;
	public List<GameObject> AuthorText;

	public int LastUsed;

	private Vector3 Posinit;

	void Awake(){

		for (int i = 0; i < creditsElements.Length; i++){
			GameObject newAreaText = Instantiate(TextPrefab);
			newAreaText.transform.SetParent(this.transform);
			AreaText.Add (newAreaText);
			for (int j = 0; j < creditsElements [i].authors.Length; j++) {
				GameObject newAuthorText = Instantiate(TextPrefab);
				newAuthorText.transform.SetParent(this.transform);
				AuthorText.Add (newAuthorText);
			}
		}

		for (int a = 0; a < creditsElements.Length; a++) {
			AreaText [a].GetComponent<Text> ().fontSize = AreaFontSize;
			AreaText [a].GetComponent<Text> ().color = AreaFontColor;
			AreaText [a].GetComponent<Text> ().text += creditsElements [a].area;
			for (int b = 0; b < creditsElements [a].authors.Length; b++) {
				AuthorText [LastUsed].GetComponent<Text> ().fontSize = AuthorFontSize;
				AuthorText [LastUsed].GetComponent<Text> ().color = AuthorFontColor;
				AuthorText [LastUsed].GetComponent<Text> ().text = creditsElements [a].authors [b];
				LastUsed++;
			}
		}
	}

	void Update(){
		
	}
}
