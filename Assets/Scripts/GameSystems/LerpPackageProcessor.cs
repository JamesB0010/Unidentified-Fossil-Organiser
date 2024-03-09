using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public abstract class GenericLerpProcessor
{
    public abstract void Update();
}

public class LerpPackageProcessor <CustomComponent>: GenericLerpProcessor
{
    #region Attributes
    //define a callback type for when a package has been processed
    public delegate void PackageProcessed(ObjectLerpPackage<CustomComponent> pkg);
    
    //queue data
    private List<ObjectLerpPackage<CustomComponent>> packageList = new List<ObjectLerpPackage<CustomComponent>>();
    
    //lerping data
    private const float moveTowardsTarget = 1.0f;
    
    #endregion

    #region Methods
    public void AddPackage(ObjectLerpPackage<CustomComponent> newPackage)
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


    private void ProcessPackage(ObjectLerpPackage<CustomComponent> pkg, int i)
    {
        LerpValue(pkg);
        RemovePackageAtIndexIfCompleted(pkg, i);
    }
    
    private void RemovePackageAtIndexIfCompleted(ObjectLerpPackage<CustomComponent> pkg, int i)
    {
        if (pkg.current == 1)
        {
            this.packageList.RemoveAt(i);
            pkg.finalCallback(pkg);
        }
    }
    private void LerpValue<T>(T pkg)
    where T : ObjectLerpPackage<CustomComponent>
    {
        updateCurrentLerpPercentage(pkg);

        if (pkg is FloatLerpPackage<CustomComponent>)
        {
            pkg.RunStepCallback(Mathf.Lerp((float)pkg.start, (float)pkg.target, pkg.current));
            return;
        }

        if (pkg is Vector3LerpPackage<CustomComponent>)
        {
            pkg.RunStepCallback(Vector3.Lerp((Vector3)pkg.start, (Vector3)pkg.target, pkg.current));
            return;
        }
    }

    private void updateCurrentLerpPercentage(LerpData.ObjectLerpPackage<CustomComponent> pkg)
    {
        pkg.elapsedTime += Time.deltaTime;
        
        pkg.current = pkg.animCurve.Evaluate(pkg.elapsedTime / pkg.timeToLerp);

        pkg.current = Mathf.Clamp01(pkg.current);
    }
    #endregion
}
