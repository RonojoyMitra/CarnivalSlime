using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStopper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.StopMusic();
        GameManager.Instance.day = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
