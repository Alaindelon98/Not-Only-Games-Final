using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public List<Actor> L_actors = new List<Actor>();
    //public Cat I_cat;

    [SerializeField] private GameManager I_gameManager;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void AllActorsLookAtSmartPhone()
    //{
    //    foreach (Actor actor in L_actors)
    //    {
    //        actor.ChangeState(actor.m_currentState, S_ActorState.LookAtSmartPhone);
    //    }
    //}

    //public void AllActorsGoPlayGround()
    //{
    //    foreach (Actor actor in L_actors)
    //    {
    //        AnActorGoPlayGround(actor);
    //    }
    //}
    //public void AnActorGoPlayGround(Actor actor)
    //{
    //    actor.ChangeState(actor.m_currentState, S_ActorState.MoveTowards);
    //}
    //public void ACatGoPlayGround(Cat cat)
    //{

    //}

    //private void GetNearestActor(Vector3 position, float distance)
    //{
    //    Vector3 l_nearestPosition = Vector3.zero;
    //    Actor l_nearestActor;

    //    foreach (Actor actor in L_actors)
    //    {
    //        if (actor == L_actors[0])
    //        {
    //            l_nearestPosition = actor.transform.position;
    //            l_nearestActor = actor;
    //        }
    //        else
    //        {
    //            if (l_nearestPosition.magnitude > actor.transform.position.magnitude)
    //            {
    //                l_nearestPosition = actor.transform.position;
    //                l_nearestActor = actor;
    //            }
    //        }

    //    }

    //    Collider[] l_nearActors;
    //    l_nearActors = Physics.OverlapSphere(this.transform.position, distance);

    //    foreach (Collider nearActor in l_nearActors)
    //    {
    //        if (nearActor.gameObject.tag == "Actor")
    //        {
    //            Actor l_actor = nearActor.gameObject.GetComponent<Actor>();
    //            l_actor.ChangeState(l_actor.m_currentState, S_ActorState.BullyActionGroupal);

    //        }
    //    }
    //}
}
