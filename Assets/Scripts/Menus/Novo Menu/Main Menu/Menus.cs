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
            if (Menu.name == "Opções" && gameObject.name != "Sair Do Jogo")
            {
                Menu.SetActive(true);
                Menu.GetComponent<TesteTV>().MenuSelected = 0;
                Menu.GetComponent<TesteTV>().InMenuSelection = 0;
                Menu.GetComponent<TesteTV>().SetMenus();
                Menu.GetComponent<TesteTV>().SetColor();
                StartCoroutine(Menu.GetComponent<TesteTV>().SetCanChange());
            }else if(Menu.name == "Opções" && gameObject.name == "Sair Do Jogo")
            {
                Menu.SetActive(true);
                Menu.GetComponent<TesteTV>().MenuSelected = Menu.GetComponent<TesteTV>().Menus.Length - 1;
                Menu.GetComponent<TesteTV>().InMenuSelection = 0;
                Menu.GetComponent<TesteTV>().SetMenus();
                Menu.GetComponent<TesteTV>().SetColor();
                StartCoroutine(Menu.GetComponent<TesteTV>().SetCanChange());
            }
            
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
