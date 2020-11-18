using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // tracks what day it is (0, 1, 2, 3, 4, 5)
    public int day;
    // stores what number break this is during the day (1 before irregular, 2 before marching, 3 before hoops)
    public int breakNumber;
    // keeps the score of each level
    public float score;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        day = 0;
        breakNumber = 0;
        score = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(float addend)
    {
        score += addend;
        if (score > 10f)
        {
            score = 10f;
        }
    }

    public void SubtractScore(float subtrahend)
    {
        score -= subtrahend;
        if (score < 0f)
        {
            score = 0f;
        }
    }
}
