using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UnityEngine;

public static class TypeLerpExtensions
{
    public static void LerpTo(this float value, float endValue, float timeToTake,
        LerpPackage.FloatLerpStep updateCallback = null, LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        if (updateCallback == null)
        {
            updateCallback = (val) =>
            {
                value = val;
                Debug.Log(value);
            };
        }

        if (finishedCb == null)
        {
            finishedCb = pkg =>
            {
                Debug.Log("finished Lerping: " + value);
            };
        }

        if (animCurve == null)
        {
            AnimationCurve.Linear(0, 0, 1, 1);
        }
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

    public static void LerpTo(this Vector3 value, Vector3 endValue, float timeToTake,
        LerpPackage.Vector3LerpStep updateCallback = null,
        LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        if (updateCallback == null)
        {
            updateCallback = (val) =>
            {
                value = val;
                Debug.Log(value);
            };
        }

        if (finishedCb == null)
        {
            finishedCb = pkg =>
            {
                Debug.Log("finished Lerping: " + value);
            };
        }
        
        if (animCurve == null)
        {
            AnimationCurve.Linear(0, 0, 1, 1);
        }
        
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
    
    public static void SlerpTo(this Vector3 value, Vector3 endValue, float timeToTake,
        LerpPackage.Vector3LerpStep updateCallback = null,
        LerpPackageProcessor.PackageProcessed finishedCb = null,
        AnimationCurve animCurve = null)
    {
        if (updateCallback == null)
        {
            updateCallback = (val) =>
            {
                value = val;
                Debug.Log(value);
            };
        }

        if (finishedCb == null)
        {
            finishedCb = pkg =>
            {
                Debug.Log("finished Lerping: " + value);
            };
        }
        
        if (animCurve == null)
        {
            AnimationCurve.Linear(0, 0, 1, 1);
        }
        
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
}