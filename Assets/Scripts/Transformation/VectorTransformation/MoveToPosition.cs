using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectTransformer))]
public class MoveToPosition : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition = default;
    [SerializeField] private bool moveOnStart = false;

    private ObjectTransformer transformer = null;

    private void Awake()
    {
        transformer = GetComponent<ObjectTransformer>();
    }

    private void Start()
    {
        if (moveOnStart)
        {
            Move();
        }
    }

    public void Move()
    {
        transformer.SetPosition(targetPosition);
    }
}
