using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UnityEngine;

public static class TypeLerpExtensions
{
    public static void LerpTo(this float value, float endValue, float timeToTake = 1,
       LerpStep<float> updateCallback = null, LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        updateCallback ??= (val) => { value = val; Debug.Log(value); };

        finishedCb ??= pkg => { Debug.Log("finished Lerping: " + value); };

        animCurve??= AnimationCurve.Linear(0, 0, 1, 1);

        GlobalProcessorHandler.AddLerpPackage(
            new LerpData.FloatLerpPackage(
                value,
                endValue,
                updateCallback,
                finishedCb,
                timeToTake,
                animCurve
                )
            );
    }

    public static void LerpTo(this Vector3 value, Vector3 endValue, float timeToTake = 1,
        LerpStep<Vector3> updateCallback = null,
        LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        updateCallback ??= (val) => { value = val; Debug.Log(value); };

        finishedCb ??= pkg => { Debug.Log("finished Lerping: " + value); };
        
        animCurve ??= AnimationCurve.Linear(0, 0, 1, 1);
        
        GlobalProcessorHandler.AddLerpPackage(
            new Vector3LerpPackage(
                value,
            endValue,
            updateCallback,
            finishedCb,
            timeToTake,
                animCurve
           ));
    }
    
    public static void SlerpTo(this Vector3 value, Vector3 endValue, float timeToTake = 1,
        LerpStep<Vector3> updateCallback = null,
        LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        updateCallback ??= (val) => { value = val; Debug.Log(value); };

        finishedCb ??= pkg => { Debug.Log("finished Lerping: " + value); };
        
        animCurve ??= AnimationCurve.Linear(0, 0, 1, 1);
        
        GlobalProcessorHandler.AddLerpPackage(
            new Vector3SlerpPackage(
                value,
                endValue,
                updateCallback,
                finishedCb,
                timeToTake,
                animCurve
            ));
    }
    
    public static void LerpTo(this Vector4 value, Vector4 endValue, float timeToTake = 1,
        LerpStep<Vector4> updateCallback = null,
        LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        updateCallback ??= (val) => { value = val; Debug.Log(value); };

        finishedCb ??= pkg => { Debug.Log("finished Lerping: " + value); };
        
        animCurve ??= AnimationCurve.Linear(0, 0, 1, 1);
        
        GlobalProcessorHandler.AddLerpPackage(
            new Vector4LerpPackage(
                value,
                endValue,
                updateCallback,
                finishedCb,
                timeToTake,
                animCurve
            ));
    }
}