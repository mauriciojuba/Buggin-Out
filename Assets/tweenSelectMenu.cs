using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tweenSelectMenu : MonoBehaviour {

    public enum FSMStates { Options, Game, Credits, Exit ,Menu};
    public FSMStates state = FSMStates.Options;


    public string Select;

    public void FixedUpdate()
    {
        switch (state)
        {

            case FSMStates.Options:
                Options();
                break;

            case FSMStates.Game:
                Game();
                break;

            case FSMStates.Credits:
                Credits();
                break;

            case FSMStates.Exit:
                Exit();
                break;

            case FSMStates.Menu:
                Menu();
                break;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void Menu()

    {

        // if (........)
        //state = FSMStates.Estado_2;
    }

    private void Options()
    {

        // if (........)
        //state = FSMStates.Estado_2;
    }
    private void Game()

    {

        // if (........)
        //state = FSMStates.Estado_2;
    }
    private void Credits()

    {

        // if (........)
        //state = FSMStates.Estado_2;
    }
    private void Exit()

    {

        // if (........)
        //state = FSMStates.Estado_2;
    }

}
