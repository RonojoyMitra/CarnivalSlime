using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HoopsMiniGame : MonoBehaviour
{
    public TextMeshPro audienceSentence; // top tmpro
    public bool finishedMinigame;

    public KeyboardSystem Board;

    public AudioController soundManager;
    public GameJuiceAudio gameSoundManager;

    public Animator barnum;

    public CreateEffect slimeEffect;

    public Animator curtainLeft;
    public Animator curtainRight;

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

    public int phases;

    void Start()
    {
        barnum.SetTrigger("Tap");
        // adjust difficulty based on this int (it'll be 0 for training, and 1,2,3,4,5 for the performance days
        day = GameManager.Instance.day;
        if (day != 0)
        {
            soundManager.ShowStarting();
        }
        roundStage = 0;
        occupiedKey = 0;
        currentRound = 0;
        //Board = GetComponent<KeyboardSystem>();

        stagingKey = Random.Range(0,26);
        stagingString = Board.alphabet[stagingKey];
        Debug.Log($"Go To Key {Board.alphabet[stagingKey]}");
        audienceSentence.text = $"Go To Key {Board.alphabet[stagingKey]}";
        GameObject.Find("DIST").GetComponent<TextMeshPro>().text = $"Go To Key {Board.alphabet[stagingKey]}";

        newPos = new Vector3(Marker.position.x, 0, Marker.position.z);
    }

    public AnimationCurve jump;

    void Update()
    {

        if (!finishedMinigame)
        {
            if (roundStage == 0)
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

                            if (Board.isKeyTangent(occupiedKey, inputID))
                            {
                                if (occupiedKey != inputID)
                                {
                                    gameSoundManager.Step();
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
                                newPos = new Vector3(Board.keySprites[occupiedKey].transform.position.x, 0, Board.keySprites[occupiedKey].transform.position.z);
                                initDist = Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos);
                                jumping = true;
                            }
                        }

                    }
                    if (Board.isKeyTangent(occupiedKey, targetKey))
                    {
                        slimeEffect.createHappy();
                        barnum.SetTrigger("Happy");
                        //Debug.Log("SUCCESS!!");
                        gameSoundManager.Win();
                        float randomAudienceReaction = Random.Range(0f, 1f);
                        if (randomAudienceReaction <= 0.5f)
                        {
                            if (day != 0)
                            {
                                soundManager.CrowdClap();
                            }
                        }
                        else
                        {
                            if (day != 0)
                            {
                                soundManager.CrowdLaugh();
                            }
                        }
                        GameObject.Find("Spot Light").GetComponent<Light>().color = Color.green;
                        if (phase + 1 == phases && Vector3.Distance(Marker.gameObject.transform.position, Board.keySprites[stagingKey].transform.position) <= 1)
                        {
                            finishedMinigame = true;
                        }
                    }
                    else
                    {
                        slimeEffect.createSad();
                        barnum.SetTrigger("Angry");
                        //Debug.Log("FAILURE!!");
                        gameSoundManager.Wrong();
                        if (day != 0)
                        {
                            soundManager.CrowdBoo();
                        }
                        GameObject.Find("Spot Light").GetComponent<Light>().color = Color.red;
                    }
                    stagingKey = Random.Range(0, 26);
                    stagingString = Board.alphabet[stagingKey];
                    //Debug.Log($"Go To Key {Board.alphabet[stagingKey]}");
                    audienceSentence.text = $"Go To Key {Board.alphabet[stagingKey]}";
                    GameObject.Find("DIST").GetComponent<TextMeshPro>().text = $"Go To Key {Board.alphabet[stagingKey]}";
                    phase++;
                    roundStage = 0;
                }
            }

            if (jumping)
            {
                Debug.Log((initDist - Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos)) / initDist);

                Slime.localPosition = new Vector3(0, -.84f + 5 * jump.Evaluate((initDist - Vector3.Distance(new Vector3(Marker.position.x, 0, Marker.position.z), newPos)) / initDist), 0);
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
                audienceSentence.text = "You have successfully completed the minigame!";
                if (Vector3.Distance(Marker.gameObject.transform.position, Board.keySprites[stagingKey].transform.position) <= 1.51)
                {
                    finishedMinigame = true;
                }
            }
            Marker.position = Vector3.Lerp(Marker.position, new Vector3(Board.keySprites[occupiedKey].transform.position.x, 1.5f, Board.keySprites[occupiedKey].transform.position.z), Time.deltaTime * 8);
            SlimeController();
            GameObject.Find("Spot Light").GetComponent<Light>().color = Color.Lerp(GameObject.Find("Spot Light").GetComponent<Light>().color, new Color(1, 0.7215686f, 0), Time.deltaTime * 15);
        }
        else
        {
                Marker.position = Vector3.Lerp(Marker.position, new Vector3(Board.keySprites[occupiedKey].transform.position.x, 1.5f, Board.keySprites[occupiedKey].transform.position.z), Time.deltaTime * 8);
                if (day != 0)
                {
                    soundManager.CrowdClapEnd();
                }
                loadNextScene = LoadScene();
                StartCoroutine(loadNextScene);
        }
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
        audienceSentence.text = dist.ToString();
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
        curtainLeft.SetTrigger("Close");
        curtainRight.SetTrigger("Close");
        if (day != 0)
        {
            soundManager.CrowdClapEnd();
        }
        yield return new WaitForSeconds(2f);
        if (day != 0)
        {
            SceneManager.LoadScene("ScoreReview", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("IntroCutscene", LoadSceneMode.Single);
        }
    }
}
