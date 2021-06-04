using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFly : MonoBehaviour
{

    [SerializeField] Animator animator;

    private void fly()
    {
        animator.SetBool("Fly", true);
    }

    private void Land()
    {
        animator.SetBool("Fly", false);
    }

    public void Update()
    {
        
    }
}
