using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarchingTimeManagement : MonoBehaviour
{
    public TextMeshPro timerText;

    // tracks what day in the game it is
    public int day;
    // this float is just to calculate the time that passes
    public float timerFloat;
    // this int is to store the maximum time alloted for this day (6, 10, 14, 18, 22)
    public int timerLevelDisplay;
    // this is what timerText will display
    public int timerDisplay;
    // bool is true when the timer is able to start ticking/working
    public bool startTicking;

    // Start is called before the first frame update
    void Start()
    {
        day = GameManager.Instance.day;
        timerFloat = 0f;
        if (day == 0 || day == 1)
        {
            timerLevelDisplay = 3;
            //timerText.text = "" + 3;
        }
        else if (day == 2)
        {
            timerLevelDisplay = 5;
            //timerText.text = "" + 5;
        }
        else if (day == 3)
        {
            timerLevelDisplay = 7;
            //timerText.text = "" + 7;
        }
        else if (day == 4)
        {
            timerLevelDisplay = 9;
            //timerText.text = "" + 9;
        }
        else if (day == 5)
        {
            timerLevelDisplay = 11;
            //timerText.text = "" + 11;
        }
        timerDisplay = timerLevelDisplay;
        startTicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTicking)
        {
            timerFloat += Time.deltaTime;
            if (timerFloat >= 1f)
            {
                timerFloat = 0f;
                if (timerDisplay > 0)
                {
                    timerDisplay--;
                    timerText.text = "" + timerDisplay;
                }
            }
        }
    }

    public void Reset()
    {
        timerFloat = 0f;
        timerDisplay = timerLevelDisplay;
    }
}
