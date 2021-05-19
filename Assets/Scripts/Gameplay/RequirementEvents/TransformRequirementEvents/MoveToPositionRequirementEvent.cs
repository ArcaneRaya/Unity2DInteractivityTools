using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionRequirementEvent : VectorRequirementReachedEvent
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
            return targetPosition;
        }
    }

    [SerializeField] private ObjectTransformer objectToCheck = default;
    [SerializeField] private Vector3 targetPosition = default;
}
