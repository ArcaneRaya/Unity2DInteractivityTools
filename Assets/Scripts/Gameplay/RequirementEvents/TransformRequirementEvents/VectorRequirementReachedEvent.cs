using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VectorRequirementReachedEvent : RequirementReachedEvent
{
    protected abstract Vector3 CheckValue { get; }
    protected abstract Vector3 TargetValue { get; }

    [SerializeField] private float maxDistance = 1;
    [SerializeField] private bool ignoreZValue = true;
    private Vector3 previousValue = Vector3.zero;

    private void Start()
    {
        previousValue = CheckValue;
    }

    private void Update()
    {
        if (AllowedToInvoke == false) { return; }

        if (CheckValue != previousValue && HasRequirementBeenReached())
        {
            SendRequirementReachedEvent();
            previousValue = CheckValue;
        }
    }

    protected virtual bool HasRequirementBeenReached()
    {
        if (ignoreZValue)
        {
            return ((Vector2)CheckValue - (Vector2)TargetValue).sqrMagnitude <= maxDistance * maxDistance;
        }
        return (CheckValue - TargetValue).sqrMagnitude <= maxDistance * maxDistance;
    }
}
