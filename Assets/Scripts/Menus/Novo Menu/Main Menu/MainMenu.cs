using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject[] Menus;
    [SerializeField] GameObject[] MenuPos;
    [SerializeField] int PreSelected;
    [SerializeField] int PlayerNumber;
    [SerializeField] bool CanChange, inMenu;

    private void Update()
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
    }

    public void NextSelection()
    {
        CanChange = false;
        if (PreSelected < MenuPos.Length - 1)
            PreSelected++;
        else if (PreSelected >= MenuPos.Length - 1)
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
            PreSelected = MenuPos.Length - 1;
        }
    }

    bool ChangePosition()
    {
        transform.position = Vector3.Lerp(transform.position, MenuPos[PreSelected].transform.position, Time.deltaTime * 15);
        if (transform.position == MenuPos[PreSelected].transform.position)
        {
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
        inMenu = true;
    }

    public void DesactiveMenu()
    {
        Menus[PreSelected].GetComponent<Menus>().DeactiveMenu();
        inMenu = false;
    }
}
