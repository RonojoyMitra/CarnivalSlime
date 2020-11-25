using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrollSoundManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip scroll;

    public void Scroll()
    {
        source.clip = scroll;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
        source.clip = null;
    }
}
