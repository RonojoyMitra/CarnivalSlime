using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrainingMarching : MonoBehaviour
{
    public int day;
    public int totalRounds;
    public int currentRound;
    public int numberOfSlimes;

    public KeyboardSystem stageBoard;
    public MarchingTimeManagement timer;

    public GameJuiceAudio gameSoundManager;
    public TimeAudio timeSoundManager;
    public TextScrollSoundManager textScrollSoundManager;

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

    public TestMarkerScript slime1;
    public TestMarkerScript slime2;
    public TestMarkerScript slime3;

    public Vector3 waitPosition1;
    public Vector3 waitPosition2;
    public Vector3 waitPosition3;

    public TextMeshPro audienceSentence; // top tmpro
    public TextMeshPro playerInput; // bottom tmpro--will display the correct letters that the player inputs

    public List<string> queueLetters;
    public List<TestMarkerScript> slimes;
    public Vector3[] previousPositions;
    public int maxLengthLetters;
    public int tracker;
    public int slimeTracker;
    public int removedLetterIndex;
    public bool ranOutOfTime;
    public int randoLetter;

    public int phase;

    public IEnumerator countDownCoroutine;
    private IEnumerator loadNextScene;
    public bool countingDown;

    // Start is called before the first frame update
    void Start()
    {
        day = GameManager.Instance.day;
        maxLengthLetters = 1;
        phase = 0;

        slime1.gameObject.SetActive(true);
        slime2.gameObject.SetActive(true);
        slime3.gameObject.SetActive(true);
        slimes.Add(slime1);
        slimes.Add(slime2);
        slimes.Add(slime3);
        slimes[0].assignPos(waitPosition1);
        slimes[1].assignPos(waitPosition2);
        slimes[2].assignPos(waitPosition3);

        totalRounds = 13; 
        numberOfSlimes = 3;
        previousPositions = new Vector3[numberOfSlimes];
        previousPositions[0] = waitPosition1;
        previousPositions[1] = waitPosition2;
        previousPositions[2] = waitPosition3;

        currentRound = 0;

        // tutorial exclusives
        dialgueLines = new List<string>(textFile.text.Split('\n'));
        currentLine = 0;
        tutorialText.text = dialgueLines[currentLine];
        typeSpeed = 0.03f;
        isTyping = false;
        cancelTyping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0 && !finishedTalking)
        {
            if (!goText)
            {
                Scroll();
                goText = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentLine == endLine && cancelTyping && tutorialText.text == dialgueLines[currentLine])
            {
                tutorialSpeech.SetActive(false);
                countDownCoroutine = CountDown();
                StartCoroutine(countDownCoroutine);
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
        // unseen phase that just selects a random letter 
        if (phase == 1)
        {
            if (!ranOutOfTime)
            {
                if (queueLetters.Count >= numberOfSlimes)
                {
                    removedLetterIndex = stageBoard.alphabet.IndexOf(queueLetters[0]);
                    queueLetters.RemoveAt(0);
                }
                randoLetter = Random.Range(0, 26);
                while (queueLetters.Contains(stageBoard.alphabet[randoLetter]) || removedLetterIndex == randoLetter)
                {
                    randoLetter = Random.Range(0, 26);
                }
                queueLetters.Add(stageBoard.alphabet[randoLetter]);
                tracker = queueLetters.Count - 1;
            }
            audienceSentence.text = "<color=red>" + stageBoard.alphabet[randoLetter] + "</color>";
            playerInput.text = "";
            slimeTracker = 0;
            phase = 2;
            ranOutOfTime = false;
        }
        // this phase is the core gameplay stage but only when SOME slimes are on stage
        else if (phase == 2 && !countingDown)
        {
            // if the player correctly types in all of the letters
            if (tracker == -1)
            {
                gameSoundManager.Win();
                if (maxLengthLetters != numberOfSlimes)
                {
                    maxLengthLetters++;
                    while (slimeTracker < numberOfSlimes)
                    {
                        slimes[slimeTracker].assignPos(previousPositions[slimeTracker - 1]);
                        slimeTracker++;
                    }
                }
                for (int i = 0; i < numberOfSlimes; i++)
                {
                    previousPositions[i] = slimes[i].positionToMove;
                }
                currentRound++;
                if (currentRound < totalRounds)
                {
                    countDownCoroutine = CountDown();
                    StartCoroutine(countDownCoroutine);
                }
                else
                {
                    phase = 3;
                }
                GameManager.Instance.AddScore(Random.Range(0.5f, 1f));
            }
            // if the player enters a key
            if (Input.anyKeyDown && tracker != -1)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
                {
                    string inputString = vKey.ToString(); //convert to string
                    int inputID = stageBoard.alphabet.IndexOf(inputString); // find string id (index on alphabet list)

                    if (Input.GetKeyDown(vKey) && inputID < stageBoard.keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
                    {
                        // if the player enters a correct key (and is able to because the slime isn't stunned)
                        if (inputString == queueLetters[tracker] /*&& !stun*/)
                        {
                            playerInput.text += inputString.ToUpper();
                            slimes[slimeTracker].assignPos(stageBoard.keySprites[inputID].transform.position);
                            slimeTracker++;
                            tracker--;
                            if (tracker != -1)
                            {
                                gameSoundManager.Step();
                            }
                        }

                        // if the player enters an incorrect key
                        else
                        {
                            gameSoundManager.Wrong();
                            GameManager.Instance.SubtractScore(Random.Range(0.25f, 1f));
                            GoBack();
                        }
                    }
                }
            }
        }
        else if (phase == 3)
        {
            audienceSentence.text = "Great job!";
            playerInput.text = "You have successfully completed the minigame!";
            loadNextScene = LoadScene();
            StartCoroutine(loadNextScene);
            phase = 4;
        }
    }

    void GoBack()
    {
        for (int i = 0; i < numberOfSlimes; i++)
        {
            slimes[i].assignPos(previousPositions[i]);
        }
        playerInput.text = "";
        tracker = queueLetters.Count - 1;
        slimeTracker = 0;
    }

    private IEnumerator CountDown()
    {
        //timer.timerText.text = "";
        //timer.startTicking = false;
        countingDown = true;
        timeSoundManager.CountDown();
        audienceSentence.text = "3";
        yield return new WaitForSeconds(1f);
        timeSoundManager.CountDown();
        audienceSentence.text = "2";
        yield return new WaitForSeconds(1f);
        timeSoundManager.CountDown();
        audienceSentence.text = "1";
        yield return new WaitForSeconds(1f);
        phase = 1;
        timeSoundManager.Go();
        //timer.timerText.text = "" + timer.timerLevelDisplay;
        countingDown = false;
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TrainingHoops", LoadSceneMode.Single);
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
