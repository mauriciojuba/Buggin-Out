﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] List<GameObject> Menus;
    [SerializeField] List<GameObject> MenuPos;

    [Header("Configuração seleção de fases")]
    [SerializeField] GameObject[] AllObjs;
    [SerializeField] GameObject[] AllObjsPos;

    [SerializeField] List<GameObject> UnlockedObjs;
    [SerializeField] List<GameObject> UnlockedObjsPos;
    Data DataS;
    bool Selected;

    [SerializeField] int PreSelected;
    [SerializeField] int PlayerNumber;
    [SerializeField] bool CanChange, inMenu;


    [SerializeField] bool SelectPhase, MenuMain;

    [SerializeField] GameObject Pedestal;

    private void Start()
    {
        if (GameObject.FindWithTag("DATA") != null)
            DataS = GameObject.FindWithTag("DATA").GetComponent<Data>();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main")
        {
            MenuMain = true;
            SelectPhase = false;
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SelecaoFase")
        {
           // MenuMain = false;
            //SelectPhase = true;
        }

        if (SelectPhase)
        {
            if (DataS.CompletedPhases.Contains(1))
            {
                if (!Menus.Contains(AllObjs[1]))
                {
                    Menus.Add(AllObjs[1]);
                }
                if (!MenuPos.Contains(AllObjsPos[1]))
                {
                    MenuPos.Add(AllObjsPos[1]);
                }
            }
        }

    }

    private void Update()
    {
        if (Camera.main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude <= 0)
        {
            if (!CanChange && !inMenu)
            {
                if (ChangePosition())
                {
                    CanChange = true;
                }
            }
            if (Input.GetAxis("Horizontal P" + PlayerNumber) > 0.5f && CanChange)
            {
                NextSelection();
            }
            if (Input.GetAxis("Horizontal P" + PlayerNumber) < -0.5f && CanChange)
            {
                PreviousSelection();
            }

            if (Input.GetButtonDown("A P" + PlayerNumber) && !inMenu && !Selected)
            {
                if (MenuMain)
                    ActiveMenu();
                else if (SelectPhase)
                    OnSelectPhase();
            }


            if (Input.GetButtonDown("B P" + PlayerNumber) && inMenu)
            {
                DeactiveMenu();
            }
        }
    }

    public void NextSelection()
    {
        CanChange = false;

        if (PreSelected < MenuPos.Count - 1)
            PreSelected++;
        else if (PreSelected >= MenuPos.Count - 1)
        {
            PreSelected = 0;
        }
        
    }

    public void PreviousSelection()
    {
        CanChange = false;
        if (PreSelected > 0)
            PreSelected--;
        else if (PreSelected <= 0)
        {
            PreSelected = MenuPos.Count - 1;
        }
 
    }

    bool ChangePosition()
    {
            Pedestal.transform.eulerAngles = Vector3.Lerp(Pedestal.transform.eulerAngles, MenuPos[PreSelected].transform.eulerAngles, Time.deltaTime * 5);
            if (Pedestal.transform.eulerAngles.y >= MenuPos[PreSelected].transform.eulerAngles.y - 1 && Pedestal.transform.eulerAngles.y <= MenuPos[PreSelected].transform.eulerAngles.y + 1)
            {
                Pedestal.transform.eulerAngles = MenuPos[PreSelected].transform.eulerAngles;
                return true;
            }
            else
            {
                return false;
            }
       
    }

    public void ActiveMenu()
    {
        Menus[PreSelected].GetComponent<Menus>().ActiveMenu();
        CanChange = false;
        inMenu = true;
    }

    public void DeactiveMenu()
    {
        Menus[PreSelected].GetComponent<Menus>().DeactiveMenu();
        CanChange = true;
        inMenu = false;
    }

    public void OnSelectPhase()
    {
        CanChange = false;
        Selected = true;
        DataS.PhaseName = Menus[PreSelected].GetComponent<Menus>().NamePhase;
        if (GameObject.FindWithTag("Loading") != null)
            GameObject.FindWithTag("Loading").GetComponent<Loading>().StartCoroutine(GameObject.FindWithTag("Loading").GetComponent<Loading>().LoadAsync("SelecaoPersonagem"));
    }
}
