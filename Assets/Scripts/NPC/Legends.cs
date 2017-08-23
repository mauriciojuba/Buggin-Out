using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Legends : MonoBehaviour {

	[Header("Linhas de legenda")]
	[SerializeField] private string[] Lines;
	[Header("Tempo para legenda")]
	[Tooltip("Tempo para cada linha de legenda, tempo [0] é para linha [0], etc..")]
	[SerializeField] private float[] TimeToShow;
	[Header("Velocidade de Scroll")]
	[Tooltip("Velocidade para cada linha de legenda, Velocidade [0] é para linha [0], etc..")]
	[SerializeField] private float[] ScrollSpeed;
	[Tooltip("Text do canvas para escrever a legenda")]
	[SerializeField] private Text TextSubtitle;
	private int ActualLine;


	void Start () {
		StartCoroutine (Scroll ());
	}

	//Rola as letras para o lado.
	IEnumerator Scroll(){
		string ActualText = "";
		for (int i = 0; i < Lines [ActualLine].Length; i++) {
			ActualText += Lines [ActualLine] [i];
			TextSubtitle.text = ActualText;
			yield return new WaitForSeconds (1 / ScrollSpeed[ActualLine]);
		}
		//tempo para a legenda ficar a mostra.
		yield return new WaitForSeconds (TimeToShow [ActualLine]);
		ActualLine++;
		StartCoroutine (Scroll ());
	}
}
