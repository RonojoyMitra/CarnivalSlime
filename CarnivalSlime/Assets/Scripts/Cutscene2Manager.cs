using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Cutscene2Manager : MonoBehaviour
{
    PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("TransitionScene", LoadSceneMode.Single);
            GameManager.Instance.day = 1;
        }
    }

    public void LoadMinigame()
    {
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Single);
        GameManager.Instance.day = 1;
    }
}
