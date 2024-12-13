using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class animateHandInput : MonoBehaviour
{
    public InputActionProperty pinchAnimatiionAction;
    public InputActionProperty gripAnimatiionAction;
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimatiionAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        float gripValue = gripAnimatiionAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
