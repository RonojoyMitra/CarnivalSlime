using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreReview : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comment;
    public TMP_Text nextDay;
    public string gradeString;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.score >= 9.0f)
        {
            gradeString = "A";
            comment.text = "They love you, slime!";
        }
        else if (GameManager.Instance.score >= 8.0f)
        {
            gradeString = "B";
            comment.text = "The audience is pleased!";
        }
        else if (GameManager.Instance.score >= 7.0f)
        {
            gradeString = "c";
            comment.text = "Your performance was decent.";
        }
        else if (GameManager.Instance.score >= 6.0f)
        {
            gradeString = "D";
            comment.text = "The audience is displeased.";
        }
        else
        {
            gradeString = "F";
            comment.text = "They hate you, slime!";
        }
        scoreText.text = gradeString;
        if (GameManager.Instance.day < 5)
        {
            nextDay.text += "" + (GameManager.Instance.day + 1);
        }
        else
        {
            nextDay.text = "You performed all 5 days!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && GameManager.Instance.day <= 5)
        {
            SceneManager.LoadScene("Irregular Minigame", LoadSceneMode.Single);
            GameManager.Instance.day++;
        }
    }
}
