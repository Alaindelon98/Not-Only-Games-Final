using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentsManager : MonoBehaviour {

    public ReadComments i_readComments;

    List<string> comments;

	// Use this for initialization
	void Start () {
        i_readComments.ReadString();
        comments = i_readComments.GetComments('6');    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
