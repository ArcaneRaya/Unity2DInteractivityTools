using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VectorClamp : ValueClamp<Vector3>
{
    public override Vector3 ClampValue(Vector3 vector)
    {
        Vector3 clampedVector = Vector3.zero;
        clampedVector.x = Mathf.Min(Mathf.Max(vector.x, minLimit.x), maxLimit.x);
        clampedVector.y = Mathf.Min(Mathf.Max(vector.y, minLimit.y), maxLimit.y);
        clampedVector.z = Mathf.Min(Mathf.Max(vector.z, minLimit.z), maxLimit.z);
        return clampedVector;
    }
}
