using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class John : MonoBehaviour {

    [SerializeField] private float m_speed;
    [SerializeField] private float m_proximityRadius;
    [SerializeField] private int m_hp;
    [SerializeField] private Vector2 m_cooldownRandomRange;
    [SerializeField] private Vector2 m_actionTimerRandomRange;


    [Space(30)]

    public S_TommyAnimations m_currentAnimState;
    public S_TommyState m_currentState;
    public Vector3 m_newDestination;

    [Space(30)]
    [SerializeField]
    private List<Actor> L_actors = new List<Actor>();
    [SerializeField] private GameObject m_groupOfActors;
    [SerializeField] private GameManager I_gameManager;
    [SerializeField] private Manager I_manager;
    [SerializeField] private Transform m_waitingPoint;
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private AnimationClip[] m_animations;


    private bool m_sufferingBulling = false;
    private float vAux_currentTime;
    private float vAux_currentTime2;

    private float m_cooldownNewRandomPosition;
    private float m_actionTimerRandom;
    public int m_currentAnimationIndex;
    private Animation m_currentAnimation;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        AnimationsStateMachine();

    }

    private void StateMachine()
    {
        switch (m_currentState)
        {
            case S_TommyState.Idle:
                if (I_gameManager.m_currentState == S_GameState.StartPlayGround)
                {

                    

                    ChangeState(S_TommyState.Walk);
                }
                break;
            case S_TommyState.Walk:
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

                if (this.transform.position == m_waitingPoint.transform.position)
                {
                    ChangeState(S_TommyState.Idle);
                }

                if (m_newDestination != m_waitingPoint.position)
                {
                    DecideAction();
                }

                break;
            case S_TommyState.Bullying:
                if (m_hp == 1)
                {
                    m_groupOfActors.transform.position = Vector3.MoveTowards(m_groupOfActors.transform.position, m_waitingPoint.position, m_speed * Time.deltaTime);
                    if (m_groupOfActors.transform.position == this.transform.position)
                    {
                        ChangeAnimation((int)S_TommyAnimations.Fight);
                        m_sufferingBulling = true;
                    }
                }

                ChangeAnimation(m_currentAnimationIndex);
                break;
        }
    }

    private void AnimationsStateMachine()
    {
        switch (m_currentAnimState)
        {
            case S_TommyAnimations.Idle0:
                if (I_gameManager.m_currentState == S_GameState.StartPlayGround)
                {
                    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Walk0);
                }
                break;
            case S_TommyAnimations.Idle1:
                if (I_gameManager.m_currentState == S_GameState.StartPlayGround)
                {
                    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Walk1);
                }
                break;
            case S_TommyAnimations.Idle2:
                if (I_gameManager.m_currentState == S_GameState.StartPlayGround)
                {
                    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Walk2);
                }
                break;

            case S_TommyAnimations.Walk0:

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

                if (this.transform.position == m_waitingPoint.transform.position)
                {
                    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Idle0);
                }

                if (m_newDestination != m_waitingPoint.position)
                {
                    DecideAction();
                }

                break;
            case S_TommyAnimations.Walk1:

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

                if (this.transform.position == m_waitingPoint.transform.position)
                {
                    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Idle1);
                }

                if (m_newDestination != m_waitingPoint.position)
                {
                    DecideAction();
                }

                break;
            case S_TommyAnimations.Walk2:

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

                if (this.transform.position == m_waitingPoint.transform.position)
                {
                    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Idle2);
                }

                if (m_newDestination != m_waitingPoint.position)
                {
                    DecideAction();
                }

                break;

            /*case S_TommyAnimations.BullyAction:

                if (m_hp == 1)
                {
                    m_groupOfActors.transform.position = Vector3.MoveTowards(m_groupOfActors.transform.position, m_waitingPoint.position, m_speed * Time.deltaTime);
                    if (m_groupOfActors.transform.position == this.transform.position)
                    {
                        ChangeAnimation((int)S_TommyAnimations.Fight);

                    }
                }

                ChangeAnimation(m_currentAnimationIndex);



                break;*/
        }
    }

    private void ChangeState(S_TommyState l_nextState)
    {
        switch (m_currentState)
        {
            case S_TommyState.Idle:
                switch (l_nextState)
                {
                    case S_TommyState.Walk:
                        if (m_hp == 1)
                        {
                            ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Walk0);
                        }
                        else if (m_hp == 2)
                        {

                        }
                        else
                        {

                        }
                        m_currentState = l_nextState;
                        break;
                    case S_TommyState.Bullying:
                        m_currentState = l_nextState;
                        break;
                }
                break;
            case S_TommyState.Walk:
                switch (l_nextState)
                {
                    case S_TommyState.Idle:
                        m_currentState = l_nextState;
                        break;
                    case S_TommyState.Bullying:
                        m_currentState = l_nextState;
                        break;
                }
                break;
            case S_TommyState.Bullying:
                switch (l_nextState)
                {
                    case S_TommyState.Idle:
                        m_currentState = l_nextState;
                        break;
                    case S_TommyState.Walk:
                        m_currentState = l_nextState;
                        break;
                }
                break;
        }
    }

    private void ChangeAnimationState(S_TommyAnimations currentState, S_TommyAnimations nextState)
    {
        switch (currentState)
        {
            case S_TommyAnimations.Idle0:
                switch (nextState)
                {
                    case S_TommyAnimations.Walk0:
                        ChangeAnimation((int)S_TommyAnimations.Run);
                        break;
                }
                break;
            case S_TommyAnimations.MoveTowards:
                switch (nextState)
                {
                    case S_TommyAnimations.Idle:
                        ChangeAnimation((int)S_TommyAnimations.Idle);

                        break;

                    case S_TommyAnimations.BullyAction:
                        if (m_hp == 2)
                        {
                            Actor l_actor;
                            l_actor = I_manager.GetNearestActor(this.transform.position, m_proximityRadius);
                            l_actor.ChangeState(l_actor.m_currentState, S_ActorState.BullyActionGroupal);

                        }
                        else if (m_hp == 1)
                        {
                            //it is called from actor
                        }

                        if (!m_currentAnimation.isPlaying)
                        {
                            ChangeState(m_currentAnimState, S_TommyAnimations.MoveTowards);
                        }

                        break;
                }
                break;

            case S_TommyAnimations.BullyAction:
                switch (nextState)
                {

                }
                break;

        }
        m_currentAnimState = nextState;
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
    private void ChangeAnimation(int index)
    {
        m_currentAnimationIndex = index;
        m_currentAnimation.clip = m_animations[m_currentAnimationIndex];
        m_currentAnimation.Play();

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

    private void DecideAction()
    {
        if (m_actionTimerRandom == 0)
        {
            m_actionTimerRandom = Random.Range(m_actionTimerRandomRange.x, m_actionTimerRandomRange.y);
        }

        if (vAux_currentTime2 >= m_actionTimerRandom)
        {

            if (m_hp == 3)
            {
                m_currentAnimationIndex = Random.Range(0, 2); //Animación caida y mancha
            }
            else if (m_hp == 2)
            {
                m_currentAnimationIndex = Random.Range(2, 4); //Animación patada y tirar bocata
            }
            else if (m_hp == 1)
            {
                m_currentAnimationIndex = 5; //Animación paliza
            }
            else if (m_hp == 0)
            {

            }
            m_currentAnimation.clip = m_animations[m_currentAnimationIndex];
            vAux_currentTime2 = 0f;
            ChangeState(m_currentAnimState, S_TommyAnimations.BullyAction);
        }
        else
        {
            vAux_currentTime2 += Time.deltaTime;
        }
    }
}
