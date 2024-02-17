using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LerpPackageProcessor <QueueType>
{
    //define a callback type for when a package has been processed
    public delegate void PackageProcessed(ObjectLerpPackage<QueueType> pkg);
    
    //queue data
    private Queue<ObjectLerpPackage<QueueType>> packageQueue;
    private int completedPackageCount = 0;
    
    //lerping data
    private const float moveTowardsTarget = 1.0f;
    private float lerpSpeed;
    
    public LerpPackageProcessor(float lerpSpeed, ref Queue<ObjectLerpPackage<QueueType>> queue)
    {
        this.lerpSpeed = lerpSpeed;
        this.packageQueue = queue;
    }

    public void ProcessLerpPackageQueue(PackageProcessed packageProcessedCallBack)
    {
        this.ResetCompletedPackagesCount();
        foreach (ObjectLerpPackage<QueueType> pkg in this.packageQueue)
        {
            this.ProcessPackage(pkg, packageProcessedCallBack);
        }
    }

    private void ProcessPackage(ObjectLerpPackage<QueueType> pkg, PackageProcessed packageProcessedCallBack)
    {
        pkg.current = Mathf.MoveTowards(pkg.current, moveTowardsTarget, this.lerpSpeed * Time.deltaTime);

        pkg.objectToLerp.transform.position = Vector3.Lerp(pkg.startPosition, pkg.targetPosition, pkg.current);

        Vector3 lerpedRotation = Vector3.Lerp(pkg.startRotation, pkg.targetRotation, pkg.current);
        pkg.objectToLerp.transform.rotation = Quaternion.Euler(lerpedRotation);

        if (pkg.current == 1)
        {
            this.completedPackageCount++;
            packageProcessedCallBack(pkg);
        }
    }

    private void ResetCompletedPackagesCount()
    {
        this.completedPackageCount = 0;
    }

    public void DequeueCompletedPackages()
    {
        for (int i = 0; i < this.completedPackageCount; i++)
        {
            this.packageQueue.Dequeue();
        }
    }
}
