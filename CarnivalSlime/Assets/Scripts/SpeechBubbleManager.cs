using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleManager : MonoBehaviour
{
    public void CloseGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
