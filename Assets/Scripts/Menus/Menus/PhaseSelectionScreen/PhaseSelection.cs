using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhaseSelection : MonoBehaviour {

	[SerializeField] private List<Button> Phases;

	private Data DataS;

	void Start () {
		//busca o codigo de dados
		DataS = GameObject.FindWithTag ("DATA").GetComponent<Data> ();
		//consulta nos dados se ja foi passada a fase, para desbloquear a proxima.
		for (int i = 1; i < Phases.Count; i++) {
			if (DataS.CompletedPhases.Contains (i)) {
				Phases [i].interactable = true;
			} else {
				Phases [i].interactable = false;
			}
		}
	}

	//quando é selecionada a fase, registra o nome da fase nos dados para ser chamada em sequencia.
	public void PhaseSelected(string PhaseName){
		DataS.PhaseName = PhaseName;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Selecao Personagens");
	}
}
