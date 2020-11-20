using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoopsMiniGame : MonoBehaviour
{
    KeyboardSystem Board;

    public int occupiedKey;

    public int stagingKey;
    public string stagingString;

    public int targetKey;


    public string jumpCommand;
    string[] directions = { "North", "North-East", "East", "South-East", "South", "South-West", "West", "North-West" };

    public int currentRound = 0;

    public int roundStage = 0;
    // 0 = get to area
    // 1 = go to hoops

    public Transform Marker; 

    void Start()
    {
        roundStage = 0;
        occupiedKey = 0;
        currentRound = 0;
        Board = GetComponent<KeyboardSystem>();

        stagingKey = Random.Range(0,26);
        stagingString = Board.alphabet[stagingKey];
        Debug.Log($"Go To Key {Board.alphabet[stagingKey]}");
        GameObject.Find("DIST").GetComponent<TextMeshPro>().text = $"Go To Key {Board.alphabet[stagingKey]}";
    }

    public AnimationCurve jump;

    void Update()
    {


        if (roundStage==0)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
            {
                string inputString = vKey.ToString(); //convert to string
                int inputID = Board.alphabet.IndexOf(inputString); // find string id (index on alphabet list)

                if (inputID < Board.keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
                {
                    if (Input.GetKey(vKey)) //if this key is being pressed
                    {

                        if (Board.isKeyTangent(occupiedKey,inputID))
                        {
                            occupiedKey = inputID;
                        }

                    }
                }

            }

            if (occupiedKey == stagingKey) { GenerateDistance(); roundStage = 1; }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) // for each virtual key
                {
                    string inputString = vKey.ToString(); //convert to string
                    int inputID = Board.alphabet.IndexOf(inputString); // find string id (index on alphabet list)

                    if (inputID < Board.keyBools.Count && inputID >= 0) //if its a valid input (can be expanded upon)
                    {
                        if (Input.GetKey(vKey)) //if this key is being pressed
                        {

                            occupiedKey = inputID;

                        }
                    }

                }
                    if (Board.isKeyTangent(occupiedKey, targetKey))
                    {
                        Debug.Log("SUCCESS!!");
                        GameObject.Find("Directional Light").GetComponent<Light>().color = Color.green;
                    }
                    else
                    {
                        Debug.Log("FAILURE!!");
                        GameObject.Find("Directional Light").GetComponent<Light>().color = Color.red;
                    }
                    stagingKey = Random.Range(0, 26);
                    stagingString = Board.alphabet[stagingKey];
                    Debug.Log($"Go To Key {Board.alphabet[stagingKey]}");
                    GameObject.Find("DIST").GetComponent<TextMeshPro>().text = $"Go To Key {Board.alphabet[stagingKey]}";
                roundStage = 0;
            }
            
        }

        Marker.position = Vector3.Lerp(Marker.position, new Vector3(Board.keySprites[occupiedKey].transform.position.x, 1.5f, Board.keySprites[occupiedKey].transform.position.z), Time.deltaTime * 16);
        GameObject.Find("Directional Light").GetComponent<Light>().color = Color.Lerp(GameObject.Find("Directional Light").GetComponent<Light>().color,Color.white,Time.deltaTime * 15);
    }
    public Transform pole;
    void GenerateDistance()
    {
        targetKey = Random.Range(0,26);
        float dist = Vector3.Distance(Board.keySprites[occupiedKey].transform.position, Board.keySprites[targetKey].transform.position);
        dist /= 2;
        dist = Mathf.Round(dist);
        dist -= 1;
        dist = Mathf.Clamp(dist,1,9);
        GameObject.Find("DIST").GetComponent<TextMeshPro>().text = dist.ToString();
        //Debug.Log($"The distance between {Board.alphabet[occupiedKey]} and {Board.alphabet[targetKey]} is {dist}");
        Debug.Log($"The distance between {Board.alphabet[occupiedKey]} and ??? is {dist}");
        Vector2 directionVector = new Vector2(Board.keySprites[targetKey].transform.position.x - Board.keySprites[occupiedKey].transform.position.x,
                                        Board.keySprites[targetKey].transform.position.z - Board.keySprites[occupiedKey].transform.position.z);
        Debug.Log($"The Direction is {directionVector.normalized}");
        pole.forward = new Vector3(directionVector.x,0,directionVector.y);
    }

}
