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

    public void HandleInteraction(CameraForwardsSampler playerCamSampler)
    {
        //Get a reference to the object the player is holding
        GameObject bone = playerCamSampler.ObjectInRange;
        
        //if the player is not holding a bone then return
        if (!bone.TryGetComponent(out Bone BoneCastObj))
            return;
        
        AddNewLerpPackageToPkgQueue(bone, BoneCastObj);
        this.BonesDelivered++;
    }

    private void AddNewLerpPackageToPkgQueue(GameObject bone, Bone BoneCastObj)
    {
        //finally create a new LerpPackage and add it to the queue
        //this.boneProcessingData.LerpPackageProcessor.AddPackage(new Vector3LerpPackage<Bone>(bone.transform.position,boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].position,
            //(pos, obj) => { obj.transform.position = pos;}));
        //this.boneProcessingData.LerpPackageProcessor.AddPackage(new Vector3LerpPackage<Bone>(bone.transform.rotation.eulerAngles,boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].rotation.eulerAngles,
            //(rot, obj) => { obj.gameObject.transform.rotation = Quaternion.Euler(rot);}));
    }

    private void Update()
    {
        this.boneProcessingData.LerpPackageProcessor.Update();
    }
}