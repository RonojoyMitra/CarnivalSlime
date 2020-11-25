using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAudio : MonoBehaviour
{
    public AudioSource source;

    public AudioClip tick;
    public AudioClip countdown;
    public AudioClip go;

    public void Tick()
    {
        source.clip = tick;
        source.Play();
    }

    public void CountDown()
    {
        source.clip = countdown;
        source.Play();
    }

    public void Go()
    {
        source.clip = go;
        source.Play();
    }
}
