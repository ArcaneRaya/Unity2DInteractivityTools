using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseInteractable))]
[RequireComponent(typeof(ObjectTransformer))]
public class MouseRotator : MonoBehaviour
{
    [SerializeField] private bool alwaysRotate = false;

    private MouseInteractable mouseInteractable;
    private ObjectTransformer transformer;
    private Vector3 previousMouseDirection = Vector3.zero;
    private bool isRotating = false;

    public void StartRotating()
    {
        previousMouseDirection = mouseInteractable.Position - transform.position;
        isRotating = true;
    }

    public void StopRotating()
    {
        isRotating = false;
    }

    private void Awake()
    {
        mouseInteractable = GetComponent<MouseInteractable>();
        transformer = GetComponent<ObjectTransformer>();
        previousMouseDirection = mouseInteractable.Position - transform.position;
    }

    private void LateUpdate()
    {
        if (isRotating || alwaysRotate)
        {
            Vector3 mouseDirection = mouseInteractable.Position - transform.position;
            float angle = Vector3.SignedAngle(previousMouseDirection, mouseDirection, transform.forward);
            Vector3 targetRotation = transformer.eulerRotation + new Vector3(0, 0, angle);
            transformer.SetRotation(targetRotation);
            previousMouseDirection = mouseDirection;
        }
    }
}
