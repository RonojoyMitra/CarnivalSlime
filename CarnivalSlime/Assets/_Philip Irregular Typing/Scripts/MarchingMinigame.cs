using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarchingMinigame : MonoBehaviour
{
    public int day;
    public int totalRounds;
    public int currentRound;
    public int numberOfSlimes;

    public KeyboardSystem stageBoard;
    public MarchingTimeManagement timer;

    public TestMarkerScript slime1;
    public TestMarkerScript slime2;
    public TestMarkerScript slime3;
    public TestMarkerScript slime4;
    public TestMarkerScript slime5;
    public TestMarkerScript slime6;
    public TestMarkerScript slime7;

    public Vector3 waitPosition1;
    public Vector3 waitPosition2;
    public Vector3 waitPosition3;
    public Vector3 waitPosition4;

    public TextMeshPro audienceSentence; // top tmpro
    public TextMeshPro playerInput; // bottom tmpro--will display the correct letters that the player inputs

    public List<string> queueLetters;
    public List<TestMarkerScript> slimes;
    public Vector3[] previousPositions;
    public int maxLengthLetters;
    public int tracker;
    public int slimeTracker;
    public int removedLetterIndex;

    public int phase;

    // Start is called before the first frame update
    void Start()
    {
        day = 0;
        maxLengthLetters = 1;
        phase = 0;

        // setting the basic slime stuff that all days share
        slime1.gameObject.SetActive(true);
        slime2.gameObject.SetActive(true);
        slime3.gameObject.SetActive(true);
        slimes.Add(slime1);
        slimes.Add(slime2);
        slimes.Add(slime3);
        slimes[0].assignPos(waitPosition1);
        slimes[1].assignPos(waitPosition2);
        slimes[2].assignPos(waitPosition3);
        // ending of the basic slime stuff that all days share

        if (day == 0 || day == 1)
        {
            totalRounds = 13;
            numberOfSlimes = 3;
            previousPositions = new Vector3[numberOfSlimes];
            slime4.gameObject.SetActive(false);
            slime5.gameObject.SetActive(false);
            slime6.gameObject.SetActive(false);
            slime7.gameObject.SetActive(false);
            previousPositions[0] = waitPosition1;
            previousPositions[1] = waitPosition2;
            previousPositions[2] = waitPosition3;
        }
        else if (day == 2)
        {
            totalRounds = 14;
            numberOfSlimes = 4;
            previousPositions = new Vector3[numberOfSlimes];
            slime4.gameObject.SetActive(true);
            slime5.gameObject.SetActive(false);
            slime6.gameObject.SetActive(false);
            slime7.gameObject.SetActive(false);
            slimes.Add(slime4);
            slimes[3].assignPos(waitPosition4);
            previousPositions[0] = waitPosition1;
            previousPositions[1] = waitPosition2;
            previousPositions[2] = waitPosition3;
            previousPositions[3] = waitPosition4;
        }
        else if (day == 3)
        {
            totalRounds = 15;
            numberOfSlimes = 5;
            previousPositions = new Vector3[numberOfSlimes];
            slime4.gameObject.SetActive(true);
            slime5.gameObject.SetActive(true);
            slime6.gameObject.SetActive(false);
            slime7.gameObject.SetActive(false);
            slimes.Add(slime4);
            slimes.Add(slime5);
            slimes[3].assignPos(waitPosition4);
            slimes[4].assignPos(waitPosition4);
            previousPositions[0] = waitPosition1;
            previousPositions[1] = waitPosition2;
            previousPositions[2] = waitPosition3;
            previousPositions[3] = waitPosition4;
            previousPositions[4] = waitPosition4;
        }
        else if (day == 4)
        {
            totalRounds = 16;
            numberOfSlimes = 6;
            previousPositions = new Vector3[numberOfSlimes];
            slime4.gameObject.SetActive(true);
            slime5.gameObject.SetActive(true);
            slime6.gameObject.SetActive(true);
            slime7.gameObject.SetActive(false);
            slimes.Add(slime4);
            slimes.Add(slime5);
            slimes.Add(slime6);
            slimes[3].assignPos(waitPosition4);
            slimes[4].assignPos(waitPosition4);
            slimes[5].assignPos(waitPosition4);
            previousPositions[0] = waitPosition1;
            previousPositions[1] = waitPosition2;
            previousPositions[2] = waitPosition3;
            previousPositions[3] = waitPosition4;
            previousPositions[4] = waitPosition4;
            previousPositions[5] = waitPosition4;
        }
        else if (day == 5)
        {
            totalRounds = 17;
            numberOfSlimes = 7;
            previousPositions = new Vector3[numberOfSlimes];
            slime4.gameObject.SetActive(true);
            slime5.gameObject.SetActive(true);
            slime6.gameObject.SetActive(true);
            slime7.gameObject.SetActive(true);
            slimes.Add(slime4);
            slimes.Add(slime5);
            slimes.Add(slime6);
            slimes.Add(slime7);
            slimes[3].assignPos(waitPosition4);
            slimes[4].assignPos(waitPosition4);
            slimes[5].assignPos(waitPosition4);
            slimes[6].assignPos(waitPosition4);
            previousPositions[0] = waitPosition1;
            previousPositions[1] = waitPosition2;
            previousPositions[2] = waitPosition3;
            previousPositions[3] = waitPosition4;
            previousPositions[4] = waitPosition4;
            previousPositions[5] = waitPosition4;
            previousPositions[6] = waitPosition4;
        }
        currentRound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                phase = 1;
                timer.startTicking = true;
            }
        }
        // unseen phase that just selects a random letter 
        if (phase == 1)
        {
            if (queueLetters.Count >= numberOfSlimes)
            {
                removedLetterIndex = stageBoard.alphabet.IndexOf(queueLetters[0]);
                queueLetters.RemoveAt(0);
            }
            int randoLetter = Random.Range(0, 26);
            while (queueLetters.Contains(stageBoard.alphabet[randoLetter]) || removedLetterIndex == randoLetter)
            {
                randoLetter = Random.Range(0, 26);
            }
            audienceSentence.text = "" + stageBoard.alphabet[randoLetter];
            queueLetters.Add(stageBoard.alphabet[randoLetter]);
            playerInput.text = "";
            tracker = queueLetters.Count-1;
            slimeTracker = 0;
            phase = 2;
        }
        // this phase is the core gameplay stage but only when SOME slimes are on stage
        else if (phase == 2)
        {
            // if the player correctly types in all of the letters
            if (tracker == -1)
            {
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
                    phase = 1;
                }
                else
                {
                    phase = 3;
                }
                timer.Reset();
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
                        }

                        // if the player enters an incorrect key
                        else
                        {
                            GoBack();
                        }
                    }
                }
            }
            if (timer.timerDisplay <= 0)
            {
                GoBack();
                timer.Reset();
            }
        }
        else if (phase == 3)
        {
            audienceSentence.text = "Great job!";
            playerInput.text = "You have successfully completed the minigame!";
        }
    }

    void GoBack()
    {
        for(int i = 0; i < numberOfSlimes; i++)
        {
            slimes[i].assignPos(previousPositions[i]);
        }
        playerInput.text = "";
        tracker = queueLetters.Count - 1;
        slimeTracker = 0;
    }
}
