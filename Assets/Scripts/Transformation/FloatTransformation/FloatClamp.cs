using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatClamp : ValueClamp<float>
{
    public override float ClampValue(float value)
    {
        return Mathf.Clamp(value, minLimit, maxLimit);
    }
}
