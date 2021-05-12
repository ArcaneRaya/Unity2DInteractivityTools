using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ValueClamp<T> : MonoBehaviour
{
    [SerializeField] protected T minLimit = default;
    [SerializeField] protected T maxLimit = default;

    public abstract T ClampValue(T value);
}
