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
    public bool IsMouseDown
    {
        get {
            return isMouseDown;
        }
    }

    private bool isMouseDown = false;
    private bool isInteractionAllowed = true;

    public void BlockInteraction()
    {
        isInteractionAllowed = false;
    }

    public void AllowInteraction()
    {
        isInteractionAllowed = true;
    }

    public void ForceRelease()
    {
        OnMouseUp();
    }

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
        if (isInteractionAllowed == false) { return; }

        isMouseDown = true;
        OnMouseClick.Invoke();
    }

    private void OnMouseUp()
    {
        if (isInteractionAllowed == false) { return; }

        if (isMouseDown)
        {
            isMouseDown = false;
            OnMouseRelease.Invoke();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        return worldPoint;
    }
}
