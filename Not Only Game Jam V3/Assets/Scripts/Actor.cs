using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public S_ActorState m_currentState;

    public Vector3 m_newDestination;



    [SerializeField] private float m_speed;
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private John I_john;
    [SerializeField] private GameManager I_gameManager;
    [SerializeField] private Manager I_manager;

    [SerializeField] private Transform m_waitingPoint;

    private List<Actor> L_actors = new List<Actor>();

    private float vAux_currentTime;
    private float m_cooldownNewRandomPosition;

    // Use this for initialization
    void Start () {
		L_actors = I_manager.L_actors;
    }
	
	// Update is called once per frame
	void Update () {
        StateMachine();

        if (Input.GetMouseButton(0))
        {
            print(m_currentState);
            ChangeState(m_currentState, S_ActorState.LookAtSmartphone);
        }


	}

    private void StateMachine()
    {
        switch(m_currentState)
        {
            case S_ActorState.Idle:
                break;
            case S_ActorState.MoveTowards: //Characters walks around

                if(m_newDestination != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, m_newDestination, m_speed * Time.deltaTime);
                }
                if(this.transform.position == m_newDestination && this.transform.position != m_waitingPoint.position) 
                    //checks if has arrived to the destination and gives a new one unless it's resting
                {
                    if (vAux_currentTime >= m_cooldownNewRandomPosition)
                    {
                        GetRandomDestination();
                        vAux_currentTime = 0f;
                    }
                    else
                    {
                        vAux_currentTime += Time.deltaTime;
                    }

                }
                

                break;
            case S_ActorState.LookAtSmartPhone:
                break;
            case S_ActorState.BullyActionIndividual:
                if(this.transform.position != m_newDestination)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, m_newDestination, m_speed * Time.deltaTime);
                }
                else
                {
                    //DO ANIMATION
                    //START ANIMATION OF THE PLAYER
                }
                break;
            case S_ActorState.BullyActionGroupal:
                if (this.transform.position != m_newDestination)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, m_newDestination, m_speed * Time.deltaTime);
                }
                else
                {
                    //DO ANIMATION OF ALL SONS
                    //START ANIMATION OF THE PLAYER
                }
                break;
            case S_ActorState.LookAtSmartphone:
                //ANIMACION MIRAR AL TFNO

                //QUAN SACABI LANIMACIO
                ChangeState(m_currentState, S_ActorState.MoveTowards);
                break;
        }
    }

    public void ChangeState(S_ActorState currentState, S_ActorState nextState)
    {
        switch(currentState)
        {
            case S_ActorState.Idle:
                switch(nextState)
                {
                    case S_ActorState.MoveTowards:
                            GetRandomDestination();
                        break;
                }
                break;
            case S_ActorState.MoveTowards:

                vAux_currentTime = 0f;
                switch (nextState)
                {
                    case S_ActorState.Idle:
                        break;
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        m_newDestination = new Vector3(I_john.transform.position.x, I_john.transform.position.y - 1, I_john.transform.position.z); 

                        break;
                }
                break;

           
            case S_ActorState.BullyActionIndividual:
                switch(nextState)
                {
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.MoveTowards:
                        break;
                }
                break;
            case S_ActorState.BullyActionGroupal:
                switch (nextState)
                {
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.MoveTowards:
                        break;
                }
                break;

            case S_ActorState.LookAtSmartphone:
                switch(nextState)
                {
                    case S_ActorState.MoveTowards:
                        m_newDestination = m_waitingPoint.position;
                        print(m_waitingPoint);
                        break;
                }
                break;
        }
        m_currentState = nextState;
    }


    private void GetRandomDestination() //gets random position and iterates again if its the same new destination of another actor or any actor is doing an action there
    {
        CNode l_node;
        l_node = I_grid.GetRandomNode();

        foreach (Actor actor in L_actors)
        {
            if (actor != null)
            {


                if (actor.m_newDestination == l_node.position)
                {
                    GetRandomDestination();
                }
                else if (actor.transform.position == m_newDestination)
                {
                    if (actor.m_currentState == S_ActorState.LookAtSmartPhone || actor.m_currentState == S_ActorState.BullyActionIndividual)
                    {
                        GetRandomDestination();
                    }
                }
            }
        }

        if (I_john.m_newDestination == l_node.position)
        {
            GetRandomDestination();
        }
        else if (I_john.transform.position == m_newDestination)
        {
            if (I_john.m_currentState == S_JohnState.DefaultAction || I_john.m_currentState == S_JohnState.BullyAction)
            {
                GetRandomDestination();
            }
        }

        m_newDestination = l_node.position;
    }


}
