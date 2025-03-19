using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TableButton : MonoBehaviour
{
    public Animator animator;
    private enum AnimationState { Idle, Drop_Flap, Reversed_Flap }
    private AnimationState curState;

    void Start()
    { 
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null )
        {
            interactable.selectEntered.AddListener(ToggleAnimation);
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        curState = AnimationState.Idle;
        animator.Play("idle");
    }

    public void ToggleAnimation(SelectEnterEventArgs args)
    {
        if (animator == null) 
        {
            Debug.Log("animator is null");
            return;
        }

        switch (curState)
                    {
                        case AnimationState.Idle:
                            //animator.SetBool("PlayAnimation", true);
                            animator.Play("flap", 0, 0);
                            curState = AnimationState.Drop_Flap;
                            break;

                        case AnimationState.Drop_Flap:
                            //animator.SetBool("PlayAnimation", true);
                            animator.Play("reverse_flap", 0, 0);
                            curState = AnimationState.Reversed_Flap;
                            break;

                        case AnimationState.Reversed_Flap:
                            //animator.SetBool("PlayAnimation", true);
                            animator.Play("flap", 0, 0);
                            curState = AnimationState.Drop_Flap;
                            break;
                    }

    }
}
