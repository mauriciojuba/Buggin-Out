using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour {

    [SerializeField] GameObject[] AllObjs;
    [SerializeField] GameObject[] AllObjsPos;

    [SerializeField] List<GameObject> UnlockedObjs;
    [SerializeField] List<GameObject> UnlockedObjsPos;
    [SerializeField] List<Material> UnlockedSkinLiz;
    [SerializeField] List<Material> UnlockedSKinHorn;

    [SerializeField] int PosAtual;
    [SerializeField] int PosSkinAtual;
    [SerializeField] int PlayerNumber;
    [SerializeField] bool CanChange,CanChangeSkin;
    [SerializeField] bool Selected;
    [SerializeField] DetectJoysticks DetectS;
    Data DataS;

    [SerializeField] bool SelectCharacter, SelectSkin;

    [FMODUnity.EventRef]
    public string Evento_Liz;
    [FMODUnity.EventRef]
    public string Evento_Horn;

    // Colocar essa linha Onde seleciona a Liz
    //FMODUnity.RuntimeManager.PlayOneShot(Evento_Liz, transform.position);

    // Colocar essa linha Onde seleciona o Horn
    //FMODUnity.RuntimeManager.PlayOneShot(Evento_Horn, transform.position);

    void Start () {
        if (GameObject.FindWithTag("DATA") != null)
            DataS = GameObject.FindWithTag("DATA").GetComponent<Data>();

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SelecaoPersonagem")
        {
            SelectCharacter = true;
        }

        if (DataS != null)
        {
            if (SelectCharacter)
            {
                if (DataS.UnlockedCharacter.Contains("Juan"))
                {
                    if (!UnlockedObjs.Contains(AllObjs[2]))
                    {
                        UnlockedObjs.Add(AllObjs[2]);
                    }
                    if (!UnlockedObjsPos.Contains(AllObjsPos[2]))
                    {
                        UnlockedObjsPos.Add(AllObjsPos[2]);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Data Não encontrado");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.GetComponent<OLDTVFilter3>().preset.noiseFilter.magnetude <= 0)
        {
            if (!CanChange && !Selected)
            {
                if (ChangePosition())
                {
                    CanChange = true;
                }
            }
            if (Input.GetAxis("Horizontal P" + PlayerNumber) > 0.5f && CanChange)
            {
                if (!Selected)
                    NextSelection();
               
            }
            if (Input.GetAxis("Horizontal P" + PlayerNumber) < -0.5f && CanChange)
            {
                if (!Selected)
                    PreviousSelection();
            }

            if (Input.GetButtonDown("A P" + PlayerNumber))
            {
                if (SelectCharacter)
                {
                    OnSelectCharacter(PlayerNumber);
                }
            }

            if (Input.GetButtonDown("B P" + PlayerNumber) && Selected)
            {
                if (SelectCharacter && Selected)
                    OnDeselectCharacter(PlayerNumber);
            }

            if(Input.GetButtonDown("RB P" + PlayerNumber))
            {
                if (!Selected)
                {
                    NextSkinSelection();
                }
            }

            if (Input.GetButtonDown("LB P" + PlayerNumber))
            {
                if (!Selected)
                {
                    PreviousSkinSelection();
                }
            }

        }
    }

    public void NextSelection()
    {
        CanChange = false;
        PosSkinAtual = 0;
        SetSkin();
        if (PosAtual < UnlockedObjsPos.Count - 1)
            PosAtual++;
        else if (PosAtual >= UnlockedObjsPos.Count - 1)
        {
            PosAtual = 0;
        }
    }

    public void PreviousSelection()
    {
        CanChange = false;
        PosSkinAtual = 0;
        SetSkin();
        if (PosAtual > 0)
            PosAtual--;
        else if (PosAtual <= 0)
        {
            PosAtual = UnlockedObjsPos.Count - 1;
        }
    }

    public void NextSkinSelection()
    {
        CanChangeSkin = false;

        if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Liz")
        {
            if (PosSkinAtual < UnlockedSkinLiz.Count - 1)
                PosSkinAtual++;
            else if (PosSkinAtual >= UnlockedSkinLiz.Count - 1)
            {
                PosSkinAtual = 0;
            }
        }
        if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Horn")
        {
            if (PosSkinAtual < UnlockedSKinHorn.Count - 1)
                PosSkinAtual++;
            else if (PosSkinAtual >= UnlockedSKinHorn.Count - 1)
            {
                PosSkinAtual = 0;
            }
        }
        
        SetSkin();
        StartCoroutine(SetCanChangeSkinTrue());

    }

    public void PreviousSkinSelection()
    {
        CanChangeSkin = false;
        if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Liz")
        {
            if (PosSkinAtual > 0)
                PosSkinAtual--;
            else if (PosSkinAtual <= 0)
            {
                PosSkinAtual = UnlockedSkinLiz.Count - 1;
            }
        }
        if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Horn")
        {
            if (PosSkinAtual > 0)
                PosSkinAtual--;
            else if (PosSkinAtual <= 0)
            {
                PosSkinAtual = UnlockedSKinHorn.Count - 1;
            }
        }
        
        SetSkin();
        StartCoroutine(SetCanChangeSkinTrue());

    }

    public void SetSkin()
    {
        if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Liz")
        {
            for (int i = 0; i < UnlockedObjs[PosAtual].transform.Find("Liz:Liz_ALL").Find("Liz:MALHA").childCount; i++)
            {
                UnlockedObjs[PosAtual].transform.Find("Liz:Liz_ALL").Find("Liz:MALHA").GetChild(i).GetComponent<SkinnedMeshRenderer>().material = UnlockedSkinLiz[PosSkinAtual];
            }
        }

        if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Horn")
        {
            for (int i = 0; i < UnlockedObjs[PosAtual].transform.Find("Horn:Horn_ALL").Find("Horn:MALHA").childCount; i++)
            {
                UnlockedObjs[PosAtual].transform.Find("Horn:Horn_ALL").Find("Horn:MALHA").GetChild(i).GetComponent<SkinnedMeshRenderer>().material = UnlockedSKinHorn[PosSkinAtual];
            }
        }
    }

    public void OnSelectCharacter(int PlayerNumb)
    {
        CanChange = false;
        Selected = true;
        DetectS.QuantSelected++;
        UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().Sound();
        if (PlayerNumb == 1)
        {
            UnlockedObjs[PosAtual].GetComponent<Animator>().SetBool("Selected", true);
            DataS.P1SelectedCharacter = UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Liz")
            {
                DataS.P1SkinSelected = UnlockedSkinLiz[PosSkinAtual];
            }
            if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Horn")
            {
                DataS.P1SkinSelected = UnlockedSKinHorn[PosSkinAtual];
            }
            DataS.P1SelectedCharacter.PlayerNumber = PlayerNumb;
        }
        if (PlayerNumb == 2)
        {
            UnlockedObjs[PosAtual].GetComponent<Animator>().SetBool("Selected", true);
            DataS.P2SelectedCharacter = UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Liz")
            {
                DataS.P2SkinSelected = UnlockedSkinLiz[PosSkinAtual];
            }
            if (UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter.CharacterName == "Horn")
            {
                DataS.P2SkinSelected = UnlockedSKinHorn[PosSkinAtual];
            }
            DataS.P2SelectedCharacter.PlayerNumber = PlayerNumb;
        }
    }

    public void OnDeselectCharacter(int PlayerNumb)
    {
        CanChange = true;
        DetectS.QuantSelected--;
        Selected = false;
        if (PlayerNumb == 1)
        {
            UnlockedObjs[PosAtual].GetComponent<Animator>().SetBool("Selected", false);
            DataS.P1SelectedCharacter = null;
        }
        else if (PlayerNumb == 2)
        {
            UnlockedObjs[PosAtual].GetComponent<Animator>().SetBool("Selected", false);
            DataS.P2SelectedCharacter = null;
        }
    }
    
    public void OnSelectPhase()
    {
        CanChange = false;
        Selected = true;
        DataS.PhaseName = UnlockedObjs[PosAtual].GetComponent<Menus>().NamePhase;
        if (GameObject.FindWithTag("Loading") != null)
            GameObject.FindWithTag("Loading").GetComponent<Loading>().StartCoroutine(GameObject.FindWithTag("Loading").GetComponent<Loading>().LoadAsync("SelecaoPersonagem"));
    }

    bool ChangePosition()
    {
        transform.position = Vector3.Lerp(transform.position, UnlockedObjsPos[PosAtual].transform.position, Time.deltaTime * 10);
        if (transform.position == UnlockedObjsPos[PosAtual].transform.position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator SetCanChangeTrue()
    {
        yield return new WaitForSeconds(0.15f);
        CanChange = true;
    }

    IEnumerator SetCanChangeSkinTrue()
    {
        yield return new WaitForSeconds(0.15f);
        CanChangeSkin = true;
    }
}
