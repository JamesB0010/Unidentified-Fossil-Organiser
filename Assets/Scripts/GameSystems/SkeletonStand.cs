using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UFO_PickupStuff;
using UFO_PlayerStuff;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;

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


public class SkeletonStand : MonoBehaviour, I_Interactable
{
    //Bone positional Data
    [SerializeField] 
    private List<GameObject> skeletonBones = new List<GameObject>();
    private Dictionary<string, Transform> boneNameTransforms = new Dictionary<string, Transform>();

    [SerializeField] 
    private float bonePlacementSpeed = 1;

    private BoneProcessingData boneProcessingData;
    
    private void Start()
    {
        this.boneProcessingData = new BoneProcessingData(this.bonePlacementSpeed);
        PopulateBoneNameTransforms();
    }

    private void PopulateBoneNameTransforms()
    {
        foreach (GameObject bone in this.skeletonBones)
        {
            this.boneNameTransforms.Add(bone.name, bone.transform);
        }
    }

    public void HandleInteraction(CameraForwardsSampler playerCamSampler)
    {
        GameObject bone = playerCamSampler.ObjectInRange;
        if (!bone.TryGetComponent(out Bone BoneCastObj))
            return;
        this.boneProcessingData.lerpPackageQueue.Enqueue(new ObjectLerpPackage<Bone>(bone, bone.transform.position, bone.transform.rotation.eulerAngles, boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].position,boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].rotation.eulerAngles));
    }
    
    private void Update()
    {
        this.boneProcessingData.LerpPackageProcessor.ProcessLerpPackageQueue(boneProcessingData.processedPackageFinalizationCallback);
        this.boneProcessingData.LerpPackageProcessor.DequeueCompletedPackages();
    }
}