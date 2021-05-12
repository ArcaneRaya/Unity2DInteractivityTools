using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteRendererAlphaTransformer : FloatTransformer
{
    protected override float transformTarget
    {
        get {
            return spriteRenderer.color.a;
        }
        set {
            var color = spriteRenderer.color;
            color.a = value;
            spriteRenderer.color = color;
        }
    }

    private SpriteRenderer spriteRenderer;

    public void SetTargetSpriteRenderer(SpriteRenderer renderer)
    {
        spriteRenderer = renderer;
        SetTargetValueAndApplyInstantly(transformTarget);
    }
}
