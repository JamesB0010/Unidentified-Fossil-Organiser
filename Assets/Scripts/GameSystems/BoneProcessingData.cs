using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFO_PickupStuff;
struct BoneProcessingData
{
    public Queue<ObjectLerpPackage<Bone>> lerpPackageQueue;
    public LerpPackageProcessor<Bone> LerpPackageProcessor;
    public LerpPackageProcessor<Bone>.PackageProcessed processedPackageFinalizationCallback;
    
    public BoneProcessingData(float bonePlacementSpeed)
    {
        //create a new queue to hold the packages
        this.lerpPackageQueue = new Queue<ObjectLerpPackage<Bone>>();
        
        //create a new processor to process the queue
        this.LerpPackageProcessor =
            new LerpPackageProcessor<Bone>(bonePlacementSpeed, ref this.lerpPackageQueue);
        
        //create a new lambda function to be called whenever a package has been processed
        this.processedPackageFinalizationCallback = pkg =>
        {
            pkg.rb.isKinematic = true;
            pkg.customComponent.IsEnabled = false;
        };
    }
}
