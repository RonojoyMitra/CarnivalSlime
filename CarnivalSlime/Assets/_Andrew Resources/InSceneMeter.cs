using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InSceneMeter : MonoBehaviour
{
    public Slider slider;

    float MaxScore;
    // Start is called before the first frame update
    void Start()
    {
        MaxScore = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(slider.value/MaxScore,1,1),Time.deltaTime * 10);
    }
}
