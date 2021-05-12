using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class VectorTransformer : ValueTransformer<Vector3>
{

    protected override float GetMagnitude(Vector3 currentValue, Vector3 targetValue)
    {
        return (currentValue - targetValue).magnitude;
    }

    protected override Vector3 AddValues(Vector3 lhs, Vector3 rhs)
    {
        return lhs + rhs;
    }

    protected override Vector3 MoveTowards(Vector3 currentValue, Vector3 targetValue, float maxDelta)
    {
        return Vector3.MoveTowards(currentValue, targetValue, maxDelta);
    }

    protected override Vector3 SnapTargetValue(Vector3 value, float snapValue)
    {
        Vector3 snappedVector = new Vector3();
        snappedVector.x = value.x.GetSnappedValue(snapValue);
        snappedVector.y = value.y.GetSnappedValue(snapValue);
        snappedVector.z = value.z.GetSnappedValue(snapValue);
        return snappedVector;
    }
}
