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
    [Header("Scaling")]
    [SerializeField] private Mode scaleMode = default;
    [SerializeField] private float scaleSpeed = 1;
    [SerializeField] private Vector3 minimalScale = Vector3.one;
    [Header("Rotation")]
    [SerializeField] private Mode rotationMode = default;
    [SerializeField] private float rotationSpeed = 1;

    private Vector3 targetPosition = Vector3.zero;
    private Vector3 targetScale = Vector3.one;
    private Vector3 targetEulerRotation = Vector3.zero;

    public void SetPosition(Vector3 position)
    {
        switch (moveMode)
        {
            case Mode.Instant:
                targetPosition = position;
                transform.position = position;
                break;
            case Mode.Animated:
                targetPosition = position;
                break;
        }
    }

    public void SetScale(Vector3 scale)
    {
        scale.x = Mathf.Max(minimalScale.x, scale.x);
        scale.y = Mathf.Max(minimalScale.y, scale.y);
        scale.z = Mathf.Max(minimalScale.z, scale.z);

        switch (scaleMode)
        {
            case Mode.Instant:
                targetScale = scale;
                transform.localScale = scale;
                break;
            case Mode.Animated:
                targetScale = scale;
                break;
        }
    }

    public void SetRotation(Vector3 eulerAngles)
    {
        switch (rotationMode)
        {
            case Mode.Instant:
                eulerRotation = eulerAngles;
                targetEulerRotation = eulerRotation;
                transform.rotation = Quaternion.Euler(eulerRotation);
                break;
            case Mode.Animated:
                targetEulerRotation = eulerAngles;
                break;
        }
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
            float distanceMultiplier = Mathf.Max((transform.position - targetPosition).magnitude, 0.25f);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime * distanceMultiplier);
        }

        if(transform.localScale != targetScale)
        {
            float distanceMultiplier = Mathf.Max((transform.localScale - targetScale).magnitude, 0.25f);
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleSpeed * Time.deltaTime * distanceMultiplier);
        }

        if(eulerRotation != targetEulerRotation)
        {
            float distanceMultiplier = Mathf.Max((targetEulerRotation - eulerRotation).magnitude, 0.25f);
            eulerRotation = Vector3.MoveTowards(eulerRotation, targetEulerRotation, rotationSpeed * Time.deltaTime * distanceMultiplier);
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }
    private enum Mode { Instant, Animated }
}
