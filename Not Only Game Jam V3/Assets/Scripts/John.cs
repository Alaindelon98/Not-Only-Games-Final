﻿using System.Collections;
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
    public AudioSource tensionAudio;

    [Space(30)]
    [SerializeField]
    private List<Actor> L_actors = new List<Actor>();
    [SerializeField] private GameObject m_groupOfActors;
    [SerializeField] private GameManager I_gameManager;
    [SerializeField] private Manager I_manager;
    [SerializeField] private Transform m_waitingPoint;
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private EventDetector I_eventDetector;

    [SerializeField] private Animator m_animator;

    [SerializeField] private AnimationClip A_walk0;
    [SerializeField] private AnimationClip A_walk1;
    [SerializeField] private AnimationClip A_walk2;
    [SerializeField] private AnimationClip A_idle0;
    [SerializeField] private AnimationClip A_idle1;
    [SerializeField] private AnimationClip A_idle2;
    [SerializeField] private AnimationClip A_dirt;
    [SerializeField] private AnimationClip A_fall;
    [SerializeField] private AnimationClip A_foodTrap;
    [SerializeField] private AnimationClip A_kick;
    [SerializeField] private AnimationClip A_fight;


    public float m_bullyTimer = 3f;
    public bool m_sufferingBulling = false;

    private float vAux_currentTime;
    private float vAux_currentTime2;
    private float vAux_currentTime3;


    private bool m_pictureHasBeenTaken;
    private float m_cooldownNewRandomPosition;
    private float m_actionTimerRandom;
    public int m_currentAnimationIndex;
    private Animation m_currentAnimation;


    bool isPlaying;

    // Use this for initialization
    void Start()
    {
        ChangeState(S_TommyState.Walk);

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlaying)
        {
            Debug.Log("PLAY AUDIO");
            tensionAudio.Play();
            StartCoroutine(UpVolume());
            isPlaying = true;
        }
        StateMachine();
    }

    private void StateMachine()
    {
        switch (m_currentState)
        {
            case S_TommyState.Idle:
                //if (I_gameManager.m_currentState == S_GameState.StartPlayGround)
                //{
                //    ChangeState(S_TommyState.Walk);
                //}
                break;
            case S_TommyState.Walk:
                    Move();

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
                }


                DecideAction();


                break;
            case S_TommyState.Bullying:


                print(I_gameManager.m_currentDay);

                if (m_bullyTimer <= vAux_currentTime3) // poner que tambien se ejecute cuando se acaba la animacion
                {
                    ChangeState(S_TommyState.Walk);
                    m_pictureHasBeenTaken = false;
                    vAux_currentTime3 = 0f;

                }
                else
                {
                    vAux_currentTime3 += Time.deltaTime;
                }



                if ((int)I_gameManager.m_currentDay == 0)//cambiar por dia
                {
                    //animacion trabanqueta
                    //m_animator.SetBool("walk", true);
                    m_sufferingBulling = true;

                }
                else if((int)I_gameManager.m_currentDay == 1)
                {
                    //animacion 2
                    m_sufferingBulling = true;

                }
                else if((int)I_gameManager.m_currentDay == 2)
                {

                    tensionAudio.Play();
                    StartCoroutine(UpVolume());

                    m_groupOfActors.transform.position = Vector3.MoveTowards(m_groupOfActors.transform.position, m_waitingPoint.position, m_speed * Time.deltaTime);
                    if (m_groupOfActors.transform.position == this.transform.position)
                    {
                        //ChangeAnimation((int)S_TommyAnimations.Fight);
                        m_sufferingBulling = true;

                    }
                }

                else if(((int)I_gameManager.m_currentDay == 3))
                {
                    m_newDestination = new Vector3(100, 7, 0);

                }

                //ChangeAnimation(m_currentAnimationIndex);
                break;

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
                        //if (m_hp == 1)
                        //{
                        //    ChangeAnimationState(m_currentAnimState, S_TommyAnimations.Walk0);
                        //}
                        //else if (m_hp == 2)
                        //{

                        //}
                        //else
                        //{

                        //}
                        GetRandomDestination();
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

                        //decide which animation you have to take depending on the day
                        GetRandomDestination();
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

        m_newDestination = l_node.position;
    }
    private void ChangeAnimation(int index)
    {
        m_currentAnimationIndex = index;
        //m_currentAnimation.clip = m_animations[m_currentAnimationIndex];
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

            //if (m_hp == 3)
            //{
            //    m_currentAnimationIndex = Random.Range(0, 2); //Animación caida y mancha
            //}
            //else if (m_hp == 2)
            //{
            //    m_currentAnimationIndex = Random.Range(2, 4); //Animación patada y tirar bocata
            //}
            //else if (m_hp == 1)
            //{
            //    m_currentAnimationIndex = 5; //Animación paliza
            //}
            //else if (m_hp == 0)
            //{

            //}

            //m_currentAnimation.clip = m_animations[m_currentAnimationIndex];
            vAux_currentTime2 = 0f;
            ChangeState(S_TommyState.Bullying);
        }
        else
        {
            vAux_currentTime2 += Time.deltaTime;
        }
    }

    public void TakePhoto()
    {
        m_pictureHasBeenTaken = true;
    }


    IEnumerator UpVolume()
    {
        while (tensionAudio.volume < 1)
        {
            tensionAudio.volume += 0.1f;
            Debug.Log(tensionAudio.volume);
            yield return new WaitForSeconds(1);
        }
        yield break;
    }
}

