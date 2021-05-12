using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ObjectTransformer : MonoBehaviour
{
    private Vector3 eulerRotation;

    [SerializeField] private PositionVectorTransformer positionTransformer = default;
    [SerializeField] private ScaleVectorTransformer scaleTransformer = default;
    [SerializeField] private RotationVectorTransformer rotationTransformer= default;

    public void SetPosition(Vector3 position)
    {
        positionTransformer.SetTargetValue(position);
    }

    public void SetPositionInstant(Vector3 position)
    {
        positionTransformer.SetTargetValueAndApplyInstantly(position);
    }

    public void AddPosition(Vector3 position)
    {
        positionTransformer.AddToTargetValue(position);
    }

    public void AddPositionInstant(Vector3 position)
    {
        positionTransformer.AddToTargetValueAndApplyInstantly(position);
    }

    public void SetScale(Vector3 scale)
    {
        scaleTransformer.SetTargetValue(scale);
    }

    public void SetScaleInstant(Vector3 scale)
    {
        scaleTransformer.SetTargetValueAndApplyInstantly(scale);
    }

    public void AddScale(Vector3 scale)
    {
        scaleTransformer.AddToTargetValue(scale);
    }

    public void AddScaleInstant(Vector3 scale)
    {
        scaleTransformer.AddToTargetValueAndApplyInstantly(scale);
    }

    public void SetRotation(Vector3 eulerAngles)
    {
        rotationTransformer.SetTargetValue(eulerAngles);
    }

    public void SetRotationInstant(Vector3 eulerAngles)
    {
        rotationTransformer.SetTargetValueAndApplyInstantly(eulerAngles);
    }

    public void AddRotation(Vector3 eulerAngles)
    {
        rotationTransformer.AddToTargetValue(eulerAngles);
    }

    public void AddRotationInstant(Vector3 eulerAngles)
    {
        rotationTransformer.AddToTargetValueAndApplyInstantly(eulerAngles);
    }

    private void Awake()
    {
        positionTransformer.SetLinkedTransform(transform);
        scaleTransformer.SetLinkedTransform(transform);
        rotationTransformer.SetLinkedTransform(transform);

        positionTransformer.SetLimiter(GetComponent<LimitMovement>());
        scaleTransformer.SetLimiter(GetComponent<LimitScale>());
        rotationTransformer.SetLimiter(GetComponent<LimitRotation>());
    }

    private void Update()
    {
        positionTransformer.Update();
        scaleTransformer.Update();
        rotationTransformer.Update();
    }
}
