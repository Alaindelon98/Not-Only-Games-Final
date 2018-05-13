using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDetector : MonoBehaviour {

    [SerializeField] private ScreenShot m_screenShot;
    [SerializeField] private John m_mainCharacter;

    public GameManager gm;

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

        //Collider[] l_colliders = Physics.OverlapBox(_position, l_halfExtents);
        Collider2D[] l_colliders = Physics2D.OverlapBoxAll(new Vector2(_position.x, _position.y), new Vector2(6, 3), 0);
        //Collider2D[] l_colliders = Physics2D.OverlapCircleAll(new Vector2(_position.x, _position.y), 1.5f);
        //GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.GetComponent<SphereCollider>().radius = l_halfExtents.x;
        //sphere.transform.position = _position;


        //Debug.Log(l_colliders[0].name);



        foreach (Collider2D _collider in l_colliders)
        {
            if(_collider.tag == "MainCharacter")
            {
                //if (m_mainCharacter.m_currentState == S_JohnState.BullyAction && m_mainCharacter.m_sufferingBulling)
                if (m_mainCharacter.m_sufferingBulling)
                {
                    Debug.Log("being bullied, change day");
                    gm.ChangeGameState(GameManager.S_GameStates.ChangeDay);
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
