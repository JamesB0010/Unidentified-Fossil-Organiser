using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UnityEngine;

public static class TypeLerpExtensions
{
    public static void LerpTo(this float value, float endValue, float timeToTake, ObjectLerpPackage.FloatLerpStep updateCallback = null, LerpPackageProcessor.PackageProcessed finishedCb = null)
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
        GlobalProcessorHandler.AddLerpPackage(
            new LerpData.FloatLerpPackage(
                value,
                endValue,
                updateCallback,
                finishedCb,
                timeToTake
                )
            );
    }

    public static void LerpTo(this Vector3 value, Vector3 endValue, float timeToTake,
        ObjectLerpPackage.Vector3LerpStep updateCallback = null,
        LerpPackageProcessor.PackageProcessed finishedCb = null)
    {
        GameObject obj = GlobalProcessorHandler.reference.gameObject;

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
        
        GlobalProcessorHandler.AddLerpPackage(
            new Vector3LerpPackage(
                value,
            endValue,
            updateCallback,
            finishedCb,
            timeToTake
           ));
    }
}