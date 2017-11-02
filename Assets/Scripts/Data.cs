using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

	//lista de fases completas.
	public List<int> CompletedPhases;
    public List<string> UnlockedCharacter;
	private GameObject[] Datas;

	public string PhaseName;

	public Character P1SelectedCharacter;
	public Character P2SelectedCharacter;
	public Character P3SelectedCharacter;
	public Character P4SelectedCharacter;

	public GameObject P1Life, P2Life;

	void Awake(){
		//procura todas os objetos de Dados do jogo e coloca no Array.
		Datas = GameObject.FindGameObjectsWithTag ("DATA");
		//se tiver mais de um objeto de dados, ele destroi o objeto mais recente, mantendo apenas 1.
		if (Datas.Length >= 2) {
			Destroy (Datas [1]);
		}

		DontDestroyOnLoad (this);
	}
}
