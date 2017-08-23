using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour {

	//Menu Inicial, script apenas para a primeira tela, onde esta escrito pressione qualquer botão.

	private bool Active;
	[Header("Menu Manager")]
	private MenuManager Menu;
	[SerializeField] private Menu MainMenu;
	[SerializeField] private GameObject Cursor;

	void Start () {
		Time.timeScale = 1;
		Cursor.SetActive (false);
		Menu = gameObject.GetComponent<MenuManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!Active) {
			Cursor.SetActive (false);
			if (Input.anyKeyDown) {
				Cursor.SetActive (true);
				Menu.ShowMenu (MainMenu);
				Active = true;
			}
		}
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
