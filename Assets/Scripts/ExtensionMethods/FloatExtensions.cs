using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions
{
    public static float GetSnappedValue(this float value, float snapValue)
    {
        return value == 0 ? 0 :
            (Mathf.Abs(value)                       // make positive for modulo calculations
            - (Mathf.Abs(value) % snapValue)        // snap to grid
            + (Mathf.Round((Mathf.Abs(value) % snapValue) / snapValue) * snapValue))  // if more than half of snapvalue removed, add to go to next position
            * (value / Mathf.Abs(value)             // return to negative if necessary
            );
    }
}
