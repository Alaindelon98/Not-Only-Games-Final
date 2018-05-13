using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public S_ActorState m_currentState;

    public Vector3 m_newDestination;

    public bool m_isTheBully;
    public bool m_isGroup;


    [SerializeField] private Vector2 m_cooldownRandomRange;
    [SerializeField] private float m_speed;
    [Space(30)]
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private John I_john;
    [SerializeField] private GameManager I_gameManager;
    [SerializeField] private Manager I_manager;
    [SerializeField] private List<Actor> L_actors = new List<Actor>();


    [SerializeField] private Transform m_waitingPoint;


    private float vAux_currentTime;
    private float m_cooldownNewRandomPosition;
    private int m_currentAnimationIndex;
    private Animation m_currentAnimation;

    // Use this for initialization
    void Start () {
        ChangeState(S_ActorState.Walk);
    }
	
	// Update is called once per frame
	void Update () {
        StateMachine();

	}
    private void StateMachine()
    {
        switch (m_currentState)
        {
            case S_ActorState.Idle:
                //if (I_gameManager.m_currentState == S_GameState.StartPlayGround)
                //{
                //    ChangeState(S_ActorState.Walk);
                //}
                break;
            case S_ActorState.Walk: //Characters walks around

                if (m_newDestination != null)
                {
                    Move();
                }
                if (this.transform.position == m_newDestination)
                //checks if has arrived to the destination and gives a new one unless it's resting
                {
                    if (vAux_currentTime >= m_cooldownNewRandomPosition)
                    {
                        GetRandomDestination();
                        vAux_currentTime = 0f;
                        m_cooldownNewRandomPosition = Random.Range(m_cooldownRandomRange.x, m_cooldownRandomRange.y);
                    }
                    else
                    {
                        vAux_currentTime += Time.deltaTime;
                    }
                     if(I_john.m_currentState == S_TommyState.Bullying && m_isTheBully)
                    {
                        ChangeState(S_ActorState.Bullying);
                    }
                }


                break;

            case S_ActorState.Bullying:

                if((int)I_gameManager.m_currentDay == 1)
                {
                    if (Vector3.Distance(this.transform.position, I_john.transform.position) <= 3f)
                    {
                        //change animation
                        ChangeState(S_ActorState.Walk);
                        print("I've chganded");
                    }
                    else
                    {
                        m_newDestination = I_john.transform.position;
                        Move();
                    }
                }
                else if((int)I_gameManager.m_currentDay ==2 && m_isTheBully)
                {
                    ChangeState(S_ActorState.Walk);
                }
                else if((int)I_gameManager.m_currentDay == 2 && m_isGroup)
                {
                    if (Vector3.Distance(this.transform.position, I_john.transform.position) <= 3f)
                    {
                        //change animation
                        ChangeState(S_ActorState.Walk);
                    }
                    else
                    {
                        m_newDestination = I_john.transform.position;
                        Move();
                    }
                }



                break;


        }
    }


    private void ChangeState(S_ActorState l_nextState)
    {
        switch (m_currentState)
        {
            case S_ActorState.Idle:
                switch (l_nextState)
                {
                    case S_ActorState.Walk:
                        GetRandomDestination();
                        m_currentState = l_nextState;
                        break;
                    case S_ActorState.Bullying:
                        m_currentState = l_nextState;
                        break;
                }
                break;
            case S_ActorState.Walk:
                switch (l_nextState)
                {
                    case S_ActorState.Idle:
                        m_currentState = l_nextState;
                        break;
                    case S_ActorState.Bullying:
                        m_currentState = l_nextState;
                        break;
                }
                break;
            case S_ActorState.Bullying:
                switch (l_nextState)
                {
                    case S_ActorState.Idle:
                        m_currentState = l_nextState;
                        break;
                    case S_ActorState.Walk:

                        //decide which animation you have to take depending on the day

                        m_currentState = l_nextState;
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
            }
        }

        if (I_john.m_newDestination == l_node.position)
        {
            GetRandomDestination();
        }

        m_newDestination = l_node.position;
    }

    private void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, m_newDestination, m_speed * Time.deltaTime);

        if (m_newDestination.x < this.transform.position.x && this.transform.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, 1, 1);
        }
        else if (m_newDestination.x > this.transform.position.x && this.transform.localScale.x < 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, 1, 1);

        }
    }

    private void ChangeAnimation(int index)
    {
        m_currentAnimationIndex = index;
        //m_currentAnimation.clip = m_animations[m_currentAnimationIndex];
        m_currentAnimation.Play();
    }
}
