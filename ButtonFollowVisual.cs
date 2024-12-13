using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5;
    public float followAngleThreshold = 45;

    private bool freeze = false;    

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    private Animator animator;
    private enum AnimationState { Idle, Drop_Flap, Reversed_Flap }
    private AnimationState curState;
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);
        animator = GetComponent<Animator>();
        curState = AnimationState.Idle;
        animator.Play("idle");
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;

            isFollowing = true;
            freeze = false;

            ToggleAnimation();

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if(pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                freeze = false;
            }
        }
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
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

    // Update is called once per frame
    void Update()
    {
        if (freeze)
            return;

        if(isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
