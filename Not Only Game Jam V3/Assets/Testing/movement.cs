using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    float speed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 _newPosition = transform.position;
        _newPosition.x += Mathf.Sin(Time.time) * Time.deltaTime * speed;
        transform.position = _newPosition;

    }
}
