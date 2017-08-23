using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDialogue : MonoBehaviour {

	//script apenas para definir se o dialogo esta ativo ou não.
	public bool DialogueActive;
	[SerializeField] private GameObject NPCDialogueBox;

	void Update () {
		NPCDialogueBox.SetActive (DialogueActive);
	}
}
