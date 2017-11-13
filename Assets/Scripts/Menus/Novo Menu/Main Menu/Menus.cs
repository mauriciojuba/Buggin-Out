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
            if (Menu.name == "Opções")
            {
                Menu.GetComponent<TesteTV>().InMenuSelection = 0;
                Menu.GetComponent<TesteTV>().SetColor();
            }
            Menu.SetActive(true);
        }
    }

    public void DeactiveMenu()
    {
        Menu.SetActive(false);
    }

    public void NewGame()
    {
        GameObject.FindWithTag("Loading").GetComponent<Loading>().StartCoroutine(GameObject.FindWithTag("Loading").GetComponent<Loading>().LoadAsync("SelecaoFase"));
    }
}
