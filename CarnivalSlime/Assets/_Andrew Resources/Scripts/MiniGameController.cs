using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    MinigameValidator Validator;

    public List<string> testingDictionary;

    public string TempOverride;

    // Start is called before the first frame update
    void Start()
    {
        Validator = GetComponent<MinigameValidator>();
        if (TempOverride == "")
        {
            Validator.SwitchGame("type it tangent", testingDictionary[Random.Range(0, testingDictionary.Count)]);
        }
        else
        {
            Validator.SwitchGame(TempOverride, testingDictionary[Random.Range(0, testingDictionary.Count)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
