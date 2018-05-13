using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public S_ActorState m_currentState;

    public Vector3 m_newDestination;


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
        switch (m_currentState)
        {
            case S_ActorState.Idle:
                break;
            case S_ActorState.MoveTowards: //Characters walks around

                if (m_newDestination != null)
                {
                    Move();
                }
                if (this.transform.position == m_newDestination && this.transform.position != m_waitingPoint.position)
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

                }


                break;
            case S_ActorState.LookAtSmartPhone:
                break;
            case S_ActorState.BullyActionIndividual:
                if (this.transform.position != m_newDestination)
                {
                    Move();
                }
                else
                {
                    if (I_john.m_currentAnimationIndex == (int)S_TommyAnimations.FoodTrap)
                    {
                        ChangeAnimation((int)S_ActorAnimations.FoodTrap);
                    }
                    else if (I_john.m_currentAnimationIndex == (int)S_TommyAnimations.Kick)
                    {
                        ChangeAnimation((int)S_ActorAnimations.Kick);
                    }

                    //DO ANIMATION
                    //START ANIMATION OF THE PLAYER
                }
                break;
            case S_ActorState.BullyActionGroupal:
                if (this.transform.position != m_newDestination)
                {
                    Move();
                }
                else
                {
                    ChangeAnimation((int)S_ActorAnimations.Fight);

                }
                break;
            case S_ActorState.LookAtSmartphone:
                //ANIMACION MIRAR AL TFNO


                //QUAN SACABI LANIMACIO
                if (!m_currentAnimation.isPlaying)
                {
                    ChangeState(m_currentState, S_ActorState.MoveTowards);
                }
                break;


        }
    }

    public void ChangeState(S_ActorState currentState, S_ActorState nextState)
    {
        switch (currentState)
        {
            case S_ActorState.Idle:
                switch (nextState)
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
                    case S_ActorState.BullyActionGroupal:
                        m_newDestination = new Vector3(I_john.transform.position.x, I_john.transform.position.y - 1, I_john.transform.position.z);
                        break;
                    case S_ActorState.LookAtSmartPhone:
                        ChangeAnimation((int)S_ActorAnimations.LookAtSmartphones);
                        break;
                }
                break;


            case S_ActorState.BullyActionIndividual:
                switch (nextState)
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
                switch (nextState)
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
            if (I_john.m_currentAnimState == S_TommyAnimations.DefaultAction || I_john.m_currentAnimState == S_TommyAnimations.BullyAction)
            {
                GetRandomDestination();
            }
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
        m_currentAnimation.clip = m_animations[m_currentAnimationIndex];
        m_currentAnimation.Play();
    }
}
