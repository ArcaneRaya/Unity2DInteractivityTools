using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectTransformer))]
public class CameraController : MonoBehaviour
{
    private ObjectTransformer transformer = default;

    private void Awake()
    {
        transformer = GetComponent<ObjectTransformer>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 eulerRotation = new Vector3();
            Vector2 normalizedMouseMovement = GetNormalizedMouseMovement();
            eulerRotation.x = normalizedMouseMovement.y * 90;
            eulerRotation.y = normalizedMouseMovement.x * 90;
            transformer.SetRotation(eulerRotation);
        }
        if (Input.GetMouseButtonUp(0))
        {
            transformer.SetRotation(Vector3.zero);
        }
    }

    private Vector2 GetNormalizedMouseMovement()
    {
        Vector2 normalizedMouseMovement = new Vector2();
        normalizedMouseMovement.x = Input.mousePosition.x / Screen.width - 0.5f;
        normalizedMouseMovement.y = Input.mousePosition.y / Screen.height - 0.5f;
        return normalizedMouseMovement;
    }
}
