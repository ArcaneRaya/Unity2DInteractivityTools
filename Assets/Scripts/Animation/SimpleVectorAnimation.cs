using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class SimpleVectorAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Vector3 target = Vector3.zero;
    [SerializeField] private AnimationMode mode = AnimationMode.Repeating;
    [SerializeField] private Ease animationEase = Ease.InOutSine;
    [SerializeField] private float animationTime = 1;
    [SerializeField] private Ease animationEasePong = Ease.InOutSine;
    [SerializeField] private float animationTimePong = 1;

    [Header("Other")]
    [SerializeField] private bool alwaysAnimate = false;

    private Sequence currentTweenSequence = null;
    private Vector3 startValue = Vector3.zero;
    private Vector3 targetValue = Vector3.zero;

    private const int INFINITE_LOOP = -1;

    public void StartAnimating()
    {
        if (currentTweenSequence != null)
        {
            currentTweenSequence.Play();
        }
        else
        {
            CreateAnimationAndLinkCallbacks();
        }
    }

    public void PauseAnimating()
    {
        PauseAnimationTween();
    }

    public void StopAnimatingAndResetToStartState()
    {
        KillAnimationTween();
        HandleReset(startValue);
    }

    protected virtual void Awake()
    {
        startValue = GetStartValue();
    }

    protected virtual void Start()
    {
        if (alwaysAnimate)
        {
            CreateAnimationAndLinkCallbacks();
        }
    }

    protected virtual void OnDestroy()
    {
        currentTweenSequence.Kill();
    }

    protected abstract Vector3 GetStartValue();

    protected abstract void OnAnimationUpdate(Vector3 currentValue);

    protected abstract void HandleReset(Vector3 startValue);

    private void PauseAnimationTween()
    {
        if (currentTweenSequence != null)
        {
            currentTweenSequence.Pause();
        }
    }

    private void KillAnimationTween()
    {
        if (currentTweenSequence != null)
        {
            currentTweenSequence.Kill();
        }
    }

    private void CreateAnimationAndLinkCallbacks()
    {
        InitializeValues();
        AppendAnimationTowardsTargetvalue();

        if (ShouldHaveReturnAnimation())
        {
            AppendAnimationTowardsStartvalue();
        }

        if (ShouldLoop())
        {
            currentTweenSequence.SetLoops(INFINITE_LOOP);
        }

        currentTweenSequence.OnUpdate(() => OnAnimationUpdate(targetValue));
        currentTweenSequence.OnKill(OnTweenKilled);
    }

    private void InitializeValues()
    {
        targetValue = startValue;
        currentTweenSequence = DOTween.Sequence();
    }

    private void AppendAnimationTowardsTargetvalue()
    {
        Tween moveTween = DOTween.To(() => targetValue, x => targetValue = x, startValue + target, animationTime);
        moveTween.SetEase(animationEase);
        currentTweenSequence.Append(moveTween);
    }

    private void AppendAnimationTowardsStartvalue()
    {
        Tween returnTween = null;
        returnTween = DOTween.To(() => targetValue, x => targetValue = x, startValue, animationTimePong);
        returnTween.SetEase(animationEasePong);
        currentTweenSequence.Append(returnTween);
    }

    private bool ShouldHaveReturnAnimation()
    {
        switch (mode)
        {
            case AnimationMode.Single:
            case AnimationMode.Repeating:
                return false;
            case AnimationMode.PingPong:
            case AnimationMode.RepeatingPingPong:
                return true;
            default:
                return false;
        }
    }

    private bool ShouldLoop()
    {
        switch (mode)
        {
            case AnimationMode.Single:
            case AnimationMode.PingPong:
                return false;
            case AnimationMode.Repeating:
            case AnimationMode.RepeatingPingPong:
                return true;
            default:
                return false;
        }
    }

    private void OnTweenKilled()
    {
        currentTweenSequence = null;
    }

    private enum AnimationMode { Single, Repeating, PingPong, RepeatingPingPong }
}
