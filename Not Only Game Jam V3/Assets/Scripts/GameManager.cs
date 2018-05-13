using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public S_GameState m_currentState;
    [SerializeField] private Manager I_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StateMachine();

        if (Input.GetMouseButtonDown(1))
        {
            ChangeState(m_currentState, S_GameState.StartPlayGround);
        }
        if (Input.GetMouseButtonDown(0))
        {
            ChangeState(m_currentState, S_GameState.ExitPlayGround);
        }
    }

    private void StateMachine()
    {
        switch (m_currentState)
        {
            case S_GameState.StartGame:
                //show splashscreen
                ChangeState(m_currentState, S_GameState.Tutorial);
                break;

            case S_GameState.Tutorial:
                if(!I_manager.I_cat.enabled)
                {
                    ChangeState(m_currentState, S_GameState.EndGame);
                }
                break;

            case S_GameState.ActorsMakeActions:
                //cuando la animacion de mirar al telefono se haya aacabado
                ChangeState(m_currentState, S_GameState.StartPlayGround);

                break;
            case S_GameState.ExitPlayGround:
                break;
            case S_GameState.StartPlayGround:
                break;
            case S_GameState.EndGame:
                break;

        }
    }

    public void ChangeState(S_GameState currentState, S_GameState nextState)
    {
        switch(currentState)
        {
            case S_GameState.ActorsMakeActions:
                switch(nextState)
                {
                    case S_GameState.ExitPlayGround:
                        break;
                }
                break;
               
            case S_GameState.ExitPlayGround:
                switch (nextState)
                {
                    case S_GameState.StartPlayGround:

                        //call manager and instance al actors to go to the playground
                        I_manager.AllActorsGoPlayGround();

                        break;
                }
                break;

            case S_GameState.StartPlayGround:
                switch (currentState)
                {
                    case S_GameState.ActorsMakeActions:

                        //call manager and instance al actors to make the action
                        I_manager.AllActorsLookAtSmartPhone();


                        break;
                }
                break;

            case S_GameState.StartGame:
                switch (currentState)
                {
                    case S_GameState.Tutorial:
                        

                        break;
                }
                break;

            case S_GameState.Tutorial:
                {
                    switch(currentState)
                    {
                        case S_GameState.StartPlayGround:
                            I_manager.AllActorsGoPlayGround();

                            break;
                    }
                }
                    break;


        }
        m_currentState = nextState;
    }
}
