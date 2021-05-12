using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTransparencyController : MonoBehaviour
{
    [SerializeField] private SpriteRendererAlphaTransformer alphaTransformer = default;

    public void SetAlpha(float alpha)
    {
        alphaTransformer.SetTargetValue(alpha);
    }

    public void SetAlphaInstant(float alpha)
    {
        alphaTransformer.SetTargetValueAndApplyInstantly(alpha);
    }

    public void AddAlpha(float alpha)
    {
        alphaTransformer.AddToTargetValue(alpha);
    }

    public void AddalphaInstant(float alpha)
    {
        alphaTransformer.AddToTargetValueAndApplyInstantly(alpha);
    }

    private void Awake()
    {
        alphaTransformer.SetTargetSpriteRenderer(GetComponent<SpriteRenderer>());
    }

    private void Update()
    {
        alphaTransformer.Update();
    }
}
