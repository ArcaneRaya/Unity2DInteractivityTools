using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectTransformer))]
public class CameraController : MonoBehaviour
{
    private ObjectTransformer transformer = default;
    private Vector3 previousNormalizedMousePosition;
    private Gyroscope gyroscope = default;

    private void Awake()
    {
        transformer = GetComponent<ObjectTransformer>();
        gyroscope = Input.gyro;
        gyroscope.enabled = true;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    previousNormalizedMousePosition = GetNormalizedMousePosition();
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 eulerRotation = new Vector3();
        //    Vector3 normalizedMousePosition = GetNormalizedMousePosition();
        //    Vector3 delta = normalizedMousePosition - previousNormalizedMousePosition;
        //    eulerRotation.x = delta.y * 90;
        //    eulerRotation.y = delta.x * 90;
        //    transformer.AddRotation(eulerRotation);
        //    previousNormalizedMousePosition = normalizedMousePosition;
        //}

        
        Vector3 eulerRotation = new Vector3();
        eulerRotation.x = gyroscope.rotationRate.x;
        eulerRotation.y = gyroscope.rotationRate.y;
        transformer.AddRotation(eulerRotation);
    }

    private Vector2 GetNormalizedMousePosition()
    {
        Vector2 normalizedMouseMovement = new Vector2();
        normalizedMouseMovement.x = Input.mousePosition.x / Screen.width;
        normalizedMouseMovement.y = Input.mousePosition.y / Screen.height;
        return normalizedMouseMovement;
    }
}
