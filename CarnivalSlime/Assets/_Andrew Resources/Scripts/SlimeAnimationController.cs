using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationController : MonoBehaviour
{
    public bool walking = false;
    public bool idling = true;
    public Animator anim;

    public void ToggleWalk()
    {
        walking = true;
        idling = false;
        anim.SetBool("Idling", idling);
        anim.SetBool("Walking",walking);
    }

    public void TriggerJump()
    {
        anim.SetTrigger("Jump");
    }

    public void ToggleIdle()
    {
        idling = true;
        walking = false;
        anim.SetBool("Idling", idling);
        anim.SetBool("Walking", walking);
    }

    public void TriggerCeleb()
    {
        anim.SetTrigger("Celebrate");
    }
}
