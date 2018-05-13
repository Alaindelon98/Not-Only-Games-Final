using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    private Transform catDestination;
    private Vector3 catOrigin;
    [SerializeField] float speed;
    [SerializeField] PhoneManager I_phoneManager;
    [SerializeField] GameManager I_gameManager;




    // Use this for initialization
    void Start () {
        catOrigin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(I_gameManager.m_currentState == S_GameState.Tutorial)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, catDestination.position, speed * Time.deltaTime);

            if (this.transform.position == catDestination.position)
            {
                //animacion de cat idle
            }
            else if (I_phoneManager.m_currentState == s_PhoneState.PicturePosted)
            {
                //animacion corriendo
                this.transform.position = Vector3.MoveTowards(this.transform.position, catOrigin, speed * Time.deltaTime);

                if (this.transform.position == catOrigin) // cuando llega a su desstino final se desactiva el script
                {
                    this.enabled = false;
                }
            }
        }
        


    }
}
