using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseInteractable))]
[RequireComponent(typeof(ObjectTransformer))]
public class MouseScaler : MonoBehaviour
{
    [SerializeField] private bool alwaysScale = false;

    private MouseInteractable mouseInteractable;
    private ObjectTransformer transformer;
    private bool isScaling = false;

    public void StartScaling()
    {
        isScaling = true;
    }

    public void StopScaling()
    {
        isScaling = false;
    }

    private void Awake()
    {
        mouseInteractable = GetComponent<MouseInteractable>();
        transformer = GetComponent<ObjectTransformer>();
    }

    private void LateUpdate()
    {
        if (isScaling || alwaysScale)
        {
            Vector3 myNeutralPosition = transform.position;
            myNeutralPosition.z = 0;

            float distanceToMouse = Vector3.Distance(myNeutralPosition, mouseInteractable.Position);

            transformer.SetScale(new Vector3(distanceToMouse, distanceToMouse) * 2);
        }
    }
}
