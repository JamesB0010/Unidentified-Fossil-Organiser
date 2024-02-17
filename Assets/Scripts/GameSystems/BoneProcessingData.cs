using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFO_PickupStuff;
struct BoneProcessingData
{
    public BoneProcessingData(float bonePlacementSpeed)
    {
        this.lerpPackageQueue = new Queue<ObjectLerpPackage<Bone>>();
        this.LerpPackageProcessor =
            new LerpPackageProcessor<Bone>(bonePlacementSpeed, ref this.lerpPackageQueue);
        this.processedPackageFinalizationCallback = pkg =>
        {
            pkg.rb.isKinematic = true;
            pkg.customComponent.IsEnabled = false;
        };
    }
    public Queue<ObjectLerpPackage<Bone>> lerpPackageQueue;
    public LerpPackageProcessor<Bone> LerpPackageProcessor;
    public LerpPackageProcessor<Bone>.PackageProcessed processedPackageFinalizationCallback;
}
