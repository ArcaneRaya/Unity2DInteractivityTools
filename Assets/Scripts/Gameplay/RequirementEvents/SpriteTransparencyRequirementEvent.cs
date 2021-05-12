using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTransparencyRequirementEvent : FloatRequirementReachedEvent
{
    protected override float CheckValue
    {
        get {
            return spriteRenderer.color.a;
        }
    }
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
