using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStep : MonoBehaviour
{
    public bool lookAtSomething;
    public Transform lookAtThis;
    // Start is called before the first frame update
    void Start()
    {
        lookAtSomething = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtSomething)
        {
            transform.LookAt(lookAtThis);
        }

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.36029f, Time.deltaTime * 15);
    }

    public void Squish()
    {
        transform.localScale = new Vector3(0.36029f*1.2f, 0.36029f*.6f, 0.36029f * 1.2f);
    }
}
