using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FloatRequirementReachedEvent : RequirementReachedEvent
{
    protected abstract float CheckValue { get; }

    [SerializeField] private FloatRequirementType mode = FloatRequirementType.Equal;
    [SerializeField] private float requirement = 0;
    private float previousValue = 0;

    protected virtual void Start()
    {
        previousValue = CheckValue;
    }

    private void Update()
    {
        if (AllowedToInvoke == false) { return; }

        if (CheckValue != previousValue && HasRequirementBeenReached()){
            SendRequirementReachedEvent();
            previousValue = CheckValue;
        }
    }

    private bool HasRequirementBeenReached()
    {
        switch (mode)
        {
            case FloatRequirementType.SmallerThan:
                return CheckValue < requirement;
            case FloatRequirementType.Equal:
                return CheckValue == requirement;
            case FloatRequirementType.LargerThan:
                return CheckValue > requirement;
            default:
                throw new System.NotImplementedException();
        }
    }

    private enum FloatRequirementType { SmallerThan, Equal, LargerThan }
}
