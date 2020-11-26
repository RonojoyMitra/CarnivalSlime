using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrainingIrregular : MonoBehaviour
{
    public int day;

    public KeyboardSystem stageBoard;

    public GameJuiceAudio gameSoundManager;
    public TextScrollSoundManager textScrollSoundManager;

    public Animator barnum;

    public CreateEffect slimeEffect;

    public Animator curtainLeft;
    public Animator curtainRight;

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

    public Animator speechBubble;
    // tutorial exclusives

    public Vector3 leftStartPosition;
    public Vector3 rightStartPosition;

    public TestMarkerScript slime;
    public SlimeStep step;
    public TextMeshPro audienceSentence; // top tmpro
    public TextMeshPro playerInput; // bottom tmpro--will display the correct letters that the player inputs

    public TextAsset sentenceBank;

    public int randomLine;

    public List<string> textLines;

    public int phase;

    public int letterCount;

    public bool leftToRight;

    string boldWord;

    string[] sortedArray;

    public Dictionary<string, int> keyboardAlphabetNumbers = new Dictionary<string, int>();

    public int tracker;

    public bool stun;
    private IEnumerator stunCoroutine;
    private IEnumerator loadNextScene;

    public int rounds;

    void Start()
    {
        MusicManager.Instance.Tutorial();
        randomLine = Random.Range(0, 12);
        letterCount = 4;

        textLines = new List<string>(sentenceBank.text.Split('\n'));
        phase = 0;
        leftToRight = true;
        tracker = 0;
        playerInput.text = "";
        rounds = 0;

        // this is just setting all of the keyboards in a list according to the direction they are in on the keyboard
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

        slime.assignPos(leftStartPosition);
        step.Squish();

        // tutorial exclusives
        dialgueLines = new List<string>(textFile.text.Split('\n'));
        endLine = dialgueLines.Count - 1;
        currentLine = 0;
        tutorialText.text = dialgueLines[currentLine];
        typeSpeed = 0.03f;
        isTyping = false;
        cancelTyping = false;
    }

    // Update is called once per frame
    void Update()
    {
        // this is just the starting scene to start the minigame
        if (phase == 0)
        {
            if (!goText)
            {
                Scroll();
                goText = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && currentLine == endLine && cancelTyping && tutorialText.text == dialgueLines[currentLine])
            {
                speechBubble.SetTrigger("Close");
                barnum.SetTrigger("Tap");
                phase = 1;
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
        // this is an unseen phase that gets the audience comments ready
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
        // this phase is the actual gameplay!
        else if (phase == 2)
        {
            // if the player enters a key
            if (Input.anyKeyDown && !stun)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
                {
                    string inputString = vKey.ToString(); //convert to string
                    int inputID = stageBoard.alphabet.IndexOf(inputString); // find string id (index on alphabet list)

                    if (Input.GetKeyDown(vKey) && inputID < stageBoard.keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
                    {
                        // if the player enters a correct key (and is able to because the slime isn't stunned)
                        if (inputString == sortedArray[tracker])
                        {
                            gameSoundManager.Step();
                            tracker++;
                            playerInput.text += inputString.ToUpper();
                            slime.assignPos(stageBoard.keySprites[inputID].transform.position);
                            step.Squish();
                        }

                        // if the player enters an incorrect key
                        else
                        {
                            slimeEffect.createSad();
                            barnum.SetTrigger("Angry");
                            gameSoundManager.Wrong();
                            if (!stun)
                            {
                                GameManager.Instance.SubtractScore(Random.Range(0.25f, 0.75f));
                            }
                            stun = true;
                            stunCoroutine = Stun();
                            StartCoroutine(stunCoroutine);
                        }
                    }
                }
            }

            // if player correctly types out the word, then reset
            if (tracker == letterCount && Vector3.Distance(slime.gameObject.transform.position, stageBoard.keySprites[stageBoard.alphabet.IndexOf(sortedArray[letterCount - 1])].transform.position) <= 1)
            {
                if (leftToRight)
                {
                    slime.assignPos(rightStartPosition);
                    step.Squish();
                }
                else
                {
                    slime.assignPos(leftStartPosition);
                    step.Squish();
                }
                slimeEffect.createHappy();
                barnum.SetTrigger("Happy");
                gameSoundManager.Win();
                GameManager.Instance.AddScore(Random.Range(1f, 1.5f));
                stun = false;
                leftToRight = !leftToRight;
                tracker = 0;
                randomLine = Random.Range(0, 12);
                phase = 1;
                playerInput.text = "";
                rounds++;
                // if 6 rounds have been completed, go to phase 3
                if (rounds == 6)
                {
                    audienceSentence.text = "You have successfully completed this tutorial!";
                    playerInput.text = "Great job!";
                    phase = 3;
                }
            }
        }

        // this last phase is for when the minigame ends (player completes all rounds)
        else if (phase == 3)
        {
            slime.assignPos(new Vector3(0, 0, 0));
            step.Squish();
            loadNextScene = LoadScene();
            StartCoroutine(loadNextScene);
            phase = 4;
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

    private IEnumerator LoadScene()
    {
        curtainLeft.SetTrigger("Close");
        curtainRight.SetTrigger("Close");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TrainingMarching", LoadSceneMode.Single);
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
        textScrollSoundManager.Scroll();
        while (isTyping && !cancelTyping && (index < currentTextLine.Length - 1))
        {
            tutorialText.text += currentTextLine[index];
            index++;
            yield return new WaitForSeconds(typeSpeed);
        }
        cancelTyping = true;
        textScrollSoundManager.Stop();
        tutorialText.text = dialgueLines[currentLine];
    }
}
