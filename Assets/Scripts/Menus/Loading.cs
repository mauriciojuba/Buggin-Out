using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{

    public bool LoadingGame;
    Camera Main;


    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    public ThreadPriority loadThreadPriority;

    AsyncOperation operation;
    public Scene currentScene;

    public static int sceneToLoad = -1;

    private GameObject[] Loadings;

    private void Awake()
    {
        Loadings = GameObject.FindGameObjectsWithTag("Loading");
        //se tiver mais de um objeto de dados, ele destroi o objeto mais recente, mantendo apenas 1.
        if (Loadings.Length >= 2)
        {
            Destroy(Loadings[1]);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (Main == null)
        {
            Main = Camera.main;
        }
        if (GameObject.FindWithTag("DollyCam") != null)
        {
            if (!GameObject.FindWithTag("DollyCam").GetComponent<CameraControl>().GameOver)
            {
                if (Main != null)
                {
                    if (LoadingGame && Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude <= 1)
                    {
                        Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude += Time.deltaTime;
                    }
                    else if (!LoadingGame && Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude > 0)
                    {
                        Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude -= Time.deltaTime;
                    }
                    if (Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude < 0)
                    {
                        Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude = 0;
                    }
                }
            }
        }else
        {
            if (Main != null)
            {
                if (LoadingGame && Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude <= 1)
                {
                    Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude += Time.deltaTime;
                }
                else if (!LoadingGame && Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude > 0)
                {
                    Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude -= Time.deltaTime;
                }
                if (Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude < 0)
                {
                    Main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude = 0;
                }
            }
        }
    }

    private void StartOperation(string levelNum)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;

    }

    public IEnumerator LoadAsync(string levelNum)
    {

        yield return null;

        LoadingGame = true;
        StartOperation(levelNum);


        // operation does not auto-activate scene, so it's stuck at 0.9
        while (DoneLoading() == false)
        {
            yield return null;

            
        }


        yield return new WaitForSeconds(1.2f);
        LoadingGame = false;


        if (loadSceneMode == LoadSceneMode.Additive)
            SceneManager.UnloadSceneAsync(currentScene.name);
        else
            operation.allowSceneActivation = true;
    }

    private bool DoneLoading()
    {
        return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

   
}
