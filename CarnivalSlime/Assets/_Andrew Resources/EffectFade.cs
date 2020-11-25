using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFade : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.up*1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);

        transform.position = Vector3.Lerp(transform.position, endPos+Vector3.up*.1f, Time.deltaTime * 3); //*3 is speed
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one*3,Time.deltaTime * 3.3f);
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(1,1,1,0), Time.deltaTime * 3.3f);
        if (transform.position.y>endPos.y)
        {
            Destroy(this.gameObject);
        }
    }
}
