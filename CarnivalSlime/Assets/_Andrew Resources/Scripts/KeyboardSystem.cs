using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSystem : MonoBehaviour
{
    public List<string> alphabet; //a list of all the virtual key strings in order of how they appear on a qwerty keyboard
    public List<bool> keyBools; // a set of correspinding booleans that identify which keys are being pressed

    public List<SpriteRenderer> keySprites; // a set of corresponding spriteRenderers for each key on a Qwerty keyboard

    string inputString; //virtual keypress converted to string
    int inputID; //corresponging number Q=0, Space = 26

    public string LastLetterEntered; //the last key pressed as a string
    public int LastLetterEnteredID; //the last key pressed as an int (corresponds to bool index)

    void Update()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
        {
            inputString = vKey.ToString(); //convert to string
            inputID = alphabet.IndexOf(inputString); // find string id (index on alphabet list)

            if (inputID < keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
            {
                if (Input.GetKey(vKey)) //if this key is being pressed
                {

                    keyBools[inputID] = true; //set corresponding boolean to true
                    LastLetterEntered = inputString;
                    LastLetterEnteredID = inputID;

                }
                else
                {

                    keyBools[inputID] = false;

                }

                if (keyBools[inputID]) //sets color of corresponding sprite to white when pressed
                {
                    keySprites[inputID].color = Color.white;
                }
                else
                {
                    keySprites[inputID].color = Color.Lerp(keySprites[inputID].color, Color.grey,Time.deltaTime * 15); //lerp to achieve nice fade
                }
            }

        }
    }

    public bool isKeyTangent(int currentKey, int inputKey)
    {

        switch (currentKey)
        {
            case 0:
                return inputKey == (1) || 
                    inputKey == (10) || 
                    inputKey == (0);
            case 1:
                return inputKey == (0) || 
                    inputKey == (1) || 
                    inputKey == (2) || 
                    inputKey == (10) || 
                    inputKey == (11);
            case 2:
                return inputKey == (1) || 
                    inputKey == (2) || 
                    inputKey == (3) || 
                    inputKey == (11) || 
                    inputKey == (12);
            case 3:
                return inputKey == (2) || 
                    inputKey == (3) || 
                    inputKey == (4) || 
                    inputKey == (12) || 
                    inputKey == (13);
            case 4:
                return inputKey == (3) || 
                    inputKey == (4) || 
                    inputKey == (5) || 
                    inputKey == (13) || 
                    inputKey == (14);
            case 5:
                return inputKey == (4) || 
                    inputKey == (5) || 
                    inputKey == (6) || 
                    inputKey == (14) || 
                    inputKey == (15);
            case 6:
                return inputKey == (5) || 
                    inputKey == (6) ||
                    inputKey == (7) ||
                    inputKey == (15) ||
                    inputKey == (16);
            case 7:
                return inputKey == (6) || 
                    inputKey == (7) || 
                    inputKey == (8) || 
                    inputKey == (16) || 
                    inputKey == (17);
            case 8:
                return inputKey == (7) || 
                    inputKey == (8) || 
                    inputKey == (9) || 
                    inputKey == (17) || 
                    inputKey == (18);
            case 9:
                return inputKey == (8) || 
                    inputKey == (9) || 
                    inputKey == (18);
            case 10:
                return inputKey == (0) || 
                    inputKey == (1) || 
                    inputKey == (10) || 
                    inputKey == (11) || 
                    inputKey == (19);
            case 11:
                return inputKey == (1) || 
                    inputKey == (2) || 
                    inputKey == (10) || 
                    inputKey == (11) || 
                    inputKey == (12) || 
                    inputKey == (19) || 
                    inputKey == (20);
            case 12:
                return inputKey == (2) || 
                    inputKey == (3) || 
                    inputKey == (11) || 
                    inputKey == (12) || 
                    inputKey == (13) || 
                    inputKey == (20) || 
                    inputKey == (21);
            case 13:
                return inputKey == (3) || 
                    inputKey == (4) || 
                    inputKey == (12) || 
                    inputKey == (13) || 
                    inputKey == (14) || 
                    inputKey == (21) || 
                    inputKey == (22);
            case 14:
                return inputKey == (4) || 
                    inputKey == (5) || 
                    inputKey == (13) || 
                    inputKey == (14) || 
                    inputKey == (15) || 
                    inputKey == (22) || 
                    inputKey == (23);
            case 15:
                return inputKey == (5) || 
                    inputKey == (6) || 
                    inputKey == (14) || 
                    inputKey == (15) || 
                    inputKey == (16) || 
                    inputKey == (23) || 
                    inputKey == (24);
            case 16:
                return inputKey == (6) || 
                    inputKey == (7) ||
                    inputKey == (15) ||
                    inputKey == (16) ||
                    inputKey == (17) ||
                    inputKey == (24) ||
                    inputKey == (25);
            case 17:
                return inputKey == (7) ||
                    inputKey == (8) || 
                    inputKey == (16) || 
                    inputKey == (17) || 
                    inputKey == (18) || 
                    inputKey == (25);
            case 18:
                return inputKey == (8) || 
                    inputKey == (9) || 
                    inputKey == (17) || 
                    inputKey == (18);
            case 19:
                return inputKey == (10) || 
                    inputKey == (11) || 
                    inputKey == (19) || 
                    inputKey == (20);
            case 20:
                return inputKey == (11) || 
                    inputKey == (12) || 
                    inputKey == (19) || 
                    inputKey == (20) || 
                    inputKey == (21) || 
                    inputKey == (26);
            case 21:
                return inputKey == (12) || 
                    inputKey == (13) || 
                    inputKey == (20) || 
                    inputKey == (21) || 
                    inputKey == (22) || 
                    inputKey == (26);
            case 22:
                return inputKey == (13) || 
                    inputKey == (14) || 
                    inputKey == (21) || 
                    inputKey == (22) || 
                    inputKey == (23) || 
                    inputKey == (26);
            case 23:
                return inputKey == (14) || 
                    inputKey == (15) || 
                    inputKey == (22) || 
                    inputKey == (23) || 
                    inputKey == (24) || 
                    inputKey == (26);
            case 24:
                return inputKey == (15) || 
                    inputKey == (16) || 
                    inputKey == (23) || 
                    inputKey == (24) || 
                    inputKey == (25) || 
                    inputKey == (26);
            case 25:
                return inputKey == (16) || 
                    inputKey == (17) || 
                    inputKey == (24) || 
                    inputKey == (25) || 
                    inputKey == (26);
            case 26:
                return inputKey == (20) || 
                    inputKey == (21) || 
                    inputKey == (22) || 
                    inputKey == (23) || 
                    inputKey == (24) || 
                    inputKey == (25) || 
                    inputKey == (26);
            default:
                break;
        }
        return false;
    }
}
