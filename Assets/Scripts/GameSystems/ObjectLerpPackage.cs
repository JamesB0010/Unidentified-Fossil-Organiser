using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ObjectLerpPackage <CustomComponent>
{
    #region ClassDataTypes
    public struct PositionRotationPair
    {
        public Vector3 position;
        public Vector3 rotation;
    }
    #endregion

    #region Attributes
    
    public GameObject objectToLerp;

    public PositionRotationPair start;
    public PositionRotationPair target;
    
    public Rigidbody rb;
    
    public CustomComponent customComponent;

    public float current = 0.0f;
    
    #endregion

    #region Methods
    public ObjectLerpPackage(GameObject objectToLerp, PositionRotationPair start,
        PositionRotationPair target)
    {
        this.objectToLerp = objectToLerp;
        this.start = start;
        this.target = target;
        this.rb = this.objectToLerp.GetComponent<Rigidbody>();
        this.customComponent = this.objectToLerp.GetComponent<CustomComponent>();
    }
    #endregion
}
