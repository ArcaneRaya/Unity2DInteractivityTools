using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ValueTransformer <T>
{
    protected abstract T transformTarget { get; set; }

    private enum Mode { Instant, Animated }

    [SerializeField] private Mode transformMode = Mode.Animated;
    [SerializeField] private float changeSpeed = 5;
    [SerializeField] private bool useSnapping = false;
    [SerializeField] private float snapValue = 2;

    private ValueClamp<T> limiter = null;
    private T targetValue = default;
    private T targetValueSnapped = default;

    public void Update()
    {
        if (transformTarget.Equals(targetValue) == false)
        {
            UpdateTransformTargetValue(transformMode);
        }
    }

    public void SetLimiter(ValueClamp<T> limiter)
    {
        this.limiter = limiter;
    }

    public void SetTargetValue(T value)
    {
        value = limiter == null ? value : limiter.ClampValue(value);
        targetValue = value;
        UpdateSnapValue();
    }

    public void SetTargetValueAndApplyInstantly(T value)
    {
        SetTargetValue(value);
        UpdateTransformTargetValue(Mode.Instant);
    }

    public void AddToTargetValue(T value)
    {
        SetTargetValue(AddValues(targetValue,value));
    }

    public void AddToTargetValueAndApplyInstantly(T value)
    {
        SetTargetValueAndApplyInstantly(AddValues(targetValue, value));
    }

    private void UpdateTransformTargetValue(Mode mode)
    {
        T target = useSnapping ? targetValueSnapped : targetValue;
        switch (mode)
        {
            case Mode.Instant:
                transformTarget = target;
                break;
            case Mode.Animated:
                float distanceMultiplier = Mathf.Max(GetMagnitude(transformTarget, target), 0.25f);
                transformTarget = MoveTowards(transformTarget, target, changeSpeed * Time.deltaTime * distanceMultiplier);
                break;
        }
    }

    private void UpdateSnapValue()
    {
        targetValueSnapped = SnapTargetValue(targetValue, snapValue);
        targetValueSnapped = limiter == null ? targetValueSnapped : limiter.ClampValue(targetValueSnapped);
    }

    protected abstract T SnapTargetValue(T value, float snapValue);
    protected abstract T AddValues(T lhs, T rhs);
    protected abstract float GetMagnitude(T currentValue, T targetValue);

    protected abstract T MoveTowards(T currentValue, T targetValue, float maxDelta);
}
