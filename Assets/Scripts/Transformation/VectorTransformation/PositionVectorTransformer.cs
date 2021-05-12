using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionVectorTransformer : VectorTransformer
{
    protected override Vector3 transformTarget
    {
        get {
            if (linkedTransform == null)
            {
                throw new System.NullReferenceException("No transform set for Position Vector Transformer.");
            }
            return linkedTransform.position;
        }
        set {
            if (linkedTransform == null)
            {
                return;
            }
            linkedTransform.position = value;
        }
    }

    private Transform linkedTransform = null;

    public void SetLinkedTransform(Transform transform)
    {
        linkedTransform = transform;
        SetTargetValueAndApplyInstantly(transformTarget);
    }
}
