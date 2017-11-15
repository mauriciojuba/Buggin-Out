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
    [SerializeField] GameObject[] Enemies;
    [SerializeField] GameObject Player1,Player2;
    float OldTime;

    [SerializeField] OLDTVFilter3 TVMenu;

    private void Start()
    {
        if (GameObject.FindWithTag("Player1_3D") != null)
        {
            Player1 = GameObject.FindWithTag("Player1_3D");
        }
        if (GameObject.FindWithTag("Player2_3D") != null)
        {
            Player2 = GameObject.FindWithTag("Player2_3D");
        }
        OldTime = 1.5f;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void SairDoJogo(){
		Application.Quit();
	}
	public void SelecaoPersonagem(){
        //////colocar função pra voltar pra seleção aqui
        //gamePaused = false;
        //Time.timeScale = OldTime;
        //SceneManager.LoadScene("Selecao Personagens 3D");
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
            PauseGame();
        }
		if(Input.GetButtonDown("PS4 Options") || Input.GetButtonDown("PS4 Share")){
			gamePaused = !gamePaused;
			pauseMenu.SetActive(gamePaused);
		}
	}

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        CheckEnemies();
        EnableDesableEnemies(gamePaused);
        pauseMenu.SetActive(gamePaused);
        if (gamePaused)
        {
            //Event.SetSelectedGameObject(ButtonSelect);
            StartCoroutine(Static());
        }
        else
        {
            //Event.SetSelectedGameObject(null);
            TVMenu.preset.staticFilter.staticMagnitude = 0;
            StopAllCoroutines();
        }
    }

    void CheckEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void EnableDesableEnemies(bool isDeactive)
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            //Desativa ou Ativa script do mosquito
            if (Enemies[i].GetComponent<IA_Mosquito>() != null)
            {
                Enemies[i].GetComponent<IA_Mosquito>().enabled = !isDeactive;
            }
            //Desativa ou Ativa script da aranha (trocar pela IA nova da aranha quando pronta)
            if (Enemies[i].GetComponent<FSMAranha>() != null)
            {
                Enemies[i].GetComponent<FSMAranha>().enabled = !isDeactive;
            }
        }
        if (Player1 != null)
        {
            Player1.GetComponent<HornControl>().enabled = !isDeactive;
            Player1.GetComponent<ComboSystem>().enabled = !isDeactive;
            Player1.GetComponent<GrabObject>().enabled = !isDeactive;
        }
        if (Player2 != null)
        {
            Player2.GetComponent<HornControl>().enabled = !isDeactive;
            Player2.GetComponent<ComboSystem>().enabled = !isDeactive;
            Player2.GetComponent<GrabObject>().enabled = !isDeactive;
        }

        if (isDeactive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = OldTime;
        }
    }

    IEnumerator Static()
    {
        TVMenu.preset.staticFilter.staticMagnitude = Random.Range(0.05f, 0.15f);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.3f));
        TVMenu.preset.staticFilter.staticMagnitude = 0;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(5));
        StartCoroutine(Static());
    }
}
