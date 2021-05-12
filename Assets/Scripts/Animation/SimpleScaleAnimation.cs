using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleScaleAnimation : SimpleVectorAnimation
{
    private ObjectTransformer transformer = null;

    protected override void Awake()
    {
        base.Awake();
        transformer = GetComponent<ObjectTransformer>();
    }

    protected override Vector3 GetStartValue()
    {
        return transform.localScale;
    }

    protected override void HandleReset(Vector3 startValue)
    {
        transformer.SetScaleInstant(startValue);
    }

    protected override void OnAnimationUpdate(Vector3 currentValue)
    {
        transformer.SetScaleInstant(currentValue);
    }
}
