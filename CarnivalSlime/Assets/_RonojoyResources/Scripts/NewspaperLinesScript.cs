using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperLinesScript : MonoBehaviour
{
    public TextMesh Date;
    public TextMesh Headline1;
    public TextMesh Headline2;
    // Start is called before the first frame update
    void Start()
    {
        SetDate();
        SetHeadline1();
        SetHeadline2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDate()
    {
        int day = GameManager.Instance.day;
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
        float score = GameManager.Instance.score;

        if (score < 3f)
        {
            Headline1.text = "Barnum's 'Slime' Flops";
        }
    }

    void SetHeadline2()
    {
        int day = GameManager.Instance.day;
        if (day == 1)
        {
            Headline2.text = "Monday, March 15th 1853";
        }
        if (day == 2)
        {
            Headline2.text = "Tuesday, March 16th 1853";
        }
        if (day == 3)
        {
            Headline2.text = "Wednesday, March 17th 1853";
        }
        if (day == 4)
        {
            Headline2.text = "Thursday, March 18th 1853";
        }
        if (day == 5)
        {
            Headline2.text = "Friday, March 19th 1853";
        }
    }
}
