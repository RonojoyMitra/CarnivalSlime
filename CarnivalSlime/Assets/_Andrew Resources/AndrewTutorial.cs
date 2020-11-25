using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AndrewTutorial : MonoBehaviour
{

    // tutorial exclusives
    public TextAsset textFile;
    public List<string> dialgueLines;
    public int currentLine;
    public int endLine;

    public GameObject tutorialSpeech;
    public TMP_Text tutorialText;

    public Coroutine currentScrollType;
    public float typeSpeed;       //how fast the scroll works
    public bool isTyping;       //used for checking if textScroll is happening
    public bool cancelTyping;   //used to check if player wants to cancel the scroll
    public bool goText;    //if coroutine is happening

    public bool finishedTalking;
    // tutorial exclusives

    // Start is called before the first frame update
    void Start()
    {
        // tutorial exclusives
        dialgueLines = new List<string>(textFile.text.Split('\n'));
        currentLine = 0;
        tutorialText.text = dialgueLines[currentLine];
        typeSpeed = 0.03f;
        isTyping = false;
        cancelTyping = false;
        endLine = dialgueLines.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedTalking)
        {
            if (!goText)
            {
                Scroll();
                goText = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentLine == endLine && cancelTyping && tutorialText.text == dialgueLines[currentLine])
            {
                GetComponent<HoopsMiniGame>().enabled = true;
                tutorialSpeech.SetActive(false);
                finishedTalking = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !cancelTyping)
            {
                cancelTyping = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && cancelTyping)
            {
                NextLine();
            }
        }
    }



    public void Scroll()
    {
        currentScrollType = StartCoroutine(ScrollTyping());
    }

    public void NextLine()
    {
        StopCoroutine(currentScrollType);
        currentLine++;
        goText = false;
    }

    public IEnumerator ScrollTyping()
    {
        int index = 0;
        tutorialText.text = "";
        isTyping = true;
        cancelTyping = false;
        string currentTextLine = dialgueLines[currentLine];
        while (isTyping && !cancelTyping && (index < currentTextLine.Length - 1))
        {
            tutorialText.text += currentTextLine[index];
            index++;
            yield return new WaitForSeconds(typeSpeed);
        }
        cancelTyping = true;
        tutorialText.text = dialgueLines[currentLine];
    }

}
