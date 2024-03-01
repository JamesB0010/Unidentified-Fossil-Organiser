using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UFO_PickupStuff;
using UFO_PlayerStuff;
using UnityEngine;
using UnityEngine.Events;
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
    
    //Progress data

    [SerializeField] 
    private UnityEvent allBonesCollected = new UnityEvent();
    private int bonesNeededToWin;

    private int bonesDelivered = 0;

    private int BonesDelivered
    {
        get
        {
            return this.bonesDelivered;
        }

        set
        {
            this.bonesDelivered = value;
            if(this.bonesDelivered >= this.bonesNeededToWin)
                this.allBonesCollected.Invoke();
        }
    }
    
    private void Start()
    {
        this.bonesNeededToWin = this.skeletonBones.Count;
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

    public new void HandleInteraction(CameraForwardsSampler playerCamSampler)
    {
        if (playerCamSampler.InteractableObjectInRangeRef != this.gameObject)
            return;

        try
        {
            //Get a reference to the object the player is holding
            GameObject bone = playerCamSampler.ObjectInRange;

            //if the player is not holding a bone then return
            if (!bone.TryGetComponent(out Bone BoneCastObj))
                return;

            AddNewLerpPackageToPkgQueue(bone, BoneCastObj);
            this.BonesDelivered++;
        }
        catch (NullReferenceException e)
        {
            return;
        }
    }

    private void AddNewLerpPackageToPkgQueue(GameObject bone, Bone BoneCastObj)
    {
        //define a start position and rotation
        PositionRotationPair start = new PositionRotationPair
        {
            position = bone.transform.position,
            rotation = bone.transform.rotation.eulerAngles
        };

        //define a end/target position and rotation
        PositionRotationPair end = new PositionRotationPair()
        {
            position = boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].position,
            rotation = boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].rotation.eulerAngles
        };
        
        //finally create a new LerpPackage and add it to the queue
        this.boneProcessingData.LerpPackageProcessor.AddPackage(new ObjectLerpPackage<Bone>(bone, start, end, this.boneProcessingData.processedPackageFinalizationCallback));
    }

    private void Update()
    {
        this.boneProcessingData.LerpPackageProcessor.ProcessLerpPackageList();
    }
}