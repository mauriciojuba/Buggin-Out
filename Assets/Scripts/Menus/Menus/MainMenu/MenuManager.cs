using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public Menu CurrentMenu;

	void Start(){
		//mostra o primeiro menu selecionado
		if (CurrentMenu != null)
			ShowMenu (CurrentMenu);
	}

	//função para mostrar o menu, e ativar as animaçoes do mesmo.
	public void ShowMenu (Menu menu){
		if (CurrentMenu != null)
			CurrentMenu.IsOpen = false;

		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}
}
