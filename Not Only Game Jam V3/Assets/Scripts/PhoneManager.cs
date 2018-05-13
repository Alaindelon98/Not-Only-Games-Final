using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour {

    public s_PhoneState m_currentState;

    [SerializeField] private John I_jhon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StateMachine()
    {
        switch(m_currentState)
        {
            case s_PhoneState.PicturePosted:
                //can't take pictures
                //the picture is uploade to the social media and the comments start to appear
                //likes and follows change
                break;

            case s_PhoneState.TakingPicture:
                //can takes pictures using the mouse
                break;

            case s_PhoneState.Hiding:
                break;
        }
    }

    public void ChangeState(s_PhoneState currentState, s_PhoneState nextState)
    {
        switch(m_currentState)
        {
            case s_PhoneState.PicturePosted:
                break;
            case s_PhoneState.TakingPicture:
                break;
            case s_PhoneState.Hiding:
                //enable camera
                break;

        }

    }

    //private void CheckQualityOfThePicture( )
    //{
    //    if(I_jhon.transform.position.x < )
    //}

}
