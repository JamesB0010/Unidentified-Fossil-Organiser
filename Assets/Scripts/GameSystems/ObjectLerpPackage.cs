using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LerpData
{
    public struct PositionRotationPair
    {
        public Vector3 position;
        public Vector3 rotation;
    }
    
class ObjectLerpPackage <CustomComponent>
{

    #region Attributes
    
    public GameObject objectToLerp;

    public PositionRotationPair start;
    public PositionRotationPair target;
    
    public Rigidbody rb;
    
    public CustomComponent customComponent;

    public float current = 0.0f;

    public float lerpSpeed;
    
    #endregion

    #region Methods
    public ObjectLerpPackage(GameObject objectToLerp, PositionRotationPair start,
        PositionRotationPair target, float lerpSpeed = 1.0f)
    {
        this.objectToLerp = objectToLerp;
        this.start = start;
        this.target = target;
        this.rb = this.objectToLerp.GetComponent<Rigidbody>();
        this.customComponent = this.objectToLerp.GetComponent<CustomComponent>();
        this.lerpSpeed = lerpSpeed;
    }
    #endregion
}
}
