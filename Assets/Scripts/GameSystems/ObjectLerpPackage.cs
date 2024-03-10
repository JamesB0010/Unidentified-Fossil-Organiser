using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace LerpData
{

    public abstract class ObjectLerpPackage
    {
        public delegate void Vector3LerpStep(Vector3 currentValue);

        public delegate void FloatLerpStep(float currentValue);

        public LerpPackageProcessor.PackageProcessed finalCallback;
        public float timeToLerp;
        public float elapsedTime = 0.0f;
        public float current = 0.0f;
        public AnimationCurve animCurve;
        
        public abstract object start { get; set; }
        public abstract object target { get; set; }

        public virtual void RunStepCallback(float val)
        {
        }

        public virtual void RunStepCallback(Vector3 val)
        {
        }

        public void ResetTiming()
        {
            this.elapsedTime = 0.0f;
            this.current = 0.0f;
        }
    }
    
class FloatLerpPackage: ObjectLerpPackage
{
    #region attributes

    public FloatLerpStep lerpStepCallback;

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
        float target, FloatLerpStep stepCallback, LerpPackageProcessor.PackageProcessed finalCb, float timeToLerp = 1.0f, AnimationCurve animCurve = null)
    {
        this.start = start;
        this.target = target;
        this.timeToLerp = timeToLerp;
        this.lerpStepCallback = stepCallback;
        this.finalCallback = finalCb;
        this.animCurve = animCurve != null ? animCurve : AnimationCurve.Linear(0, 0, 1, 1);
    }

    public override void RunStepCallback(float value)
    {
        this.lerpStepCallback(value);
    }

    #endregion
}

class Vector3LerpPackage: ObjectLerpPackage
{
    #region attributes
    
    public Vector3LerpStep lerpStepCallback;

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

    public Vector3LerpPackage(Vector3 start, Vector3 target, Vector3LerpStep stepCallback, LerpPackageProcessor.PackageProcessed finalCb, float timeToLerp = 1.0f, AnimationCurve animCurve = null)
    {
        this.start = start;
        this.target = target;
        this.timeToLerp = timeToLerp;
        this.lerpStepCallback = stepCallback;
        this.finalCallback = finalCb;
        this.animCurve = animCurve != null ? animCurve : AnimationCurve.Linear(0, 0, 1, 1);
    }

    public override void RunStepCallback(Vector3 val)
    {
        this.lerpStepCallback(val);
    }
}
}
