using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {

	[System.Serializable]
	public class Dialogues{
		public string NumberOfDialogue;
		public string[] Words;
	}

	[Header("Quantidade de dialogos do npc")]
	[SerializeField] private Dialogues[] QuantDialogues;
	//Image do Canvas
	[SerializeField] private Image NPCImage;
	//Sprite do NPC.
	[SerializeField] private Sprite NPCSprite;
	//Text do Canvas
	[SerializeField] private Text NPCText;
	//Para ativar o dialogo
	[SerializeField] private InDialogue DialogueActive;
	//Posição dos players
	[SerializeField] private Transform P1T, P2T, P3T, P4T;
	//Tempo para rolar as letars
	public float TimeScroll;
	//Numero da coluna e da linha de dialogo.
	public int ActualDialogue, ActualWords;
	//definir se esta em dialogo
	private bool InDialogue;
	//definir se esta rolando letras.
	private bool IsScrolling;
	//player mais proximo.
	[SerializeField] private Transform Target;

	[SerializeField] bool InRange;

	[SerializeField] ApplyCombo Combo;

	void Start () {

		//Registra os transforms dos players.
		if(GameObject.FindWithTag ("Player1_3D") != null)
			P1T = GameObject.FindWithTag ("Player1_3D").transform;
		if(GameObject.FindWithTag ("Player2_3D") != null)
			P2T = GameObject.FindWithTag ("Player2_3D").transform;
		if(GameObject.FindWithTag ("Player3_3D") != null)
			P3T = GameObject.FindWithTag ("Player3_3D").transform;
		if(GameObject.FindWithTag ("Player4_3D") != null)
			P4T = GameObject.FindWithTag ("Player4_3D").transform;
	}
	
	void Update () {
		//calcula qual player esta mais proximo
		if (!InDialogue) {
			Target = SelectTarget (P1T, P2T, P3T, P4T);
		}
		Combo = Target.GetComponent<ApplyCombo> ();
		Combo.enabled = !InRange;

			//para iniciar o dialogo, apertar X
//		if (Target.GetComponent<Movimentacao3D> ().KeyboardCanControl) {
//			if (Input.GetButtonDown ("X P" + Target.gameObject.GetComponent<Movimentacao3D> ().PlayerNumber) && InRange && !InDialogue || 
//				Input.GetKeyDown(KeyCode.J) && InRange && !InDialogue) {
//				//começa a rolar as letras e inicia o dialogo
//				Target.gameObject.GetComponent<Movimentacao3D> ().InDialogue = true;
//				Target.gameObject.GetComponent<Movimentacao3D> ().InMovement = false;
//				StartCoroutine (Scroll ());
//				InDialogue = true;
//				DialogueActive.DialogueActive = true;
//			}
//			if (Input.GetButtonDown ("A P" + Target.gameObject.GetComponent<Movimentacao3D> ().PlayerNumber) && InDialogue ||
//				Input.GetKeyDown(KeyCode.K) && InDialogue) {
//				//se as letras estiverem rolando e apertar A, mostra a frase inteira.
//				if (IsScrolling) {
//					NPCText.text = QuantDialogues [ActualDialogue].Words [ActualWords];
//					IsScrolling = false;
//				} else {
//					//se a linha que esta sendo mostrada nao for a ultima, volta a rolar as letras da proxima linha.
//					if (ActualWords < QuantDialogues [ActualDialogue].Words.Length - 1) {
//						ActualWords++;
//						NPCText.text = "";
//						StartCoroutine (Scroll ());
//					} else {
//						//se a linha que esta sendo mostrada for a ultima, desativa o dialogo.
//						Target.gameObject.GetComponent<Movimentacao3D> ().StartCoroutine (Target.gameObject.GetComponent<Movimentacao3D> ().SetDialogueFalse ());
//						ActualWords = 0;
//						NPCText.text = "";
//						InDialogue = false;
//						DialogueActive.DialogueActive = false;
//						//se caso tiver mais de um tipo de dialogo, ele passa para o proximo dialogo.
//						if (ActualDialogue < QuantDialogues.Length - 1) {
//							ActualDialogue++;
//						} else {
//							ActualDialogue = 0;
//						}
//					}
//				}
//			}
//		}else{
			if (Input.GetButtonDown ("X P" + Target.gameObject.GetComponent<Movimentacao3D> ().PlayerNumber) && InRange && !InDialogue) {
				//começa a rolar as letras e inicia o dialogo
				StartCoroutine (Scroll ());
				InDialogue = true;
				DialogueActive.DialogueActive = true;
				Target.gameObject.GetComponent<Movimentacao3D> ().InDialogue = true;
			}
			if (Input.GetButtonDown ("A P" + Target.gameObject.GetComponent<Movimentacao3D> ().PlayerNumber) && InDialogue) {
				//se as letras estiverem rolando e apertar A, mostra a frase inteira.
				if (IsScrolling) {
					NPCText.text = QuantDialogues [ActualDialogue].Words [ActualWords];
					IsScrolling = false;
				} else {
					//se a linha que esta sendo mostrada nao for a ultima, volta a rolar as letras da proxima linha.
					if (ActualWords < QuantDialogues [ActualDialogue].Words.Length - 1) {
						ActualWords++;
						NPCText.text = "";
						StartCoroutine (Scroll ());
					} else {
						//se a linha que esta sendo mostrada for a ultima, desativa o dialogo.
						Target.gameObject.GetComponent<Movimentacao3D> ().StartCoroutine (Target.gameObject.GetComponent<Movimentacao3D> ().SetDialogueFalse ());
						ActualWords = 0;
						NPCText.text = "";
						InDialogue = false;
						DialogueActive.DialogueActive = false;
						//se caso tiver mais de um tipo de dialogo, ele passa para o proximo dialogo.
						if (ActualDialogue < QuantDialogues.Length - 1) {
							ActualDialogue++;
						} else {
							ActualDialogue = 0;
						}
					}
				}
			}
		//}
	}

	Transform SelectTarget (Transform P1, Transform P2,Transform P3, Transform P4){
		if (P4 != null) {
			//Decide qual player esta mais perto para Iniciar o dialogo.
			if (Vector3.Distance (transform.position, P1.position) < Vector3.Distance (transform.position, P2.position)
				&& Vector3.Distance (transform.position, P1.position) < Vector3.Distance (transform.position, P3.position)
				&& Vector3.Distance (transform.position, P1.position) < Vector3.Distance (transform.position, P4.position)) {
				return P1;
			} else if (Vector3.Distance (transform.position, P2.position) < Vector3.Distance (transform.position, P3.position)
				&& Vector3.Distance (transform.position, P3.position) < Vector3.Distance (transform.position, P4.position)) {
				return P2;
			} else if (Vector3.Distance (transform.position, P3.position) < Vector3.Distance (transform.position, P4.position)) {
				return P3;
			} else {
				return P4;
			}
		} else if (P3 != null) {
			if (Vector3.Distance (transform.position, P1.position) < Vector3.Distance (transform.position, P2.position)
				&& Vector3.Distance (transform.position, P1.position) < Vector3.Distance (transform.position, P3.position)) {
				return P1;
			} else if (Vector3.Distance (transform.position, P2.position) < Vector3.Distance (transform.position, P3.position)) {
				return P2;
			} else {
				return P3;
			}
		} else if (P2 != null) {
			if (Vector3.Distance (transform.position, P1.position) < Vector3.Distance (transform.position, P2.position)) {
				return P1;
			} else {
				return P2;
			}
		} else
			return P1;
	}

	//sistema para fazer o rolamento de letras.
	IEnumerator Scroll(){
		IsScrolling = true;
		string ActualText = "";
		for (int i = 0; i < QuantDialogues [ActualDialogue].Words [ActualWords].Length; i++) {
			if (IsScrolling) {
				ActualText += QuantDialogues [ActualDialogue].Words [ActualWords] [i];
				NPCText.text = ActualText;
				yield return new WaitForSeconds (0.3f / TimeScroll);
			}
		}
		IsScrolling = false;
	}

	void OnTriggerEnter(Collider Col){
		if (Col.CompareTag (Target.tag)) {
			InRange = true;
		}
	}

	void OnTriggerExit(Collider Col){
		if (Col.CompareTag (Target.tag)) {
			InRange = false;
		}
	}
}
