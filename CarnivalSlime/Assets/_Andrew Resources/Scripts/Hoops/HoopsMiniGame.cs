using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HoopsMiniGame : MonoBehaviour
{
    public KeyboardSystem Board;

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
    public int phase;
    private IEnumerator loadNextScene;
    public int day;

    public Transform Marker;
    public Transform Slime;
    public SlimeAnimationController slimeAnim;
    public Vector3 slimeForward;

    public float phases;

    void Start()
    {
        // adjust difficulty based on this int (it'll be 0 for training, and 1,2,3,4,5 for the performance days
        day = GameManager.Instance.day;

        roundStage = 0;
        occupiedKey = 0;
        currentRound = 0;
        //Board = GetComponent<KeyboardSystem>();

        stagingKey = Random.Range(0,26);
        stagingString = Board.alphabet[stagingKey];
        Debug.Log($"Go To Key {Board.alphabet[stagingKey]}");
        GameObject.Find("DIST").GetComponent<TextMeshPro>().text = $"Go To Key {Board.alphabet[stagingKey]}";

        newPos = new Vector3(Marker.position.x, 0, Marker.position.z);
    }

    public AnimationCurve jump;

    void Update()
    {


        if (roundStage==0)
        {
            //newPos = new Vector3(Marker.position.x, 0, Marker.position.z);
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
                            if (occupiedKey!=inputID)
                            {
                                slimeForward = Board.keySprites[inputID].transform.position - Board.keySprites[occupiedKey].transform.position;
                                slimeForward.Normalize();
                            }
                            Slime.localScale = new Vector3(0.36029f * 1.2f, 0.36029f * .3f, 0.36029f * 1.2f);
                            occupiedKey = inputID;
                        }

                    }
                }

            }

            if (occupiedKey == stagingKey) { GenerateDistance(); roundStage = 1; }
        }
        else
        {
            //newPos = new Vector3(Marker.position.x, 0, Marker.position.z);
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
                            newPos =  new Vector3(Board.keySprites[occupiedKey].transform.position.x, 0, Board.keySprites[occupiedKey].transform.position.z);
                            initDist = Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos);
                            jumping = true;
                        }
                    }

                }
                    if (Board.isKeyTangent(occupiedKey, targetKey))
                    {
                        Debug.Log("SUCCESS!!");
                        GameObject.Find("Spot Light").GetComponent<Light>().color = Color.green;
                    }
                    else
                    {
                        Debug.Log("FAILURE!!");
                        GameObject.Find("Spot Light").GetComponent<Light>().color = Color.red;
                    }
                    stagingKey = Random.Range(0, 26);
                    stagingString = Board.alphabet[stagingKey];
                    Debug.Log($"Go To Key {Board.alphabet[stagingKey]}");
                    GameObject.Find("DIST").GetComponent<TextMeshPro>().text = $"Go To Key {Board.alphabet[stagingKey]}";
                phase++;
                roundStage = 0;
            } 
        }
        
        if (jumping)
        {
            Debug.Log((initDist - Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos))/initDist);

            Slime.localPosition = new Vector3(0, -.84f + 5*jump.Evaluate((initDist - Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos)) / initDist), 0);
            if (Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos) < .01f)
            {
                jumping = false;
            }
        }
        else
        {
            Slime.localPosition = new Vector3(0, -.84f, 0);
        }

        if (phase == phases)
        {
            Debug.Log("HELLOOOOO");
            loadNextScene = LoadScene();
            StartCoroutine(loadNextScene);
        }
        Marker.position = Vector3.Lerp(Marker.position, new Vector3(Board.keySprites[occupiedKey].transform.position.x, 1.5f, Board.keySprites[occupiedKey].transform.position.z), Time.deltaTime * 8);
        SlimeController();
        GameObject.Find("Spot Light").GetComponent<Light>().color = Color.Lerp(GameObject.Find("Spot Light").GetComponent<Light>().color,new Color(1, 0.7215686f,0),Time.deltaTime * 15);
    }

    float yHeight;
    float initDist;

    bool jumping;
    Vector3 newPos;

    void SlimeController()
    {
        if (roundStage==0)
        {
            Slime.forward = Vector3.Lerp(Slime.forward, slimeForward, Time.deltaTime * 10);
        }
        else
        {
            //Debug.Log(distFromLandingPoint);
            Slime.forward = Vector3.Lerp(Slime.forward, pole.forward, Time.deltaTime * 10);
        }

        Slime.localScale = Vector3.Lerp(Slime.localScale, Vector3.one * 0.36029f, Time.deltaTime * 25);
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

    /*
     * remember to make a copy of this script in the TRAINING SCENE and make it loadScene("Cutscene 2")
    */
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ScoreReview", LoadSceneMode.Single);
    }
}
