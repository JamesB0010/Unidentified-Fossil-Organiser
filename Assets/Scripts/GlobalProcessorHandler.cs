using System;
using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public class GlobalProcessorHandler : MonoBehaviour
{

    public static GlobalProcessorHandler reference = null;
    private List<GenericLerpProcessor> processors = new List<GenericLerpProcessor>();

    private void Start()
    {
        if (reference == null)
        {
            reference = this;
            return;
        }
        Destroy(this.gameObject);
    }

    public void AddPackage<packageCustomComponent>(ObjectLerpPackage<packageCustomComponent> pkg)
    {
        for (int i = 0; i < processors.Count; i++)
        {
            if (processors[i] is LerpPackageProcessor<packageCustomComponent>)
            {
                ((LerpPackageProcessor<packageCustomComponent>)this.processors[i]).AddPackage(pkg);
                return;
            }
        }
        
        processors.Add(new LerpPackageProcessor<packageCustomComponent>());
        ((LerpPackageProcessor<packageCustomComponent>)processors[processors.Count - 1]).AddPackage(pkg);
    }
    void Update()
    {
        for(int i = 0; i < processors.Count; i++)
        {
            this.processors[i].ProcessLerpPackageList();
        }
    }
}
