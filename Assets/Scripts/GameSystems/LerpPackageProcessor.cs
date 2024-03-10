using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public abstract class GenericLerpProcessor
{
    public abstract void Update();
}

public class LerpPackageProcessor : GenericLerpProcessor
{
    #region Attributes
    //define a callback type for when a package has been processed
    public delegate void PackageProcessed(LerpPackage pkg);
    
    //queue data
    private List<LerpPackage> packageList = new List<LerpPackage>();
    
    //lerping data
    private const float moveTowardsTarget = 1.0f;
    
    #endregion

    #region Methods
    public void AddPackage(LerpPackage newPackage)
    {
        this.packageList.Add(newPackage);
    }

    public override void Update()
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
        updateCurrentLerpPercentage(pkg);
        
        pkg.RunStepCallback();
    }

    private void updateCurrentLerpPercentage(LerpData.LerpPackage pkg)
    {
        pkg.elapsedTime += Time.deltaTime;
        
        pkg.current = pkg.animCurve.Evaluate(pkg.elapsedTime / pkg.timeToLerp);

        pkg.current = Mathf.Clamp01(pkg.current);
    }
    #endregion
}
