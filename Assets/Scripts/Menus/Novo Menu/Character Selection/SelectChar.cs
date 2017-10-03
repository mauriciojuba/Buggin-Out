using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour {

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
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal P" + PlayerNumber) > 0.5f && CanChange)
        {
            CanChange = false;
            if (PosAtual < CharactersPos.Length - 1)
            PosAtual++;
            else if(PosAtual >= CharactersPos.Length - 1)
            {
                PosAtual = 0;
            }
            ChangePosition();
            StartCoroutine(SetCanChangeTrue());
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
            ChangePosition();
            StartCoroutine(SetCanChangeTrue());
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
            DataS.P1SelectedCharacter = CharactersPos[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            DataS.P1SelectedCharacter.PlayerNumber = PlayerNumb;
        }
        else if (PlayerNumb == 2)
        {
            DataS.P2SelectedCharacter = CharactersPos[PosAtual].GetComponent<CharacterHolder>().ThisCharacter;
            DataS.P2SelectedCharacter.PlayerNumber = PlayerNumb;

        }
    }

    public void OnDeselectCharacter(int PlayerNumb)
    {
        if (PlayerNumb == 1)
            DataS.P1SelectedCharacter = null;
        else if (PlayerNumb == 2)
            DataS.P2SelectedCharacter = null;
    }

    void ChangePosition()
    {
        transform.position = new Vector3(CharactersPos[PosAtual].transform.position.x, transform.position.y, CharactersPos[PosAtual].transform.position.z);
    }

    IEnumerator SetCanChangeTrue()
    {
        yield return new WaitForSeconds(0.15f);
        CanChange = true;
    }
}
