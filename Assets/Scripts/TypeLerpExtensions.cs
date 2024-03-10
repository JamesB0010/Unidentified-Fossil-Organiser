using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UnityEngine;

public static class TypeLerpExtensions
{
    public static void LerpTo(this float value, float endValue, float timeToTake, ObjectLerpPackage<GameObject>.FloatLerpStep updateCallback = null, LerpPackageProcessor<GameObject>.PackageProcessed finishedCb = null)
    {
        GameObject obj = GlobalProcessorHandler.reference.gameObject;

        if (updateCallback == null)
        {
            updateCallback = (val, obj) =>
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
            new LerpData.FloatLerpPackage<GameObject>(
                value,
                endValue,
                updateCallback,
                finishedCb,
                obj,
                timeToTake
                )
            );
    }

    public static void LerpTo(this Vector3 value, Vector3 endValue, float timeToTake,
        ObjectLerpPackage<GameObject>.Vector3LerpStep updateCallback = null,
        LerpPackageProcessor<GameObject>.PackageProcessed finishedCb = null)
    {
        GameObject obj = GlobalProcessorHandler.reference.gameObject;

        if (updateCallback == null)
        {
            updateCallback = (val, obj) =>
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
            new Vector3LerpPackage<GameObject>(
                value,
            endValue,
            updateCallback,
            finishedCb,
            obj,
            timeToTake
           ));
    }
}