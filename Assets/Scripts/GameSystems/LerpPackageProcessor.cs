using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

class LerpPackageProcessor <ListType>
{
    #region Attributes
    //define a callback type for when a package has been processed
    public delegate void PackageProcessed(ObjectLerpPackage<ListType> pkg);
    
    //queue data
    private List<ObjectLerpPackage<ListType>> packageList = new List<ObjectLerpPackage<ListType>>();
    
    //lerping data
    private const float moveTowardsTarget = 1.0f;
    
    #endregion

    #region Methods
    public void AddPackage(ObjectLerpPackage<ListType> newPackage)
    {
        this.packageList.Add(newPackage);
    }

    public void ProcessLerpPackageList()
    {
        for (int i = this.packageList.Count - 1; i >= 0; i--)
        {
            this.ProcessPackage(this.packageList[i], i);
        }
    }


    private void ProcessPackage(ObjectLerpPackage<ListType> pkg, int i)
    {
        LerpPackagePositionRotation(pkg);
        RemovePackageAtIndexIfCompleted(pkg, i);
    }
    
    private void RemovePackageAtIndexIfCompleted(ObjectLerpPackage<ListType> pkg, int i)
    {
        if (pkg.current == 1)
        {
            this.packageList.RemoveAt(i);
            pkg.callback(pkg);
        }
    }
    private void LerpPackagePositionRotation(ObjectLerpPackage<ListType> pkg)
    {
        updateCurrentLerpPercentage(pkg);

        LerpPosition(pkg);

        LerpRotation(pkg);
    }

    private void updateCurrentLerpPercentage(LerpData.ObjectLerpPackage<ListType> pkg)
    {
        pkg.current = Mathf.MoveTowards(pkg.current, moveTowardsTarget, pkg.lerpSpeed * Time.deltaTime);
    }
    private static void LerpPosition(LerpData.ObjectLerpPackage<ListType> pkg)
    {
        pkg.objectToLerp.transform.position = Vector3.Lerp(pkg.start.position, pkg.target.position, pkg.current);
    }
    private static void LerpRotation(LerpData.ObjectLerpPackage<ListType> pkg)
    {
        Vector3 lerpedRotation = Vector3.Lerp(pkg.start.rotation, pkg.target.rotation, pkg.current);
        pkg.objectToLerp.transform.rotation = Quaternion.Euler(lerpedRotation);
    }
    
    
    #endregion
}
