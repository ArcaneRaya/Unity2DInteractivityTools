using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectTransformer))]
public class GyroscopeRotator : MonoBehaviour
{
    [SerializeField] private ObjectTransformer target = default;
    [SerializeField] private bool invertRotation = true;
    private Vector3 previousNormalizedMousePosition;
    private Gyroscope gyroscope = default;

    private void Awake()
    {
        gyroscope = Input.gyro;
        gyroscope.enabled = true;
    }

    private void Update()
    {
        Vector3 eulerRotation = new Vector3();

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            previousNormalizedMousePosition = GetNormalizedMousePosition();
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 normalizedMousePosition = GetNormalizedMousePosition();
            Vector3 delta = normalizedMousePosition - previousNormalizedMousePosition;
            eulerRotation.x = delta.y * -90;
            eulerRotation.y = delta.x * 90;
            target.AddRotation(eulerRotation);
            previousNormalizedMousePosition = normalizedMousePosition;
        }
#else
        eulerRotation.x = gyroscope.rotationRate.x;
        eulerRotation.y = gyroscope.rotationRate.y;
        if (invertRotation){
            eulerRotation *= -1;
        }
        target.AddRotation(eulerRotation);
#endif
    }

    private Vector2 GetNormalizedMousePosition()
    {
        Vector2 normalizedMouseMovement = new Vector2();
        normalizedMouseMovement.x = Input.mousePosition.x / Screen.width;
        normalizedMouseMovement.y = Input.mousePosition.y / Screen.height;
        return normalizedMouseMovement;
    }
}
