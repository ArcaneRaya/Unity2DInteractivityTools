using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotationVectorTransformer : VectorTransformer
{
    protected override Vector3 transformTarget
    {
        get {
            if (linkedTransform == null)
            {
                throw new System.NullReferenceException("No transform set for Position Vector Transformer.");
            }
            return eulerAngles;
        }
        set {
            if (linkedTransform == null)
            {
                return;
            }
            eulerAngles = value;
            linkedTransform.rotation = Quaternion.Euler(eulerAngles);
        }
    }

    private Transform linkedTransform = null;
    private Vector3 eulerAngles;

    public void SetLinkedTransform(Transform transform)
    {
        linkedTransform = transform;
        eulerAngles = transform.eulerAngles;
        SetTargetValueAndApplyInstantly(transformTarget);
    }
}