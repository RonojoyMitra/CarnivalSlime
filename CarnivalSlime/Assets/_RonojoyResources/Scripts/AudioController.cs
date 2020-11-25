using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource mainLoopAudioSource;
    public AudioSource secondaryLoopAudioSource;

    public AudioClip crowdMurmur;
    public AudioClip crowdQuietens;
    public AudioClip crowdClap;
    public AudioClip crowdClapEnd;
    public AudioClip crowdLaugh;
    public AudioClip crowdBoo;
    // Start is called before the first frame update
    void Start()
    {
        PreShow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreShow()
    {
        mainLoopAudioSource.Play();
    }

    public void ShowStarting()
    {
        mainLoopAudioSource.Stop();
        mainLoopAudioSource.clip = null;
        secondaryLoopAudioSource.PlayOneShot(crowdQuietens);
    }

    public void CrowdClap()
    {
        secondaryLoopAudioSource.PlayOneShot(crowdClap);
    }

    public void CrowdClapEnd()
    {
        secondaryLoopAudioSource.PlayOneShot(crowdClapEnd);
        mainLoopAudioSource.clip = crowdMurmur;
        mainLoopAudioSource.Play();
    }

    public void CrowdLaugh()
    {
        secondaryLoopAudioSource.PlayOneShot(crowdLaugh);
    }

    public void CrowdBoo()
    {
        secondaryLoopAudioSource.PlayOneShot(crowdBoo);
    }
}
