using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IrregularTimeManagement : MonoBehaviour
{
    public TextMeshPro timerText;

    // this float is just to calculate the time that passes
    public float timerFloat;
    // this int is to store the maximum time alloted for this day (6, 19, 14, 18, 22)
    public int timerLevelDisplay;
    // this is what timerText will display
    public int timerDisplay;
    // bool is true when the timer is able to start ticking/working
    public bool startTicking;

    // Start is called before the first frame update
    void Start()
    {
        timerFloat = 0f;
        timerLevelDisplay = 6;
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
        timerText.text = "" + timerDisplay;
    }
}
