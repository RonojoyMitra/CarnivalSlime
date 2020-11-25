using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionLinesScript : MonoBehaviour
{
    public Text Date;
    public Text Day;
    // Start is called before the first frame update
    void Start()
    {
        SetDate();
        Day.text = "Day" + GameManager.Instance.day;
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
}
