using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ObjectTransformer))]
public class SimpleMoveAnimation : SimpleAnimation
{
    private ObjectTransformer transformer = null;

    protected override void Awake()
    {
        base.Awake();
        transformer = GetComponent<ObjectTransformer>();
    }

    protected override Vector3 GetStartValue()
    {
        return transform.position;
    }

    protected override void HandleReset(Vector3 startValue)
    {
        transformer.SetPosition(startValue, true);
    }

    protected override void OnAnimationUpdate(Vector3 currentValue)
    {
        transformer.SetPosition(currentValue, true);
    }
}