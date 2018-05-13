using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadComments : MonoBehaviour {



    private string[] m_commentsArray;

    private string l_CommentsString;

    public TextAsset l_MainFile;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void ReadString()
    {
        l_CommentsString = l_MainFile.text;
        SplitComments();
    }

    void SplitComments()
    {

        //Count the number of subtrings
        int vAux_substringCount = 0;
        int vAux_lastCorxete = 0;

        for (int idx = 0; idx < l_CommentsString.Length; idx++)
        {
            
                if (l_CommentsString[idx] == '#' && idx < l_CommentsString.Length-1) vAux_substringCount++;
            
        }
        //Instantiate a string[] of the define number of strings
        m_commentsArray = new string[vAux_substringCount];
        vAux_substringCount = 0;


        //Split the main string
        for (int idx = 0; idx < l_CommentsString.Length; idx++)
        {
            if (l_CommentsString[idx] == '#' && idx!= 0)
            {
                //New frase
                m_commentsArray[vAux_substringCount] = l_CommentsString.Substring(vAux_lastCorxete + 1, idx - vAux_lastCorxete - 1);
                vAux_lastCorxete = idx;
                vAux_substringCount++;              
            }
        }
    }

    public List<string> GetComments(char sceneChar)
    {
        List<string> l_commentsList = new List<string>();

        for (int idx = 0; idx < m_commentsArray.Length; idx++)
        {
            if (m_commentsArray[idx][0] == sceneChar)
            {
                string newComment = m_commentsArray[idx].Substring(1, m_commentsArray[idx].Length - 2);
                l_commentsList.Add(newComment);
            }            
        }
        return l_commentsList;
    }

}
