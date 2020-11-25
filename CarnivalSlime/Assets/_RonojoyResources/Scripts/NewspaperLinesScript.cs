using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperLinesScript : MonoBehaviour
{
    public TextMesh Date;
    public TextMesh Headline1;
    public TextMesh Headline2;

    public int day;
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        //SetDate();
        //SetHeadline1();
        //SetHeadline2();
    }

    // Update is called once per frame
    void Update()
    {
        SetDate();
        SetHeadline1();
        SetHeadline2();
        day = GameManager.Instance.day;
        score = GameManager.Instance.score;
    }

    void SetDate()
    {
        //int day = GameManager.Instance.day;
        if (day == 1)
        {
            Date.text = "Monday, March 15th 1853";
        }
        if (day == 2)
        {
            Date.text = "Tuesday, March 16th 1853";
        }
        if (day == 3)
        {
            Date.text = "Wednesday, March 17th 1853";
        }
        if (day == 4)
        {
            Date.text = "Thursday, March 18th 1853";
        }
        if (day == 5)
        {
            Date.text = "Friday, March 19th 1853";
        }
    }

    void SetHeadline1()
    {
        //float score = GameManager.Instance.score;
        if (score > 0f)
        {
            Headline1.text = "Barnum's 'Slime' Flops";
            Headline1.fontSize = 15;
        }
        if (score > 3f)
        {
            Headline1.text = "Barnum's 'Slime' falls flat";
            Headline1.fontSize = 13;
        }
        if (score > 5f)
        {
            Headline1.text = "Living Slime pleases";
            Headline1.fontSize = 15;
        }
        if (score > 6f)
        {
            Headline1.text = "Marching Slime entertains";
            Headline1.fontSize = 13;
        }
        if (score > 8.8f)
        {
            Headline1.text = "Moving Slime wows";
            Headline1.fontSize = 15;
        }
    }

    void SetHeadline2()
    {
        //int day = GameManager.Instance.day;
        if (day == 1)
        {
            Headline2.text = "On Opening Night";
            Headline2.fontSize = 15;
        }
        if (day == 2)
        {
            Headline2.text = "On it's second showing";
            Headline2.fontSize = 15;
        }
        if (day == 3)
        {
            Headline2.text = "in wednesday's performance";
            Headline2.fontSize = 12;
        }
        if (day == 4)
        {
            Headline2.text = "on its penultimate show";
            Headline2.fontSize = 14;
        }
        if (day == 5)
        {
            Headline2.text = "On its Final show";
            Headline2.fontSize = 15;
        }
    }
}
