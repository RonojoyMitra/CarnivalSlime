using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnumScript : MonoBehaviour
{
    public Animator barnum;

    public void ReturnPosition()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            barnum.SetTrigger("Blink");
        }
        else
        {
            barnum.SetTrigger("Tap");
        }
    }
}
