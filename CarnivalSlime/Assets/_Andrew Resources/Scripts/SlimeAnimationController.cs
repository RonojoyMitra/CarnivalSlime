using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationController : MonoBehaviour
{
    public bool walking = false;
    public bool idling = true;
    Animator anim;

    public void ToggleWalk()
    {
        walking = !walking;
        anim.SetBool("Walking",walking);
    }

    public void TriggerJump()
    {
        anim.SetTrigger("Jump");
    }

    public void ToggleIdle()
    {
        idling = !idling;
        anim.SetBool("Idling",idling);
    }

    public void TriggerCeleb()
    {
        anim.SetTrigger("Celebrate");
    }
}
