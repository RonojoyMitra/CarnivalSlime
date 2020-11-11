using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IrregularTypingText : MonoBehaviour
{
    public KeyboardSystem stageBoard;
    public IrregularTimeManagement timer;

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
    //      phase 1 is for actually playing the game (letting the player input text)
    public int phase;

    // letter count is how many letters the bolded word has
    // can be 4, 5, 6, 7, or 8 (depends on what day it is)
    public int letterCount;

    // this bool is true when the player must type the letters from left to right
    // this is false when player must type letters from right to left
    public bool leftToRight;

    //the word that the player must type out irregularly
    string boldWord;

    string[] sortedArray;

    //dictionary that stores every letter in the alphabet with its order number from left to right on the keyboard
    public Dictionary<string, int> keyboardAlphabetNumbers = new Dictionary<string, int>();

    //int variable that just tracks how many letters have been corrected inputted already
    public int tracker;

    //this bool is true when the player makes a mistake and must wait for a stun animation to end
    public bool stun;
    private IEnumerator stunCoroutine;

    void Start()
    {
        randomLine = Random.Range(0, 12);
        textLines = new List<string>(sentenceBank.text.Split('\n'));
        phase = 0;
        leftToRight = true;
        tracker = 0;
        playerInput.text = "";
        keyboardAlphabetNumbers.Add("Q", 0);
        keyboardAlphabetNumbers.Add("A", 1);
        keyboardAlphabetNumbers.Add("Z", 2);
        keyboardAlphabetNumbers.Add("W", 3);
        keyboardAlphabetNumbers.Add("S", 4);
        keyboardAlphabetNumbers.Add("X", 5);
        keyboardAlphabetNumbers.Add("E", 6);
        keyboardAlphabetNumbers.Add("D", 7);
        keyboardAlphabetNumbers.Add("C", 8);
        keyboardAlphabetNumbers.Add("R", 9);
        keyboardAlphabetNumbers.Add("F", 10);
        keyboardAlphabetNumbers.Add("V", 11);
        keyboardAlphabetNumbers.Add("T", 12);
        keyboardAlphabetNumbers.Add("G", 13);
        keyboardAlphabetNumbers.Add("B", 14);
        keyboardAlphabetNumbers.Add("Y", 15);
        keyboardAlphabetNumbers.Add("H", 16);
        keyboardAlphabetNumbers.Add("N", 17);
        keyboardAlphabetNumbers.Add("U", 18);
        keyboardAlphabetNumbers.Add("J", 19);
        keyboardAlphabetNumbers.Add("M", 20);
        keyboardAlphabetNumbers.Add("I", 21);
        keyboardAlphabetNumbers.Add("K", 22);
        keyboardAlphabetNumbers.Add("O", 23);
        keyboardAlphabetNumbers.Add("L", 24);
        keyboardAlphabetNumbers.Add("P", 25);
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                phase = 1;
                slime.assignPos(leftStartPosition);
                timer.startTicking = true;
            }
        }
        else if (phase == 1)
        {
            string sourceLine = textLines[randomLine];
            int wordIndex = int.Parse(sourceLine.Substring(0, 3));
            string frontSentence = sourceLine.Substring(3, wordIndex);
            boldWord = sourceLine.Substring(wordIndex + 3, letterCount);
            string backSentence = sourceLine.Substring(wordIndex + letterCount + 3);
            audienceSentence.text = frontSentence + "<color=red>" + boldWord + "</color>" + backSentence; // I HAVE THE WORD AS RED, BUT THIS CAN BE CHANGED

            if (leftToRight)
            {
                sortedArray = SelectionSortLeftToRight(boldWord);
            }
            else
            {
                sortedArray = SelectionSortRightToLeft(boldWord);
            }

            phase = 2;
        }
        else
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
                {
                    string inputString = vKey.ToString(); //convert to string
                    int inputID = stageBoard.alphabet.IndexOf(inputString); // find string id (index on alphabet list)

                    if (Input.GetKeyDown(vKey) && inputID < stageBoard.keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
                    {
                        if (inputString == sortedArray[tracker] && !stun)
                        {
                            tracker++;
                            playerInput.text += inputString.ToUpper();
                            slime.assignPos(stageBoard.keySprites[inputID].transform.position);
                        }
                        else
                        {
                            Debug.Log("STUN");
                            stun = true;
                            stunCoroutine = Stun();
                            StartCoroutine(stunCoroutine);
                        }
                    }
                }
            }
            if (tracker == letterCount && Vector3.Distance(slime.gameObject.transform.position, stageBoard.keySprites[stageBoard.alphabet.IndexOf(sortedArray[letterCount-1])].transform.position) <= 1)
            {
                if (leftToRight)
                {
                    slime.assignPos(rightStartPosition);
                }
                else
                {
                    slime.assignPos(leftStartPosition);
                }
                stun = false;
                leftToRight = !leftToRight;
                tracker = 0;
                randomLine = Random.Range(0, 12);
                phase = 1;
                playerInput.text = "";
                timer.Reset();
            }
            if (timer.timerDisplay == 0)
            {
                if (leftToRight)
                {
                    slime.assignPos(leftStartPosition);
                }
                else
                {
                    slime.assignPos(rightStartPosition);
                }
                stun = false;
                tracker = 0;
                phase = 1;
                playerInput.text = "";
                timer.Reset();
            }
        }
    }

    public string[] SelectionSortLeftToRight(string word)
    {
        string[] answers = new string[letterCount];
        List<string> startingArray = new List<string>();
        for (int i = 0; i < letterCount; i++)
        {
            string character = word.Substring(i, 1).ToUpper();
            startingArray.Add(character);
        }

        for (int i = 0; i < letterCount; i++)
        {
            string lowestCharacter = startingArray[0];
            for (int j = 0; j < startingArray.Count; j++)
            {
                if (keyboardAlphabetNumbers[lowestCharacter] > keyboardAlphabetNumbers[startingArray[j]])
                {
                    lowestCharacter = startingArray[j];
                }
            }
            startingArray.Remove(lowestCharacter);
            answers[i] = lowestCharacter;
        }
        return answers;
    }

    public string[] SelectionSortRightToLeft(string word)
    {
        string[] answers = new string[letterCount];
        List<string> startingArray = new List<string>();
        for (int i = 0; i < letterCount; i++)
        {
            string character = word.Substring(i, 1).ToUpper();
            startingArray.Add(character);
        }

        for (int i = 0; i < letterCount; i++)
        {
            string highestCharacter = startingArray[0];
            for (int j = 0; j < startingArray.Count; j++)
            {
                if (keyboardAlphabetNumbers[highestCharacter] < keyboardAlphabetNumbers[startingArray[j]])
                {
                    highestCharacter = startingArray[j];
                }
            }
            startingArray.Remove(highestCharacter);
            answers[i] = highestCharacter;
        }
        return answers;
    }

    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(2f);
        stun = false;
    }
}
