using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UnityEngine;

//gpt refactored list to dictionary

public class GlobalProcessorHandler : MonoBehaviour
{

    public static GlobalProcessorHandler reference = null;

    public static void AddLerpPackage(ObjectLerpPackage pkg)
    {
        try
        {
            pkg.AddToProcessor(ref reference.lerpProcessors);
        }
        catch (NullReferenceException err)
        {
            GameObject obj = new GameObject("Global Lerp Processor Handler");
            GlobalProcessorHandler component = obj.AddComponent<GlobalProcessorHandler>();
            reference = component;
            pkg.AddToProcessor(ref reference.lerpProcessors);
        }
    }

    private LerpPackageProcessor lerpProcessors = new LerpPackageProcessor();
    
    private void Awake()
    {
        if (reference == null)
        {
            reference = this;
            return;
        }
        Destroy(this.gameObject);
    }

    private void AddPkgToProcessor(FloatLerpPackage pkg)
    {
        this.lerpProcessors.AddPackage(pkg);
    }
    
    private void AddPkgToProcessor(Vector3LerpPackage pkg)
    {
        this.lerpProcessors.AddPackage(pkg);
    }
    void Update()
    {
        this.lerpProcessors.Update();
    }
}
