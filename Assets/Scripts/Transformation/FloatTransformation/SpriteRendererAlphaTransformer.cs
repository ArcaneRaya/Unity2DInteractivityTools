using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteRendererAlphaTransformer : FloatTransformer
{
    protected override float transformTarget
    {
        get {
            if (spriteRenderer == null)
            {
                throw new System.NullReferenceException("Sprite renderer not set for Sprite Renderer Alpha Transformer");
            }
            return spriteRenderer.color.a;
        }
        set {
            if (spriteRenderer == null)
            {
                throw new System.NullReferenceException("Sprite renderer not set for Sprite Renderer Alpha Transformer");
            }
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
