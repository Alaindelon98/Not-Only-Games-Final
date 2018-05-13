using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDetector : MonoBehaviour {

    [SerializeField] private ScreenShot m_screenShot;
    [SerializeField] private John m_mainCharacter;

    //TEMP TEMP TEMP
    public bool bullied;
    //END OF TEMP

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void F_Analize(Vector3 _position)
    {
        Vector3 l_halfExtents = m_screenShot.F_GetRenderCameraOrthograficSize();
        l_halfExtents.z = l_halfExtents.x;

        Collider[] l_colliders = Physics.OverlapBox(_position, l_halfExtents);

        foreach (Collider _collider in l_colliders)
        {
            if(_collider.tag == "MainCharacter")
            {
                //if (m_mainCharacter.m_currentState == S_JohnState.BullyAction && m_mainCharacter.m_sufferingBulling)
                if (m_mainCharacter.m_currentState == S_JohnState.BullyAction && bullied)
                {
                    Debug.Log("being bullied");
                }
            }
            else if (_collider.tag == "Kid")
            {

            }
            else if (_collider.tag == "Cat")
            {

            }
        }
    }
}
