using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour {

    Text Bully1, Bully2, Bully3;

    int currentText = 1;
    Color color;


	void Start () {
        color = Bully1.color;
        color.a = 0;

        Bully1.color = color;
        Bully2.color = color;
        Bully3.color = color;
    }

    // Update is called once per frame
    void Update () {
        while (currentText < 4)
        {
            ChangeColor();
        }
       
	}

    void ChangeColor()
    {
        color.a += Time.deltaTime / 2;

        switch (currentText)
        {
            case 1:
                Bully1.color = color;
                break;

            case 2:
                Bully2.color = color;
                break;
            case 3:
                Bully3.color = color;
                break;
        }

        if (color.a > 1)
        {
            color.a = 0;
            currentText++;
        }
    }
}
