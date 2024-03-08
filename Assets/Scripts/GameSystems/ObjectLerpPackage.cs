using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace LerpData
{

    abstract class ObjectLerpPackage<CustomComponent>
    {
        public GameObject objectToLerp;
        public CustomComponent customComponent;
        public float lerpSpeed;
        public Rigidbody rb = null;
        public float current = 0.0f;
        
        public abstract object start { get; set; }
        public abstract object target { get; set; }
    }
    
class FloatLerpPackage<CustomComponent>: ObjectLerpPackage<CustomComponent>
{
    #region attributes

    private float _start;
    private float _target;

    public override object start
    {
        get => this._start;
        set => this._start = (float)value;
    }

    public override object target
    {
        get => this._target;
        set => this._target = (float)value;
    }
    #endregion
    
    #region Methods
    public FloatLerpPackage(float start,
        float target, float lerpSpeed = 1.0f, GameObject objectToLerp = null)
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

class Vector3LerpPackage<CustomComponent>: ObjectLerpPackage<CustomComponent>
{
    #region attributes

    private Vector3 _start;
    private Vector3 _target;

    public override object start
    {
        get => this._start;
        set => this._start = (Vector3)value;
    }

    public override object target
    {
        get => this._target;
        set => this._target = (Vector3)value;
    }
    #endregion

    Vector3LerpPackage(Vector3 start, Vector3 target, float lerpSpeed = 1.0f, GameObject objectToLerp = null)
    {
        this.objectToLerp = objectToLerp;
        this.start = start;
        this.target = target;
        this.rb = this.objectToLerp.GetComponent<Rigidbody>();
        this.customComponent = this.objectToLerp.GetComponent<CustomComponent>();
        this.lerpSpeed = lerpSpeed;
        this.callback = callback;
    }
}
}
