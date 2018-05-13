using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class John : MonoBehaviour {

    public S_JohnState m_currentState;

    public Vector3 m_newDestination;


    [SerializeField] private float m_speed;
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private List<Actor> L_actors = new List<Actor>();
    [SerializeField] private GameObject m_groupOfActors;
    [SerializeField] private GameManager I_gameManager;
    [SerializeField] private Transform m_waitingPoint;
    [SerializeField] private float m_proximityRadius;
    [SerializeField] private int m_hp;
    




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void StateMachine()
    {
        switch (m_currentState)
        {
            case S_JohnState.Idle:
                break;
            case S_JohnState.MoveTowards:
                if (m_newDestination != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, m_newDestination, m_speed * Time.deltaTime);
                }
                if (this.transform.position == m_newDestination && this.transform.position != m_waitingPoint.position)
                //checks if has arrived to the destination and gives a new one unless it's resting
                {
                    GetRandomDestination();
                }
                break;
            case S_JohnState.DefaultAction:
                break;
            case S_JohnState.BullyAction:
                m_groupOfActors.transform.position = Vector3.MoveTowards(m_groupOfActors.transform.position, m_waitingPoint.position, m_speed * Time.deltaTime);

                break;
        }
    }

    private void ChangeState(S_JohnState currentState, S_JohnState nextState)
    {
        switch (currentState)
        {
            case S_JohnState.Idle:
                switch (nextState)
                {
                    case S_JohnState.MoveTowards:
                        break;
                }
                break;
            case S_JohnState.MoveTowards:
                switch (nextState)
                {
                    case S_JohnState.Idle:
                        break;
                    case S_JohnState.Patrol:
                        break;

                    case S_JohnState.BullyAction:
                        break;
                }
                break;
            
            case S_JohnState.BullyAction:
                switch (nextState)
                {
                    case S_JohnState.RunAway:
                        break;
                    case S_JohnState.BullyAction:
                        Collider[] l_nearActors;
                        l_nearActors = Physics.OverlapSphere(this.transform.position, m_proximityRadius);

                        foreach(Collider nearActor in l_nearActors)
                        {
                            if(nearActor.gameObject.tag == "Actor")
                            {
                                Actor l_actor = nearActor.gameObject.GetComponent<Actor>();
                                l_actor.ChangeState(l_actor.m_currentState, S_ActorState.BullyActionGroupal);

                                if(m_hp == 1)
                                {
                                    //hacer que el grupo se active y le persiga
                                    foreach(Actor actor in L_actors)
                                    {
                                        actor.ChangeState(actor.m_currentState, S_ActorState.MoveTowards);
                                        m_newDestination = m_waitingPoint.position;

                                    }
                                    
                                }
                            }
                        }

                        break;
                }
                break;
        }
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

        m_newDestination = l_node.position;
    }

}
