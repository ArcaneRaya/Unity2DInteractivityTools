using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseInteractable))]
[RequireComponent(typeof(ObjectTransformer))]
public class MouseMover : MonoBehaviour
{
    private enum MouseFollowMode
    {
        Relative,
        Absolute
    }

    [SerializeField] private MouseFollowMode followMode = MouseMover.MouseFollowMode.Absolute;
    [SerializeField] private bool alwaysFollow = false;

    private MouseInteractable mouseInteractable;
    private ObjectTransformer transformer;
    private bool isFollowing = false;

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }

    private void Awake()
    {
        mouseInteractable = GetComponent<MouseInteractable>();
        transformer = GetComponent<ObjectTransformer>();
    }

    private void LateUpdate()
    {
        if (isFollowing || alwaysFollow)
        {
            if (followMode == MouseFollowMode.Absolute)
            {
                transformer.SetPosition(new Vector3(mouseInteractable.Position.x, mouseInteractable.Position.y, transform.position.z));
            } else
            {
                transformer.SetPosition(transform.position + mouseInteractable.MoveDelta);
            }
        }
    }
}
