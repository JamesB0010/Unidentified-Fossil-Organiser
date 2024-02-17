using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ObjectLerpPackage <CustomComponent>
{

    public ObjectLerpPackage(GameObject objectToLerp, Vector3 startPosition, Vector3 startRotation,
        Vector3 targetPosition, Vector3 targetRotation)
    {
        this.objectToLerp = objectToLerp;
        this.startPosition = startPosition;
        this.startRotation = startRotation;
        this.targetPosition = targetPosition;
        this.targetRotation = targetRotation;
        this.rb = this.objectToLerp.GetComponent<Rigidbody>();
        this.customComponent = this.objectToLerp.GetComponent<CustomComponent>();
    }
    public GameObject objectToLerp;
    public Vector3 startPosition;
    public Vector3 startRotation;

    public Vector3 targetPosition;
    public Vector3 targetRotation;

    public Rigidbody rb;
    public CustomComponent customComponent;

    public float current = 0.0f;
}
