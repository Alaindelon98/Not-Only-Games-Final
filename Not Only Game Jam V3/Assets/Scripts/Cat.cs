using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    private Vector3 m_newDestination;
    private Vector3 m_firstPosition;
    private float m_cooldownNewRandomPosition;


    [SerializeField] PhoneManager I_phoneManager;
    [SerializeField] GameManager I_gameManager;
    [SerializeField] private TheGrid I_grid;

    [SerializeField] private Vector2 m_cooldownRandomRange;
    [SerializeField] float m_speed;


    private float vAux_currentTime;
    private bool hasToMove;




    // Use this for initialization
    void Start () {
        m_firstPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (I_gameManager.m_currentState == S_GameState.Tutorial)
        {
            if (I_phoneManager.m_currentState == s_PhoneState.PicturePosted || I_gameManager.m_currentState == S_GameState.StartPlayGround)
            {
                //animacion corriendo
                m_newDestination = m_firstPosition;

                if (this.transform.position == m_firstPosition) // cuando llega a su desstino final se desactiva el script
                {
                    this.enabled = false;
                }
            }

            if (this.transform.position == m_newDestination)
            {
                //animacion de cat idle

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

            Move();

        }



    }

    private void GetRandomDestination() //gets random position and iterates again if its the same new destination of another actor or any actor is doing an action there
    {
        CNode l_node;
        l_node = I_grid.GetRandomNode();


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

}
