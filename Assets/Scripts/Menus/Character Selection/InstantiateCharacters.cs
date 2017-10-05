using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCharacters : MonoBehaviour {

	//variaveis para buscar no codigo de dados.
	public GameObject P1Model,P2Model;
	public Transform P1_Pos,P2_Pos;
	private GameObject P1,P2;
	public Data DataS;

	void Awake() {
		if (GameObject.FindWithTag ("DATA") != null) {
			DataS = GameObject.FindWithTag ("DATA").GetComponent<Data> ();
	
			//pega o modelo de personagem que o jogador 1 escolheu.
			if (DataS.P1SelectedCharacter != null)
				P1Model = DataS.P1SelectedCharacter.CharacterModel;
			//pega o modelo de personagem que o jogador 2 escolheu.
			if (DataS.P2SelectedCharacter != null)
				P2Model = DataS.P2SelectedCharacter.CharacterModel;


			//instancia o modelo de personagem do jogador 1.
			P1 = GameObject.Instantiate (P1Model, P1_Pos.transform.position, P1_Pos.transform.rotation);
			//define o nome do personagem
			P1.name = DataS.P1SelectedCharacter.CharacterName;
            P1.GetComponent<PlayerNumb>().PlayerNumber = DataS.P1SelectedCharacter.PlayerNumber;
            P1.GetComponent<PlayerLife>().LifeEmblem = DataS.P1SelectedCharacter.LifeEmblem;
            P1.GetComponent<PlayerLife>().MaxLife = DataS.P1SelectedCharacter.Life;
			//ativa o script de movimentacao 3D para o jogador 1
			//P1.transform.Find ("3D_Player").gameObject.GetComponent<PlayerNumb> ().PlayerNumber = DataS.P1SelectedCharacter.PlayerNumber;
			//P1.transform.Find ("3D_Player").gameObject.GetComponent<Life> ().LifeQuant = DataS.P1SelectedCharacter.Life;
			//P1.transform.Find ("3D_Player").gameObject.GetComponent<Life> ().LifeOF = Life.LifeType.Player;
			//P1.transform.Find ("3D_Player").gameObject.GetComponent<UseSpecial> ().SpecialRef = DataS.P1SelectedCharacter.Special;
			//P1.transform.Find ("3D_Player").gameObject.GetComponent<Life> ().LifeSpritePrefab = DataS.P1Life;
			//P1.transform.Find ("3D_Player").tag = "Player1_3D";

			if (P2Model != null) {
				//instancia o modelo de personagem do jogador 2.
				P2 = GameObject.Instantiate (P2Model, P2_Pos.transform.position, P2_Pos.transform.rotation);
				//define o nome do personagem
				P2.name = DataS.P2SelectedCharacter.CharacterName;
                P2.GetComponent<PlayerNumb>().PlayerNumber = DataS.P2SelectedCharacter.PlayerNumber;
                P2.GetComponent<PlayerLife>().LifeEmblem = DataS.P2SelectedCharacter.LifeEmblem;
                P2.GetComponent<PlayerLife>().MaxLife = DataS.P2SelectedCharacter.Life;
                //ativa o script de movimentacao 3D para o jogador 1
                //P2.transform.Find ("3D_Player").gameObject.GetComponent<PlayerNumb> ().PlayerNumber = DataS.P2SelectedCharacter.PlayerNumber;
                //P2.transform.Find ("3D_Player").gameObject.GetComponent<Life> ().LifeQuant = DataS.P2SelectedCharacter.Life;
                //P2.transform.Find ("3D_Player").gameObject.GetComponent<Life> ().LifeOF = Life.LifeType.Player;
                //P2.transform.Find ("3D_Player").gameObject.GetComponent<UseSpecial> ().SpecialRef = DataS.P2SelectedCharacter.Special;
                //P2.transform.Find ("3D_Player").gameObject.GetComponent<Life> ().LifeSpritePrefab = DataS.P2Life;
                //P2.transform.Find ("3D_Player").tag = "Player2_3D";			
            }
		}
	}		
}
