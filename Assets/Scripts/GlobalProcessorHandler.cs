using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UnityEngine;

//gpt refactored list to dictionary

public class GlobalProcessorHandler : MonoBehaviour
{

    private static GlobalProcessorHandler reference = null;

    public static AnimationCurve linearCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private LerpPackageProcessor lerpProcessor = new LerpPackageProcessor();
    public static void AddLerpPackage(LerpPackage pkg)
    {
        try
        {
            pkg.AddToProcessor(ref reference.lerpProcessor);
        }
        catch (NullReferenceException err)
        {
            GameObject obj = new GameObject("Global Lerp Processor Handler");
            GlobalProcessorHandler component = obj.AddComponent<GlobalProcessorHandler>();
            reference = component;
            pkg.AddToProcessor(ref reference.lerpProcessor);
        }
    }

    
    private void Awake()
    {
        if (reference == null)
        {
            reference = this;
            return;
        }
        Destroy(this.gameObject);
    }
    
    void Update()
    {
        this.lerpProcessor.Update();
    }
}
