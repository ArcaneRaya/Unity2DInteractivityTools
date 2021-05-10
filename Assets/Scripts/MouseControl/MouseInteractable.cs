using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class MouseInteractable : MonoBehaviour
{
    public UnityEvent OnMouseClick = default;
    public UnityEvent OnMouseRelease = default;

    public Vector3 MoveDelta { get; private set; }
    public Vector3 Position { get; private set; }

    private bool isMouseDown = false;

    private void Awake()
    {
        Position = GetMouseWorldPosition();
    }

    private void Update()
    {
        Vector3 newMousePosition = GetMouseWorldPosition();

        MoveDelta = newMousePosition - Position;

        Position = newMousePosition;
    }
    private void OnMouseDown()
    {
        OnMouseClick.Invoke();
    }

    private void OnMouseUp()
    {
        OnMouseRelease.Invoke();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        return worldPoint;
    }
}
