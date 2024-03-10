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
        if(pkg is FloatLerpPackage)
            reference.AddPkgToProcessor((FloatLerpPackage)pkg);
        else if(pkg is Vector3LerpPackage)
            reference.AddPkgToProcessor((Vector3LerpPackage)pkg);
    }

    private LerpPackageProcessor floatlerpProcessors = new LerpPackageProcessor();
    private LerpPackageProcessor vector3LerpProcessors = new LerpPackageProcessor();
    
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
        this.floatlerpProcessors.AddPackage(pkg);
    }
    
    private void AddPkgToProcessor(Vector3LerpPackage pkg)
    {
        this.floatlerpProcessors.AddPackage(pkg);
    }
    void Update()
    {
        this.floatlerpProcessors.Update();
        this.vector3LerpProcessors.Update();
    }
}
