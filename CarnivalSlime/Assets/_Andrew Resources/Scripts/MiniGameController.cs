using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    MinigameValidator Validator;

    public List<string> testingDictionary;

    // Start is called before the first frame update
    void Start()
    {
        Validator = GetComponent<MinigameValidator>();
        Validator.SwitchGame("type it tangent", testingDictionary[Random.Range(0,testingDictionary.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
