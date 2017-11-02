using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour {

    [SerializeField] GameObject[] AllObjs;
    [SerializeField] GameObject[] AllObjsPos;

    [SerializeField] List<GameObject> UnlockedObjs;
    [SerializeField] List<GameObject> UnlockedObjsPos;

    [SerializeField] int PosAtual;
    [SerializeField] int PlayerNumber;
    [SerializeField] bool CanChange;
    [SerializeField] bool Selected;
    [SerializeField] DetectJoysticks DetectS;
    Data DataS;

    [SerializeField] bool SelectCharacter, SelectPhase;

    void Start () {
        if (GameObject.FindWithTag("DATA") != null)
            DataS = GameObject.FindWithTag("DATA").GetComponent<Data>();

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SelecaoPersonagem")
        {
            SelectCharacter = true;
            SelectPhase = false;
        }
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SelecaoFase")
        {
            SelectCharacter = false;
            SelectPhase = true;
        }

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
        else if (SelectPhase)
        {
            if (DataS.CompletedPhases.Contains(1))
            {
                if (!UnlockedObjs.Contains(AllObjs[1]))
                {
                    UnlockedObjs.Add(AllObjs[1]);
                }
                if (!UnlockedObjsPos.Contains(AllObjsPos[1]))
                {
                    UnlockedObjsPos.Add(AllObjsPos[1]);
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (!CanChange)
        {
            if (ChangePosition()){
                CanChange = true;
            }
        }
        if (Input.GetAxis("Horizontal P" + PlayerNumber) > 0.5f && CanChange)
        {
            NextSelection();
        }
        if(Input.GetAxis("Horizontal P" + PlayerNumber) < -0.5f && CanChange)
        {
            PreviousSelection();
        }

        if(Input.GetButtonDown("A P" + PlayerNumber) && !Selected)
        {
            if(SelectCharacter)
            OnSelectCharacter(PlayerNumber);
        }

        if(Input.GetButtonDown("B P"+PlayerNumber) && Selected)
        {
            OnDeselectCharacter(PlayerNumber);
        }


    }

    public void NextSelection()
    {
        CanChange = false;
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
        if (PosAtual > 0)
            PosAtual--;
        else if (PosAtual <= 0)
        {
            PosAtual = UnlockedObjsPos.Count - 1;
        }
    }

    public void OnSelectCharacter(int PlayerNumb)
    {
        CanChange = false;
        DetectS.QuantSelected++;
        Selected = true;
        if (PlayerNumb == 1)
        {
            UnlockedObjs[PosAtual].GetComponent<Animator>().SetBool("Selected", true);
            DataS.P1SelectedCharacter = UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            DataS.P1SelectedCharacter.PlayerNumber = PlayerNumb;
        }
        else if (PlayerNumb == 2)
        {
            UnlockedObjs[PosAtual].GetComponent<Animator>().SetBool("Selected", true);
            DataS.P2SelectedCharacter = UnlockedObjs[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
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
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SelecaoPersonagem");
    }

    bool ChangePosition()
    {
        transform.position = Vector3.Lerp(transform.position, UnlockedObjsPos[PosAtual].transform.position, Time.deltaTime * 15);
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
}
