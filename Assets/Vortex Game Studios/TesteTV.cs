using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesteTV : MonoBehaviour {

    [SerializeField] int PlayerNumber = 1;
    public GameObject[] Menus;
    [SerializeField] GameObject[] InMenus;
    public int MenuSelected;
    [SerializeField] public int InMenuSelection;
    [SerializeField] PauseMenu PauseS;
    [Header("Cores dos Menus(Temporario)")]
    [SerializeField] Color SelectedMenuColor;
    [SerializeField] Color UnselectedMenuColor;

    [Header("Cores das opções do menu")]
    [SerializeField] Color SelectedColor;
    [SerializeField] Color UnselectedColor;


    public Text _TextGraphics;
    public Text _TextBrightness;
    public Text _TextMasterVolume;

    int QualitySelected;

    int BrightnessNumber;
    string Brightness;

    int VolumeNumber;
    string MasterVolume;

    public string OptionSelected;

    [SerializeField] OLDTVFilter3 TVMenu;

    [SerializeField] GameObject Cursor;

    [SerializeField] bool CanChange = true;

    // Use this for initialization
    void Start () {
        CanChange = true;
        SetMenus();
        for (int i = 0; i < BrightnessNumber; i++)
        {
            Brightness += "/";
        }

        if (InMenus[InMenuSelection] != null && InMenus.Length > 0)
        {
            InMenus[InMenuSelection].GetComponent<Text>().color = SelectedColor;
            OptionSelected = InMenus[InMenuSelection].name;
        }
        for (int i = 0; i < Menus.Length; i++)
        {
            if (i != MenuSelected)
            {
                Menus[i].GetComponent<Image>().color = UnselectedMenuColor;
            }
            else
            {
                Menus[i].GetComponent<Image>().color = SelectedMenuColor;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        QualitySelected = QualitySettings.GetQualityLevel();
        _TextGraphics.text = QualitySettings.names[QualitySelected];
        _TextBrightness.text = Brightness;
        _TextMasterVolume.text = MasterVolume;


        #region Buttons to Press
        if (Input.GetAxis("Horizontal P1") > 0.5f && CanChange || Input.GetKey(KeyCode.D) && CanChange)
        { 
            Plus();
        }
        if (Input.GetAxis("Horizontal P1") < -0.5f && CanChange || Input.GetKey(KeyCode.A) && CanChange)
        {
            Minus();
        }
        if (Input.GetAxis("Vertical P1") > 0.5f && CanChange || Input.GetKey(KeyCode.W) && CanChange)
        {
            MinusInMenu();
        }
        if (Input.GetAxis("Vertical P1") < -0.5f && CanChange || Input.GetKey(KeyCode.S) && CanChange)
        {
            PlusInMenu();
        }
        if (Input.GetButton("LB P1") && CanChange || Input.GetKey(KeyCode.Q) && CanChange)
        {
            MinusMenu();
        }
        if (Input.GetButton("RB P1") && CanChange || Input.GetKey(KeyCode.E) && CanChange)
        {
            PlusMenu();
        }
        if (Input.GetButton("A P1") && CanChange || Input.GetKey(KeyCode.Return) && CanChange)
        {
            ReturnMenu();
        }

        #endregion
    }

    void Plus()
    {
        StartCoroutine(SetCanChange());
        if(OptionSelected == "Gráficos")
        {
            if (QualitySelected < QualitySettings.names.Length - 1)
            {
                QualitySelected++;
                QualitySettings.IncreaseLevel();
            }
        }
        if (OptionSelected == "Brilho")
        {
            if (BrightnessNumber < 20)
            {
                BrightnessNumber++;
                Brightness += "/";
            }
        }
        if (OptionSelected == "Volume Geral")
        {
            if (VolumeNumber < 20)
            {
                VolumeNumber++;
                MasterVolume += "/";
            }
        }
    }

    void Minus()
    {
        StartCoroutine(SetCanChange());

        if (OptionSelected == "Gráficos")
        {
            if (QualitySelected > 0)
            {
                QualitySelected --;
                QualitySettings.DecreaseLevel();
            }
        }

        if (OptionSelected == "Brilho")
        {
            if (BrightnessNumber > 0)
            {
                BrightnessNumber--;
                Brightness = "";
                for (int i = 0; i < BrightnessNumber; i++)
                {
                    Brightness += "/";
                }
            }
        }

        if (OptionSelected == "Volume Geral")
        {
            if (VolumeNumber > 0)
            {
                VolumeNumber--;
                MasterVolume = "";
                for (int i = 0; i < VolumeNumber; i++)
                {
                    MasterVolume += "/";
                }
            }
        }
    }

    void PlusMenu()
    {
        StartCoroutine(SetCanChange());

        if (MenuSelected < Menus.Length - 1)
        {
            MenuSelected++;
            InMenuSelection = 0;
            SetMenus();
            if (InMenus.Length > 0)
                OptionSelected = InMenus[InMenuSelection].name;
            for (int i = 0; i < Menus.Length; i++)
            {
                if (i != MenuSelected)
                {
                    Menus[i].GetComponent<Image>().color = UnselectedMenuColor;
                }
                else
                {
                    Menus[i].GetComponent<Image>().color = SelectedMenuColor;
                }
            }
            if (InMenus.Length > 0)
            {

                InMenus[0].GetComponent<Text>().color = SelectedColor;

                for (int i = 0; i < InMenus.Length; i++)
                {
                    if (i != InMenuSelection)
                    {
                        InMenus[i].GetComponent<Text>().color = UnselectedColor;
                    }
                }
            }
           
        }
    }

    void MinusMenu()
    {
        StartCoroutine(SetCanChange());

        if (MenuSelected > 0)
        {
            MenuSelected--;
            InMenuSelection = 0;
            SetMenus();
            if (InMenus.Length > 0)
                OptionSelected = InMenus[InMenuSelection].name;
            for (int i = 0; i < Menus.Length; i++)
            {
                if (i != MenuSelected)
                {
                    Menus[i].GetComponent<Image>().color = UnselectedMenuColor;
                }
                else
                {
                    Menus[i].GetComponent<Image>().color = SelectedMenuColor;
                }
            }
            if (InMenus.Length > 0)
            {

                InMenus[0].GetComponent<Text>().color = SelectedColor;

                for (int i = 0; i < InMenus.Length; i++)
                {
                    if (i != InMenuSelection)
                    {
                        InMenus[i].GetComponent<Text>().color = UnselectedColor;
                    }
                }
            }
        }
    }

    void PlusInMenu()
    {
        StartCoroutine(SetCanChange());

        if (InMenuSelection < InMenus.Length - 1)
        {
            InMenuSelection++;
            OptionSelected = InMenus[InMenuSelection].name;
            for(int i = 0; i < InMenus.Length; i++)
            {
                if(i != InMenuSelection)
                {
                    InMenus[i].GetComponent<Text>().color = UnselectedColor;
                }
                else
                {
                    InMenus[i].GetComponent<Text>().color = SelectedColor;
                }
            }
        }
    }

    void MinusInMenu()
    {
        StartCoroutine(SetCanChange());

        if (InMenuSelection > 0)
        {
            InMenuSelection--;
            OptionSelected = InMenus[InMenuSelection].name;
            for (int i = 0; i < InMenus.Length; i++)
            {
                if (i != InMenuSelection)
                {
                    InMenus[i].GetComponent<Text>().color = UnselectedColor;
                }
                else
                {
                    InMenus[i].GetComponent<Text>().color = SelectedColor;
                }
            }
        }
    }

    public void SetMenus()
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            if (i != MenuSelected)
            {
                Menus[i].GetComponent<OptionsMenus>().Menu.SetActive(false);
            }
            else
                Menus[i].GetComponent<OptionsMenus>().Menu.SetActive(true);
        }
        InMenus = new GameObject[Menus[MenuSelected].GetComponent<OptionsMenus>().Options.Length];
        InMenus = Menus[MenuSelected].GetComponent<OptionsMenus>().Options;

    }

    void ReturnMenu()
    {
        if (InMenus[InMenuSelection].name == "Sim")
        {
            if (Menus[MenuSelected].name == "Menu Principal")
            {
                if (PauseS != null)
                    PauseS.PauseGame();

                if (GameObject.FindWithTag("Loading") != null)
                    GameObject.FindWithTag("Loading").GetComponent<Loading>().StartCoroutine(GameObject.FindWithTag("Loading").GetComponent<Loading>().LoadAsync("Main"));
                else
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main");
            }
            if (Menus[MenuSelected].name == "Selecao Personagens")
            {
                if (PauseS != null)
                    PauseS.PauseGame();
                if (GameObject.FindWithTag("Loading") != null)
                    GameObject.FindWithTag("Loading").GetComponent<Loading>().StartCoroutine(GameObject.FindWithTag("Loading").GetComponent<Loading>().LoadAsync("SelecaoPersonagem"));
                else
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SelecaoPersonagem");
            }
            if (Menus[MenuSelected].name == "Sair do jogo")
            {
                if (PauseS != null)
                    PauseS.PauseGame();
                Application.Quit();
            }
        }

        if(InMenus[InMenuSelection].name == "Não")
        {
            if (PauseS != null)
                PauseS.PauseGame();
            else if (Cursor != null)
            {
                
                Cursor.GetComponent<MainMenu>().DeactiveMenu();
            }
        }

        if (InMenus[InMenuSelection].name == "Voltar")
        {
            if (Cursor != null)
            {
                Cursor.GetComponent<MainMenu>().DeactiveMenu();
            }
        }
    }

    public void SetColor()
    {
        if (InMenus.Length > 0)
        {

            InMenus[0].GetComponent<Text>().color = SelectedColor;

            for (int i = 0; i < InMenus.Length; i++)
            {
                if (i != InMenuSelection)
                {
                    InMenus[i].GetComponent<Text>().color = UnselectedColor;
                }
            }
        }

        for (int i = 0; i < Menus.Length; i++)
        {
            if (i != MenuSelected)
            {
                Menus[i].GetComponent<Image>().color = UnselectedMenuColor;
            }
            else
            {
                Menus[i].GetComponent<Image>().color = SelectedMenuColor;
            }
        }
        if (InMenus.Length > 1)
        OptionSelected = InMenus[InMenuSelection].name;
    }

    IEnumerator Static()
    {
        TVMenu.preset.staticFilter.staticMagnitude = Random.Range(0.05f, 0.15f);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.3f));
        TVMenu.preset.staticFilter.staticMagnitude = 0;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(5));
        StartCoroutine(Static());
    }

    public IEnumerator SetCanChange()
    {
        CanChange = false;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.3f));
        CanChange = true;

    }
}
