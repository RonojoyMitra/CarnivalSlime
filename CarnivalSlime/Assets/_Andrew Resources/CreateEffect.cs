using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffect : MonoBehaviour
{
    public Sprite happyEffect;
    public Sprite sadEffect;

    public GameObject effect;

    public void createHappy()
    {
        GameObject FX = Instantiate(effect,transform.position,Quaternion.identity);
        FX.GetComponent<SpriteRenderer>().sprite = happyEffect;
    }

    public void createSad()
    {
        GameObject FX = Instantiate(effect, transform.position, Quaternion.identity);
        FX.GetComponent<SpriteRenderer>().sprite = sadEffect;
    }
}
