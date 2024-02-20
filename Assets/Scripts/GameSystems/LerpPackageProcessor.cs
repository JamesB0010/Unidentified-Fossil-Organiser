using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LerpPackageProcessor <QueueType>
{
    #region Attributes
    //define a callback type for when a package has been processed
    public delegate void PackageProcessed(ObjectLerpPackage<QueueType> pkg);
    
    //queue data
    private List<ObjectLerpPackage<QueueType>> packageList;
    private List<int> completedPackageIndexes = new List<int>();
    
    //lerping data
    private const float moveTowardsTarget = 1.0f;
    private float lerpSpeed;
    
    #endregion

    #region Methods
    
    public LerpPackageProcessor(float lerpSpeed, ref List<ObjectLerpPackage<QueueType>> list)
    {
        this.lerpSpeed = lerpSpeed;
        this.packageList = list;
    }

    public void ProcessLerpPackageQueue(PackageProcessed packageProcessedCallBack)
    {
        Debug.Log(this.packageList.Count);
        this.ResetCompletedPackagesIndexList();
        for (int i = this.packageList.Count - 1; i >= 0; i--)
        {
            this.ProcessPackage(this.packageList[i], packageProcessedCallBack, i);
        }
    }

    public void ProcessLerpPackageQueue()
    {
        this.ResetCompletedPackagesIndexList();
        for (int i = this.packageList.Count - 1; i >= 0; i--)
        {
            this.ProcessPackage(this.packageList[i], i);
        }
    }

    private void ProcessPackage(ObjectLerpPackage<QueueType> pkg, PackageProcessed packageProcessedCallBack, int i)
    {
        LerpPackagePositionRotation(pkg);

        if (pkg.current == 1)
        {
            packageProcessedCallBack(pkg);
            this.packageList.RemoveAt(i);
        }
    }

    private void ProcessPackage(ObjectLerpPackage<QueueType> pkg, int i)
    {
        LerpPackagePositionRotation(pkg);
        if (pkg.current == 1)
            this.packageList.RemoveAt(i);
    }

    private void LerpPackagePositionRotation(ObjectLerpPackage<QueueType> pkg)
    {
        updateCurrentLerpPercentage(pkg);

        LerpPosition(pkg);

        LerpRotation(pkg);
    }

    private void updateCurrentLerpPercentage(ObjectLerpPackage<QueueType> pkg)
    {
        pkg.current = Mathf.MoveTowards(pkg.current, moveTowardsTarget, this.lerpSpeed * Time.deltaTime);
    }
    private static void LerpPosition(ObjectLerpPackage<QueueType> pkg)
    {
        pkg.objectToLerp.transform.position = Vector3.Lerp(pkg.start.position, pkg.target.position, pkg.current);
    }
    private static void LerpRotation(ObjectLerpPackage<QueueType> pkg)
    {
        Vector3 lerpedRotation = Vector3.Lerp(pkg.start.rotation, pkg.target.rotation, pkg.current);
        pkg.objectToLerp.transform.rotation = Quaternion.Euler(lerpedRotation);
    }

    private void ResetCompletedPackagesIndexList()
    {
        this.completedPackageIndexes.Clear();
    }
    
    
    #endregion
}
