using System;
using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

//gpt refactored list to dictionary

public class GlobalProcessorHandler : MonoBehaviour
{

    public static GlobalProcessorHandler reference = null;

    private Dictionary<System.Type, GenericLerpProcessor> lerpProcessors =
        new Dictionary<System.Type, GenericLerpProcessor>();

    private void Start()
    {
        if (reference == null)
        {
            reference = this;
            return;
        }
        Destroy(this.gameObject);
    }

    public void AddLerpPackage<packageCustomComponent>(ObjectLerpPackage<packageCustomComponent> pkg)
    {
        System.Type packageType = typeof(packageCustomComponent);
        if(this.lerpProcessors.ContainsKey(packageType))
            ((LerpPackageProcessor<packageCustomComponent>)this.lerpProcessors[packageType]).AddPackage(pkg);
        else
        {
            var newProcessor = new LerpPackageProcessor<packageCustomComponent>();
            newProcessor.AddPackage(pkg);
            this.lerpProcessors.Add(packageType, newProcessor);
        }
    }
    void Update()
    {
        foreach (var processor in this.lerpProcessors.Values)
        {
            processor.Update();
        }
    }
}
