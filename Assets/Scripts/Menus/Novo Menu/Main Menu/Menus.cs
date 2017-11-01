using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    [SerializeField] GameObject Menu;

    public void ActiveMenu()
    {
        Menu.SetActive(true);
    }

    public void DeactiveMenu()
    {
        Menu.SetActive(false);
    }
}
