using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMarkerScript : MonoBehaviour
{
    Vector3 positionToMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, positionToMove, Time.deltaTime * 10);
    }

    public void assignPos(Vector3 pos)
    {
        positionToMove = new Vector3(pos.x,1.5f,pos.z);
    }
}
