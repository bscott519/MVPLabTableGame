using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class animateTable : MonoBehaviour
{
    private Animator animator;
    private enum AnimationState { Idle, Drop_Flap, Reversed_Flap }
    private AnimationState curState;
    void Start()
    {
        animator = GetComponent<Animator>();
        curState = AnimationState.Idle;
        animator.Play("idle");
    }

    public void ToggleAnimation()
    {
        if (animator != null)
        {
            switch (curState)
            {
                case AnimationState.Idle:
                    animator.Play("flap");
                    curState = AnimationState.Drop_Flap;
                    break;

                case AnimationState.Drop_Flap:
                    animator.Play("reverse_flap");
                    curState = AnimationState.Reversed_Flap;
                    break;

                case AnimationState.Reversed_Flap:
                    animator.Play("flap");
                    curState = AnimationState.Drop_Flap;
                    break;
            }

        }

    }
    
}
