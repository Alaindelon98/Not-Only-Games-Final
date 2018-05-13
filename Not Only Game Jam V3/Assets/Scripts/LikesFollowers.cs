using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikesFollowers : MonoBehaviour
{

    [SerializeField]
    Text likesText, followersText;
    int likesTarget;
    int likesNum, followersNum;

    public int[] likesArray, followersArray;


    int currentPhoto;
    void Start()
    {
        likesNum = 0;
        followersNum = 0;

        likesText.text = likesNum.ToString() + " Likes";
        followersText.text = followersNum.ToString() + " Following";
    }



    void Update()
    {
        //if (change photo)
        //{
        //    likesText = 0;
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPhoto++;
            if (currentPhoto >= likesArray.Length)
            {
                currentPhoto = 0;
            }
            ChangePhotoText();

        }
    }

    void ChangePhotoText()
    {
        Debug.Log("Current photo: " + currentPhoto);
        likesNum = 0;
        likesTarget = likesArray[currentPhoto] + Random.Range(0, 6);
        if (currentPhoto != 0 && currentPhoto != 7)
        {
            //followersNum = followersArray[currentPhoto];
            Debug.Log("Start counting followers");
            StartCoroutine(AddFollowers());


        }
        StartCoroutine(AddLikes());

        likesText.text = likesNum.ToString() + " Likes";
        followersText.text = followersNum.ToString() + " Following";

    }

    IEnumerator AddLikes()
    {
        while (likesNum < likesTarget)
        {
            likesNum += Random.Range(1, 10);


            if (likesNum >= likesTarget)
            {
                likesNum = likesTarget;
                likesText.text = likesNum.ToString() + " Likes";
            }

            else
            {
                likesText.text = likesNum.ToString() + " Likes";

                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
            }
        }
        yield break;
    }

    IEnumerator AddFollowers()
    {
        while (followersNum < followersArray[currentPhoto])
        {

            followersNum++;





            if (followersNum >= followersArray[currentPhoto])
            {
                followersNum = followersArray[currentPhoto];
                followersText.text = followersNum.ToString() + " Followers";


            }

            else
            {
                followersText.text = followersNum.ToString() + " Followers";


                yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));
            }

            //if (iles)
        }
    
    yield break;
    }
}