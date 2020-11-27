using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReviewSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.Review();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && GameManager.Instance.day <= 4)
        {
            SceneManager.LoadScene("TransitionScene", LoadSceneMode.Single);
            GameManager.Instance.day++;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("CreditScene", LoadSceneMode.Single);
        }
    }
}
