using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Pause : MonoBehaviour {

	private GameObject Player1_3D, Player2_3D, Player1_2D, Player2_2D;
	[SerializeField] private bool Paused;
	[SerializeField] private GameObject PausedScreen, FirstBTN;
	[SerializeField] private EventSystem _Events;
	[SerializeField] private StandaloneInputModule _Inputs;
	[SerializeField] private Menu PauseMenu;
	[SerializeField] private Image PauseColor;
	private Menu CurrentMenu;
    float OldTime;

	void Start () {
        OldTime = 1.5f;
		if(GameObject.FindWithTag ("Player1_3D") != null)
			Player1_3D = GameObject.FindWithTag ("Player1_3D");
		if(GameObject.FindWithTag ("Player2_3D") != null)
			Player2_3D = GameObject.FindWithTag ("Player2_3D");
		if(GameObject.FindWithTag ("Player1_2D") != null)
			Player1_2D = GameObject.FindWithTag ("Player1_2D");
		if(GameObject.FindWithTag ("Player2_2D") != null)
			Player2_2D = GameObject.FindWithTag ("Player2_2D");

		Color c = new Color ();
		c.a = 0;
		PauseColor.color = c;
		PausedScreen.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Start P1")) {
			PauseGame ();
			Events (1);
		}
		if (Input.GetButtonDown ("Start P2")) {
			PauseGame ();
			Events (2);
		}
	}

	void PauseGame(){
		Paused = !Paused;
		PausedScreen.SetActive (Paused);
		if (Player1_3D != null) {
			Player1_3D.GetComponent<Movimentacao3D> ().enabled = !Paused;
			Player1_3D.GetComponent<ChangeEnemyStance> ().enabled = !Paused;
		}
		if (Player1_2D != null)
			Player1_2D.GetComponent<Movimentacao2D> ().enabled = !Paused;
		if (Player2_3D != null) {
			Player2_3D.GetComponent<Movimentacao3D> ().enabled = !Paused;
			Player2_3D.GetComponent<ChangeEnemyStance> ().enabled = !Paused;
		}
		if (Player2_2D != null)
			Player2_2D.GetComponent<Movimentacao2D> ().enabled = !Paused;

		if (Paused) {
			Color c = new Color ();
			c.a = 0.5f;
			PauseColor.color = c;
			Time.timeScale = 0;
		} else {
			Color c = new Color ();
			c.a = 0;
			PauseColor.color = c;
			Time.timeScale = OldTime;
		}
		if (gameObject.GetComponent<MenuManager> ().CurrentMenu != null)
			gameObject.GetComponent<MenuManager> ().CurrentMenu.IsOpen = false;
		PauseMenu.IsOpen = Paused;
		gameObject.GetComponent<MenuManager> ().CurrentMenu = PauseMenu;
		
	
	}

	void Events(int i){
		_Inputs.horizontalAxis = "Horizontal P" + i;
		_Inputs.verticalAxis = "Vertical P" + i;
		_Inputs.submitButton = "A P" + i;
		_Inputs.cancelButton = "B P" + i;
		StartCoroutine (SetFirstSelect (FirstBTN));
	}

	public void SetButton(Button btn){
		StartCoroutine (SetFirstSelect (btn.gameObject));
	}

	IEnumerator SetFirstSelect(GameObject Btn){
		yield return new WaitForSeconds (0.2f);
		_Events.SetSelectedGameObject (Btn);
	}
}
