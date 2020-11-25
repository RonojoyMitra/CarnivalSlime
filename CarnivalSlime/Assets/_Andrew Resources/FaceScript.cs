using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceScript : MonoBehaviour
{
    public Transform slime;

    public SpriteRenderer SR;
    public Sprite opened;
    public Sprite closed;

    bool animPlaying;

    bool blinking;

    float blinkTimer;
    float blinkTime;

    float animDuration;
    float animTimer;

    // Start is called before the first frame update
    void Start()
    {
        slime = GameObject.Find("SlimeAvatar").transform;
        SR=GetComponentInChildren<SpriteRenderer>();
        transform.position = slime.position + new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.position = Vector3.Lerp(transform.position, slime.position+new Vector3(0,1,0),Time.deltaTime*20);

        if (!animPlaying)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkTime)
            {
                animDuration = .1f;
                blinkTime = Random.Range(2, 5f);
                blinkTimer = 0;
                animPlaying = true;
            }
            SR.sprite = opened;
        }
        else
        {
            animTimer += Time.deltaTime;
            if (animTimer>=animDuration)
            {
                animTimer = 0;
                animPlaying = false;
            }
            SR.sprite = closed;
        }
    }
    int frames;
    private void FixedUpdate()
    {
        frames++;
        if (frames%10==0)
        {
            transform.localScale = new Vector3(Random.Range(.9f, 1.1f), Random.Range(.9f, 1.1f), Random.Range(.9f, 1.1f));
        }
    }
}
