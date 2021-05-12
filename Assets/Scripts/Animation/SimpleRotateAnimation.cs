using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotateAnimation : SimpleAnimation
{
    private ObjectTransformer transformer = null;

    protected override void Awake()
    {
        base.Awake();
        transformer = GetComponent<ObjectTransformer>();
    }

    protected override Vector3 GetStartValue()
    {
        return transform.rotation.eulerAngles;
    }

    protected override void HandleReset(Vector3 startValue)
    {
        transformer.SetRotationInstant(startValue);
    }

    protected override void OnAnimationUpdate(Vector3 currentValue)
    {
        transformer.SetRotationInstant(currentValue);
    }
}
