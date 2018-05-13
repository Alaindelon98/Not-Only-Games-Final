using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour {

    public Text Bully1, Bully2, Bully3;
    public Text Names;


    int currentText = 1;
    Color color;
    bool fadedOut, done, fadingOut, appeared;


	void Start () {
        color = Bully1.color;
        color.a = 0;

        Bully1.color = color;
        Bully2.color = color;
        Bully3.color = color;

        Names.color = color;
    }

    // Update is called once per frame
    void Update () {
        if (!done)
        {
            if (currentText < 4)
            {
                ChangeColor();
            }

            else if(!fadedOut)
            {
                if (!fadingOut)
                {
                    color.a = 2;
                    fadingOut = true;
                }
                Debug.Log("fade out");
                AllFadeOut();
            }

            else if (fadedOut && !done)
            {
                Debug.Log("Appear");
                NamesAppear();
            }

            Debug.Log(color.a);

            Debug.Log(currentText + " current");

            
        }
    }

    void NamesAppear()
    {
        color.a += Time.deltaTime / 3;

        Names.color = color;

        if(color.a >= 1)
        done = true;
    }

    void ChangeColor()
    {
        color.a += Time.deltaTime / 3;

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

        if (currentText > 4)
        {
            color.a = 1;
        }
    }

    void AllFadeOut()
    {
        color.a -= Time.deltaTime / 3;

        Bully1.color = color;
        Bully2.color = color;
        Bully3.color = color;

        if (color.a <= 0)
        {
            fadedOut = true;
        }

    }
}
