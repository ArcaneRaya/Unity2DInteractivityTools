using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObjectRequirementEvent : VectorRequirementReachedEvent
{
    protected override Vector3 CheckValue
    {
        get {
            return objectToCheck.transform.position;
        }
    }

    protected override Vector3 TargetValue
    {
        get {
            return targetObject.transform.position;
        }
    }

    [SerializeField] private ObjectTransformer objectToCheck = default;
    [SerializeField] private GameObject targetObject = default;
    [SerializeField] private bool allowWhileObjectIsHeld = false;
    private MouseInteractable mouseInteractable = null;

    private void Awake()
    {
        if (allowWhileObjectIsHeld == false)
        {
            mouseInteractable = GetComponent<MouseInteractable>();
            if(mouseInteractable == null)
            {
                Debug.LogError("Could not find mouse interactable, will always allow!");
            }
        }
    }

    protected override bool HasRequirementBeenReached()
    {
        bool mouseRequirementReached = allowWhileObjectIsHeld || mouseInteractable == null ? true : mouseInteractable.IsMouseDown == false;
        return mouseRequirementReached && base.HasRequirementBeenReached();
    }
}
