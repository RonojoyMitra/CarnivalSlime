using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource source;

    public AudioClip tutorial;
    public AudioClip result;

    private void Awake()
    {
        // this is the magic that makes our GameManager indestructible
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tutorial()
    {
        source.clip = tutorial;
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
        source.clip = null;
    }

    public void Review()
    {
        source.clip = result;
        source.Play();
    }
}
