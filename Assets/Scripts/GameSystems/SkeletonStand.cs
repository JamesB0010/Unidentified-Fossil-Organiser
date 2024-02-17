using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UFO_PickupStuff;
using UFO_PlayerStuff;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;

public class SkeletonStand : MonoBehaviour, I_Interactable
{
    //Bone positional Data
    [SerializeField] 
    private List<GameObject> skeletonBones = new List<GameObject>();
    private Dictionary<string, Transform> boneNameTransforms = new Dictionary<string, Transform>();
    
    //Bone processing data
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
        //Get a reference to the object the player is holding
        GameObject bone = playerCamSampler.ObjectInRange;
        
        //if the player is not holding a bone then return
        if (!bone.TryGetComponent(out Bone BoneCastObj))
            return;
        
        AddNewLerpPackageToPkgQueue(bone, BoneCastObj);
    }

    private void AddNewLerpPackageToPkgQueue(GameObject bone, Bone BoneCastObj)
    {
        //define a start position and rotation
        ObjectLerpPackage<Bone>.PositionRotationPair start = new ObjectLerpPackage<Bone>.PositionRotationPair
        {
            position = bone.transform.position,
            rotation = bone.transform.rotation.eulerAngles
        };

        //define a end/target position and rotation
        ObjectLerpPackage<Bone>.PositionRotationPair end = new ObjectLerpPackage<Bone>.PositionRotationPair()
        {
            position = boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].position,
            rotation = boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].rotation.eulerAngles
        };
        
        //finally create a new LerpPackage and add it to the queue
        this.boneProcessingData.lerpPackageQueue.Enqueue(new ObjectLerpPackage<Bone>(bone, start, end));
    }

    private void Update()
    {
        this.boneProcessingData.LerpPackageProcessor.ProcessLerpPackageQueue(boneProcessingData.processedPackageFinalizationCallback);
        this.boneProcessingData.LerpPackageProcessor.DequeueCompletedPackages();
    }
}