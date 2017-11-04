using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    [SerializeField] GameObject Menu;
    [SerializeField] bool StartNewGame;
    public string NamePhase;

    public void ActiveMenu()
    {
        if (StartNewGame)
        {
            NewGame();
        }
        else
        {
            Menu.SetActive(true);
        }
    }

    public void DeactiveMenu()
    {
        Menu.SetActive(false);
    }

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SelecaoFase");
    }
}
