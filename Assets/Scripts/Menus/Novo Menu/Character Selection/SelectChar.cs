using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour {

    [SerializeField] GameObject[] Characters;
    [SerializeField] GameObject[] CharactersPos;
    [SerializeField] int PosAtual;
    [SerializeField] int PlayerNumber;
    [SerializeField] bool CanChange;
    [SerializeField] bool Selected;
    [SerializeField] DetectJoysticks DetectS;
    Data DataS;

    void Start () {
        if (GameObject.FindWithTag("DATA") != null)
            DataS = GameObject.FindWithTag("DATA").GetComponent<Data>();
        ChangePosition();
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
            CanChange = false;
            if (PosAtual < CharactersPos.Length - 1)
            PosAtual++;
            else if(PosAtual >= CharactersPos.Length - 1)
            {
                PosAtual = 0;
            }
        }
        if(Input.GetAxis("Horizontal P" + PlayerNumber) < -0.5f && CanChange)
        {
            CanChange = false;
            if (PosAtual > 0)
                PosAtual--;
            else if (PosAtual <= 0)
            {
                PosAtual = CharactersPos.Length - 1;
            }
        }

        if(Input.GetButtonDown("A P" + PlayerNumber) && !Selected)
        {
            OnSelectCharacter(PlayerNumber);
            CanChange = false;
            DetectS.QuantSelected++;
            Selected = true;
        }

        if(Input.GetButtonDown("B P"+PlayerNumber) && Selected)
        {
            OnDeselectCharacter(PlayerNumber);
            CanChange = true;
            DetectS.QuantSelected--;
            Selected = false;
        }


    }

    public void OnSelectCharacter(int PlayerNumb)
    {
        if (PlayerNumb == 1)
        {
            Characters[PosAtual].GetComponent<Animator>().SetBool("Selected", true);
            DataS.P1SelectedCharacter = Characters[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            DataS.P1SelectedCharacter.PlayerNumber = PlayerNumb;
        }
        else if (PlayerNumb == 2)
        {
            Characters[PosAtual].GetComponent<Animator>().SetBool("Selected", true);
            DataS.P2SelectedCharacter = Characters[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            DataS.P2SelectedCharacter.PlayerNumber = PlayerNumb;

        }
    }

    public void OnDeselectCharacter(int PlayerNumb)
    {
        if (PlayerNumb == 1)
        {
            Characters[PosAtual].GetComponent<Animator>().SetBool("Selected", false);
            DataS.P1SelectedCharacter = null;
        }
        else if (PlayerNumb == 2)
        {
            Characters[PosAtual].GetComponent<Animator>().SetBool("Selected", false);
            DataS.P2SelectedCharacter = null;
        }
    }

    bool ChangePosition()
    {
        transform.position = Vector3.Lerp(transform.position, CharactersPos[PosAtual].transform.position, Time.deltaTime * 15);
        if (transform.position == CharactersPos[PosAtual].transform.position)
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
