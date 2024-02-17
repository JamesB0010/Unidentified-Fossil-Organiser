using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ObjectLerpPackage <CustomComponent>
{
    public struct PositionRotationPair
    {
        public Vector3 position;
        public Vector3 rotation;
    }
    public ObjectLerpPackage(GameObject objectToLerp, PositionRotationPair start,
        PositionRotationPair end)
    {
        this.objectToLerp = objectToLerp;
        this.startPosition = start.position;
        this.startRotation = start.rotation;
        this.targetPosition = end.position;
        this.targetRotation = end.rotation;
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
