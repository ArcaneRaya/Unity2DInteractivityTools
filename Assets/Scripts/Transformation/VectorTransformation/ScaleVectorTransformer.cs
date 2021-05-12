using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScaleVectorTransformer : VectorTransformer
{
    protected override Vector3 transformTarget
    {
        get {
            if (linkedTransform == null)
            {
                throw new System.NullReferenceException("No transform set for Position Vector Transformer.");
            }
            return linkedTransform.localScale;
        }
        set {
            if (linkedTransform == null)
            {
                return;
            }
            linkedTransform.localScale = value;
        }
    }

    private Transform linkedTransform = null;

    public void SetLinkedTransform(Transform transform)
    {
        linkedTransform = transform;
        SetTargetValueAndApplyInstantly(transformTarget);
    }
}
