using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public class LerpPackageProcessor
{
    #region Attributes
    //define a callback type for when a package has been processed
    public delegate void PackageProcessed(LerpPackage pkg);
    
    //queue data
    private List<LerpPackage> packageList = new List<LerpPackage>();
    
    #endregion

    #region Methods
    public void AddPackage(LerpPackage newPackage)
    {
        this.packageList.Add(newPackage);
    }

    public void Update()
    {
        for (int i = this.packageList.Count - 1; i >= 0; i--)
        {
            this.ProcessPackage(this.packageList[i], i);
        }
    }


    private void ProcessPackage(LerpPackage pkg, int i)
    {
        LerpValue(pkg);
        RemovePackageAtIndexIfCompleted(pkg, i);
    }
    
    private void RemovePackageAtIndexIfCompleted(LerpPackage pkg, int i)
    {
        if (pkg.current == 1)
        {
            this.packageList.RemoveAt(i);
            pkg.finalCallback(pkg);
        }
    }
    private void LerpValue(LerpPackage pkg)
    {
        UpdateCurrentLerpPercentage(pkg);
        
        pkg.RunStepCallback();
    }

    private void UpdateCurrentLerpPercentage(LerpPackage pkg)
    {
        pkg.elapsedTime += Time.deltaTime;
        
        pkg.current = pkg.animCurve.Evaluate(pkg.elapsedTime / pkg.timeToLerp);

        pkg.current = Mathf.Clamp01(pkg.current);
    }
    #endregion
}
