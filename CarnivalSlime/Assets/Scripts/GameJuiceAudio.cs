using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJuiceAudio : MonoBehaviour
{
    public AudioSource source;

    public AudioClip success;
    public AudioClip step;
    public AudioClip wrong;

    public void Step()
    {
        source.clip = step;
        source.Play();
    }

    public void Win()
    {
        source.clip = success;
        source.Play();
    }

    public void Wrong()
    {
        source.clip = wrong;
        source.Play();
    }
}
