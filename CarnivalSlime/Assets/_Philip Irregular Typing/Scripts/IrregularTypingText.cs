using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IrregularTypingText : MonoBehaviour
{
    // starting (offscreen) positions for the slime at the beginning of the trick
    public Vector3 leftStartPosition;
    public Vector3 rightStartPosition;

    public TestMarkerScript slime;
    public TextMeshPro audienceSentence; // top tmpro
    public TextMeshPro playerInput; // bottom tmpro--will display the correct letters that the player inputs

    // this is a text file that contains all of the audience sentences
    // each line in this text file contains a different sentence that an audience member could say
    // the format of this text file is this:
    //         line starts with an index number that states which character the nth letter word starts in the sentence
    //         then, there is a space between the number and the rest of the sentence
    public TextAsset sentenceBank;

    //int that stores which random line to select from sentenceBank
    public int randomLine;

    //this is a list that stores every line from sentenceBank
    public List<string> textLines;

    //there will be two phases
    //      phase 0 is for creating the sentence and putting it in audienceSentence (or on display)
    //      phase 1 is for actually playing the game
    public int phase;
    public int letterCount;

    //the word that the player must type out irregularly
    string boldWord;

    void Start()
    {
        randomLine = Random.Range(0, 12);
        textLines = new List<string>(sentenceBank.text.Split('\n'));
        phase = 0;
        slime.assignPos(leftStartPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0)
        {
            string sourceLine = textLines[randomLine];
            int wordIndex = int.Parse(sourceLine.Substring(0, 3));
            string frontSentence = sourceLine.Substring(3, wordIndex);
            boldWord = sourceLine.Substring(wordIndex + 3, letterCount);
            string backSentence = sourceLine.Substring(wordIndex + letterCount + 3);
            audienceSentence.text = frontSentence + "<color=red>" + boldWord + "</color>" + backSentence; // I HAVE THE WORD AS RED, BUT THIS CAN BE CHANGED
            phase = 1;
        }
        else
        {

        }
    }
}
