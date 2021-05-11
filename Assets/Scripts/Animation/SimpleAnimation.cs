using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class SimpleAnimation : MonoBehaviour
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

    public void StartAnimating()
    {
        if (currentTweenSequence != null)
        {
            currentTweenSequence.Play();
        }
        else
        {
            CreateAnimation();
        }
    }

    public void PauseAnimating()
    {
        PauseAnimation();
    }

    public void StopAnimating(bool resetPosition)
    {
        StopAnimation(resetPosition);
    }

    protected virtual void Awake()
    {
        startValue = GetStartValue();
    }

    protected virtual void Start()
    {
        if (alwaysAnimate)
        {
            CreateAnimation();
        }
    }

    protected virtual void OnDestroy()
    {
        currentTweenSequence.Kill();
    }

    protected abstract Vector3 GetStartValue();

    protected abstract void OnAnimationUpdate(Vector3 currentValue);

    protected abstract void HandleReset(Vector3 startValue);

    private void CreateAnimation()
    {
        targetValue = startValue;
        currentTweenSequence = DOTween.Sequence();
        Tween moveTween = DOTween.To(() => targetValue, x => targetValue = x, startValue + target, animationTime);
        moveTween.SetEase(animationEase);
        currentTweenSequence.Append(moveTween);
        Tween returnTween = null;

        switch (mode)
        {
            case AnimationMode.Single:
                break;
            case AnimationMode.PingPong:
                returnTween = DOTween.To(() => targetValue, x => targetValue = x, startValue, animationTimePong);
                returnTween.SetEase(animationEasePong);
                currentTweenSequence.Append(returnTween);
                break;
            case AnimationMode.Repeating:
                currentTweenSequence.SetLoops(-1);
                break;
            case AnimationMode.RepeatingPingPong:
                returnTween = DOTween.To(() => targetValue, x => targetValue = x, startValue, animationTimePong);
                returnTween.SetEase(animationEasePong);
                currentTweenSequence.Append(returnTween); currentTweenSequence.SetLoops(-1);
                break;
        }

        currentTweenSequence.OnUpdate(()=>OnAnimationUpdate(targetValue));
        currentTweenSequence.OnKill(OnTweenKilled);
    }

    private void OnTweenKilled()
    {
        currentTweenSequence = null;
    }

    private void PauseAnimation()
    {
        if (currentTweenSequence != null)
        {
            currentTweenSequence.Pause();
        }
    }

    private void StopAnimation(bool resetToStartValues)
    {
        if (currentTweenSequence != null)
        {
            currentTweenSequence.Kill();
        }

        if (resetToStartValues)
        {
            HandleReset(startValue);
        }
    }

    private enum AnimationMode { Single, Repeating, PingPong, RepeatingPingPong }
}
