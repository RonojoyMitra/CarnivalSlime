using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameValidator : MonoBehaviour
{
    //the idea of this script broadly:
    //somewhere, we set which minigame is active and feed a string to this script identifying which minigame is active. (for example: "type it")
    //This script then sets the 'minigameWon' variable to the win condition of the corresponding minigame. For example, in the "type it" minigame, we would set the win condition to
    // "is the correct button pressed?" AKA (minigameWon = stageBoard.keyBools[ID OF CORRECT KEY])

    // info about which is correct also needs to be fed to this script. 


    KeyboardSystem stageBoard;
    MiniGameController controller;

    public string minigameID;
    public string validString;

    public string activeString;
    public int activeStringIndex;

    public bool minigameWon;

    //tangent
    public int currentOccupiedKey;

    void Awake()
    {
        stageBoard = GetComponent<KeyboardSystem>();
        controller = GetComponent<MiniGameController>();
    }

    public void SwitchGame(string game, string winningString)
    {
        minigameID = game;
        validString = winningString;
        activeString = "";
        activeStringIndex = 0;

        currentOccupiedKey = 0; //for now just set to 'Q'

        GameObject.Find("TEST MARKER").GetComponent<TestMarkerScript>().assignPos(stageBoard.keySprites[currentOccupiedKey].transform.position);
    }

    void Update()
    {
        switch (minigameID)
        {
            case "type it":
                GameTypeIt();
                break;
            case "type it tangent":
                TypeItTangent();
                break;
            default:
                break;
        }

        if (minigameWon)
        {
            //SwitchGame("type it", controller.testingDictionary[Random.Range(0, controller.testingDictionary.Count)]);
        }
    }

    void TypeItTangent()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
            {
                string inputString = vKey.ToString(); //convert to string
                int inputID = stageBoard.alphabet.IndexOf(inputString); // find string id (index on alphabet list)

                if (inputID < stageBoard.keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
                {

                    if (stageBoard.isKeyTangent(currentOccupiedKey, inputID) && Input.GetKey(vKey))
                    {
                        Debug.Log("TANGENT PRESSED");
                        currentOccupiedKey = inputID;
                        GameObject.Find("TEST MARKER").GetComponent<TestMarkerScript>().assignPos(stageBoard.keySprites[currentOccupiedKey].transform.position);
                    }
                }

            }
        }
    }

    void GameTypeIt()
    {
        if (Input.anyKeyDown)
        {
            int currentID = stageBoard.alphabet.IndexOf(validString[activeStringIndex].ToString());
            //if the current letter is being pressed. this condition can be changed to fit the win condition.
            // for example, we can ask the player to spell out a word, but they have to approach each letter by touching a tangent letter (referencing the last key pressed variable).   
            if (stageBoard.keyBools[currentID]) 
            {
                activeString += validString[activeStringIndex];
                activeStringIndex += 1;
            }
        }

        minigameWon = activeStringIndex >= validString.Length;       
    }
}
