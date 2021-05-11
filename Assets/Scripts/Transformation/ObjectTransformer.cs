using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ObjectTransformer : MonoBehaviour
{
    public Vector3 eulerRotation { get; private set; }

    [Header("Movement")]
    [SerializeField] private Mode moveMode = default;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private bool useMovementSnapping = false;
    [SerializeField] private float snapMovementDistance = 2;
    [Header("Scaling")]
    [SerializeField] private Mode scaleMode = default;
    [SerializeField] private float scaleSpeed = 1;
    [SerializeField] private Vector3 minimalScale = Vector3.one;
    [SerializeField] private bool useScaleSnapping = false;
    [SerializeField] private float snapScaleDistance = 1;
    [Header("Rotation")]
    [SerializeField] private Mode rotationMode = default;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private bool useRotationSnapping = false;
    [SerializeField] private float snapRotationDegrees = 45;

    private Vector3 targetPosition = Vector3.zero;
    private Vector3 targetSnappedPosition = Vector3.zero;
    private Vector3 targetScale = Vector3.one;
    private Vector3 targetSnappedScale = Vector3.one;
    private Vector3 targetEulerRotation = Vector3.zero;
    private Vector3 targetSnappedEulerRotation = Vector3.zero;

    public void SetPosition(Vector3 position)
    {
        targetPosition = position;
        UpdateTargetSnappedPosition();
    }

    public void AddPosition(Vector3 position)
    {
        targetPosition += position;
        UpdateTargetSnappedPosition();
    }

    public void SetScale(Vector3 scale)
    {
        scale = CheckForMinimalScale(scale);
        targetScale = scale;
        UpdateTargetSnappedScale();
    }

    public void AddScale(Vector3 scale)
    {
        targetScale += scale;
        targetScale = CheckForMinimalScale(targetScale);
        UpdateTargetSnappedScale();
    }

    public void SetRotation(Vector3 eulerAngles)
    {
        targetEulerRotation = eulerAngles;
        UpdateTargetSnappedRotation();
    }

    public void AddRotation(Vector3 eulerAngles)
    {
        targetEulerRotation += eulerAngles;
        UpdateTargetSnappedRotation();
    }

    public void StopMoveAnimationSmooth()
    {
        targetPosition = transform.position + (targetPosition - transform.position) * 0.25f;
    }

    public void StopMoveAnimationInstant()
    {
        targetPosition = transform.position;
    }

    private void Awake()
    {
        targetPosition = transform.position;
        targetScale = transform.localScale;
    }

    private void Update()
    {
        if (transform.position != targetPosition)
        {
            UpdatePosition();
        }

        if(transform.localScale != targetScale)
        {
            UpdateScale();
        }

        if(eulerRotation != targetEulerRotation)
        {
            UpdateRotation();
        }
    }

    private void UpdatePosition()
    {
        Vector3 target = useMovementSnapping ? targetSnappedPosition : targetPosition;
        switch (moveMode)
        {
            case Mode.Instant:
                transform.position = target;
                break;
            case Mode.Animated:
                float distanceMultiplier = Mathf.Max((transform.position - target).magnitude, 0.25f);
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime * distanceMultiplier);
                break;
        }
    }

    private void UpdateTargetSnappedPosition()
    {
        targetSnappedPosition = SnapVector(targetPosition, snapMovementDistance);
    }

    private void UpdateScale()
    {
        Vector3 target = useScaleSnapping ? targetSnappedScale : targetScale;
        switch (scaleMode)
        {
            case Mode.Instant:
                transform.localScale = target;
                break;
            case Mode.Animated:
                float distanceMultiplier = Mathf.Max((transform.localScale - target).magnitude, 0.25f);
                transform.localScale = Vector3.MoveTowards(transform.localScale, target, scaleSpeed * Time.deltaTime * distanceMultiplier);
                break;
        }
    }

    private void UpdateTargetSnappedScale()
    {
        targetSnappedScale = SnapVector(targetScale, snapScaleDistance);
        targetSnappedScale = CheckForMinimalScale(targetSnappedScale);
    }

    private Vector3 CheckForMinimalScale(Vector3 scale)
    {
        scale.x = Mathf.Max(minimalScale.x, scale.x);
        scale.y = Mathf.Max(minimalScale.y, scale.y);
        scale.z = Mathf.Max(minimalScale.z, scale.z);
        return scale;
    }

    private void UpdateRotation()
    {
        Vector3 target = useRotationSnapping ? targetSnappedEulerRotation : targetEulerRotation;
        switch (rotationMode)
        {
            case Mode.Instant:
                eulerRotation = target;
                transform.rotation = Quaternion.Euler(eulerRotation);
                break;
            case Mode.Animated:
                float distanceMultiplier = Mathf.Max((target - eulerRotation).magnitude, 0.25f);
                eulerRotation = Vector3.MoveTowards(eulerRotation, target, rotationSpeed * Time.deltaTime * distanceMultiplier);
                transform.rotation = Quaternion.Euler(eulerRotation);
                break;
        }
    }

    private void UpdateTargetSnappedRotation()
    {
        targetSnappedEulerRotation = SnapVector(targetEulerRotation, snapRotationDegrees);
    }

    private Vector3 SnapVector(Vector3 vector, float snapValue)
    {
        Vector3 snappedVector = new Vector3();
        snappedVector.x = GetSnappedValue(vector.x, snapValue);
        snappedVector.y = GetSnappedValue(vector.y, snapValue);
        snappedVector.z = GetSnappedValue(vector.z, snapValue);
        return snappedVector;
    }

    private float GetSnappedValue(float value, float snapValue)
    {
        return value == 0 ? 0 : 
            (Mathf.Abs(value)                       // make positive for modulo calculations
            - (Mathf.Abs(value) % snapValue)        // snap to grid
            + (Mathf.Round((Mathf.Abs(value) % snapValue) / snapValue) * snapValue))  // if more than half of snapvalue removed, add to go to next position
            * (value / Mathf.Abs(value)             // return to negative if necessary
            );
    }

    private enum Mode { Instant, Animated }
}
