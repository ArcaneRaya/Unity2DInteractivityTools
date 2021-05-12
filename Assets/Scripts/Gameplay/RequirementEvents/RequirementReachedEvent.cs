using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RequirementReachedEvent : MonoBehaviour
{
    protected bool AllowedToInvoke
    {
        get {
            return (hasBeenReached == false || canBeRepeated);
        }
    }

    [SerializeField] private UnityEvent onRequirementReached = default;
    [SerializeField] private bool canBeRepeated = false;
    private bool hasBeenReached = false;

    protected void SendRequirementReachedEvent()
    {
        if (AllowedToInvoke)
        {
            onRequirementReached.Invoke();
            hasBeenReached = true;
        }
    }
}
