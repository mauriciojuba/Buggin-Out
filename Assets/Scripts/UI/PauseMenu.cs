using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

	public static bool gamePaused;

	public GameObject pauseMenu;

	[SerializeField] private EventSystem Event;
	[SerializeField] private GameObject ButtonSelect;
    float OldTime;

    private void Start()
    {
        OldTime = 1.5f;
    }

    public void SairDoJogo(){
		Application.Quit();
	}
	public void SelecaoPersonagem(){
		gamePaused = false;
		Time.timeScale = OldTime;
		SceneManager.LoadScene("Selecao Personagens 3D");
	}
	public void MainMenu(){
		gamePaused = false;
		Time.timeScale = OldTime;
		SceneManager.LoadScene("Main Menu");
	}

	public void TemCerteza(string Menu){
		Time.timeScale = OldTime;
	}

	void Update(){
		if(Input.GetButtonDown("Start P1") || Input.GetButtonDown("Start P2") || Input.GetKeyDown(KeyCode.P)){
			gamePaused = !gamePaused;
			pauseMenu.SetActive(gamePaused);
			if (gamePaused) {
				Event.SetSelectedGameObject (ButtonSelect);
			} else {
				Event.SetSelectedGameObject (null);
			}
		}
		if(Input.GetButtonDown("PS4 Options") || Input.GetButtonDown("PS4 Share")){
			gamePaused = !gamePaused;
			pauseMenu.SetActive(gamePaused);
		}

		if (gamePaused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = OldTime;
		}
	}
}
