using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FloatTransformer : ValueTransformer<float>
{
    protected override float AddValues(float lhs, float rhs)
    {
        return lhs + rhs;
    }

    protected override float GetMagnitude(float currentValue, float targetValue)
    {
        return Mathf.Abs(currentValue - targetValue);
    }

    protected override float MoveTowards(float currentValue, float targetValue, float maxDelta)
    {
        return Mathf.MoveTowards(currentValue, targetValue, maxDelta);
    }

    protected override float SnapTargetValue(float value, float snapValue)
    {
        return value.GetSnappedValue(snapValue);
    }
}
