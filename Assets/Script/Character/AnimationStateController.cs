using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController: MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRun()
    {
        animator.SetBool("Run", true);
    }

    public void SetIdle()
    {
        animator.SetBool("Run", false);
    }
}
