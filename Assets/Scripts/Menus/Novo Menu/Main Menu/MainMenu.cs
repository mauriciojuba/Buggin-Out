using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject Main, Options, Credits, BlackScreen;
    [SerializeField] bool GoCam;

    private void Update()
    {
        if (GoCam)
        {
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
    }

    public void ActiveMenu(string Menu)
    {
        StartCoroutine(TimeToActive(Menu));
    }

    public void DesactiveMenu(string Menu)
    {
        if (Menu == "Main Menu")
        {
            Main.SetActive(false);
        }
        if (Menu == "Options")
        {
            Options.SetActive(false);
        }
        if (Menu == "Credits")
        {
            Credits.SetActive(false);
        }
    }

    public void ActiveCam()
    {
        GoCam = true;
        StartCoroutine(TimeCam());
    }

    IEnumerator TimeCam()
    {
        yield return new WaitForSeconds(1.3f);
        GoCam = false;
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Selecao");

        while (!async.isDone)
        {
           yield return null;
        }

    }

    IEnumerator TimeToActive(string Menu)
    {
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        BlackScreen.SetActive(false);
        if (Menu == "Main Menu")
        {
            Main.SetActive(true);
        }
        if (Menu == "Options")
        {
            Options.SetActive(true);
        }
        if (Menu == "Credits")
        {
            Credits.SetActive(true);
        }
    }


}
