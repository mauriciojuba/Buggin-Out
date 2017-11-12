using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesteTV : MonoBehaviour {


    [SerializeField] GameObject[] Menus;
    [SerializeField] GameObject[] InMenus;
    [SerializeField] int MenuSelected;
    [SerializeField] int InMenuSelection;
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

    // Use this for initialization
    void Start () {
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Plus();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Minus();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MinusInMenu();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlusInMenu();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MinusMenu();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlusMenu();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ReturnMenu();
        }

        #endregion
    }

    void Plus()
    {
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
        if (MenuSelected < Menus.Length)
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

    void SetMenus()
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
            if (Menus[MenuSelected].name == "Selecao Personagens")
            {
                if (PauseS != null)
                    PauseS.PauseGame();
                UnityEngine.SceneManagement.SceneManager.LoadScene("SelecaoPersonagem");
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
            if(PauseS != null)
                PauseS.PauseGame();
            else if(Cursor != null)
                Cursor.GetComponent<MainMenu>().DeactiveMenu();
        }

        if (InMenus[InMenuSelection].name == "Voltar")
        {
            if (Cursor != null)
            {
                Cursor.GetComponent<MainMenu>().DeactiveMenu();
            }
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
