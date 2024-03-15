using System.Collections;
using System.Collections.Generic;
using System.Threading;
using LerpData;
using UnityEngine;


namespace LerpData
{
        public delegate void LerpStep<T>(T currentValue);

    public abstract class LerpPackage
    {

        public LerpPackageProcessor.PackageProcessed finalCallback;
        public float timeToLerp;
        public float elapsedTime = 0.0f;
        public float current = 0.0f;
        public AnimationCurve animCurve;
        
        public abstract object start { get; set; }
        public abstract object target { get; set; }
        
        public void ResetTiming()
        {
            this.elapsedTime = 0.0f;
        }

        public abstract void AddToProcessor(ref LerpPackageProcessor processor);

        public abstract void RunStepCallback();
    }
    
class FloatLerpPackage: LerpPackage
{
    #region attributes

    public LerpStep<float> lerpStepCallback;

    private float _start;
    private float _target;

    public override object start
    {
        get => (float)this._start;
        set => this._start = (float)value;
    }

    public override object target
    {
        get => (float)this._target;
        set => this._target = (float)value;
    }
    #endregion
    
    #region Methods
    public FloatLerpPackage(float start,
        float target, LerpStep<float> stepCallback, LerpPackageProcessor.PackageProcessed finalCb, float timeToLerp = 1.0f, AnimationCurve animCurve = null)
    {
        this.start = start;
        this.target = target;
        this.timeToLerp = timeToLerp;
        this.lerpStepCallback = stepCallback;
        this.finalCallback = finalCb;
        this.animCurve = animCurve != null ? animCurve : AnimationCurve.Linear(0, 0, 1, 1);
    }

    public override void AddToProcessor(ref LerpPackageProcessor processor)
    {
        processor.AddPackage(this);
    }

    public override void RunStepCallback()
    {
        this.lerpStepCallback(Mathf.Lerp(this._start,  this._target, this.current));
    }

    #endregion
}

class Vector3LerpPackage: LerpPackage
{
    #region attributes
    
    public LerpStep<Vector3> lerpStepCallback;

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

    public Vector3LerpPackage(Vector3 start, Vector3 target, LerpStep<Vector3> stepCallback, LerpPackageProcessor.PackageProcessed finalCb, float timeToLerp = 1.0f, AnimationCurve animCurve = null)
    {
        this.start = start;
        this.target = target;
        this.timeToLerp = timeToLerp;
        this.lerpStepCallback = stepCallback;
        this.finalCallback = finalCb;
        this.animCurve = animCurve != null ? animCurve : AnimationCurve.Linear(0, 0, 1, 1);
    }
    
    public override void AddToProcessor(ref LerpPackageProcessor processor)
    {
        processor.AddPackage(this);
    }
    
    public override void RunStepCallback()
    {
        this.lerpStepCallback(Vector3.Lerp(this._start,  this._target, this.current));
    }
}
}

class Vector3SlerpPackage : LerpPackage
{
    #region attributes
    
    public LerpStep<Vector3> lerpStepCallback;

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

    public Vector3SlerpPackage(Vector3 start, Vector3 target, LerpStep<Vector3> stepCallback, LerpPackageProcessor.PackageProcessed finalCb, float timeToLerp = 1.0f, AnimationCurve animCurve = null)
    {
        this.start = start;
        this.target = target;
        this.timeToLerp = timeToLerp;
        this.lerpStepCallback = stepCallback;
        this.finalCallback = finalCb;
        this.animCurve = animCurve != null ? animCurve : AnimationCurve.Linear(0, 0, 1, 1);
    }
    
    public override void AddToProcessor(ref LerpPackageProcessor processor)
    {
        processor.AddPackage(this);
    }
    
    public override void RunStepCallback()
    {
        this.lerpStepCallback(Vector3.Slerp(this._start,  this._target, this.current));
    }
}

class Vector4LerpPackage: LerpPackage
{
    #region attributes
    
    public LerpStep<Vector4> lerpStepCallback;

    private Vector4 _start;
    private Vector4 _target;
    
    public override object start
    {
        get => this._start;
        set => this._start = (Vector4)value;
    }

    public override object target
    {
        get => this._target;
        set => this._target = (Vector4)value;
    }
    #endregion

    public Vector4LerpPackage(Vector4 start, Vector4 target, LerpStep<Vector4> stepCallback, LerpPackageProcessor.PackageProcessed finalCb, float timeToLerp = 1.0f, AnimationCurve animCurve = null)
    {
        this.start = start;
        this.target = target;
        this.timeToLerp = timeToLerp;
        this.lerpStepCallback = stepCallback;
        this.finalCallback = finalCb;
        this.animCurve = animCurve ??= AnimationCurve.Linear(0, 0, 1, 1);
    }
    
    public override void AddToProcessor(ref LerpPackageProcessor processor)
    {
        processor.AddPackage(this);
    }
    
    public override void RunStepCallback()
    {
        this.lerpStepCallback(Vector4.Lerp(this._start,  this._target, this.current));
    }
}