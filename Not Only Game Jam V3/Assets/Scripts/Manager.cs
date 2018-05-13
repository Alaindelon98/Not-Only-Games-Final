using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public List<Actor> L_actors = new List<Actor>();
    [SerializeField] private GameManager I_gameManager;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AllActorsLookAtSmartPhone()
    {
        foreach(Actor actor in L_actors)
            {
                actor.ChangeState(actor.m_currentState, S_ActorState.LookAtSmartPhone);
            }
    }

    public void AllActorsGoPlayGround()
    {
        foreach (Actor actor in L_actors)
        {
            actor.ChangeState(actor.m_currentState, S_ActorState.MoveTowards);
        }
    }
}
