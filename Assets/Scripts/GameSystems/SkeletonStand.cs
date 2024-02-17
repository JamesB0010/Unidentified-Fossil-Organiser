using System;
using System.Collections;
using System.Collections.Generic;
using UFO_PickupStuff;
using UFO_PlayerStuff;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;

public class SkeletonStand : MonoBehaviour, I_Interactable
{
    [SerializeField] 
    private List<GameObject> skeletonBones = new List<GameObject>();
    private Dictionary<string, Transform> boneNameTransforms = new Dictionary<string, Transform>();

    [SerializeField] 
    private float bonePlacementSpeed = 1;
    private const float moveTowardsTarget = 1.0f;
    private Queue<ObjectLerpPackage> lerpPackageQueue = new Queue<ObjectLerpPackage>();
    
    
    private void Start()
    {
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
        this.lerpPackageQueue.Enqueue(new ObjectLerpPackage(bone, bone.transform.position, bone.transform.rotation.eulerAngles, boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].position,boneNameTransforms[BoneCastObj.GetSkeletonStandBoneName()].rotation.eulerAngles));
    }


    private void Update()
    {
        ProcessLerpPackageQueue();
    }

    private void ProcessLerpPackageQueue()
    {
        int dequeueCount = 0;
        foreach (ObjectLerpPackage pkg in this.lerpPackageQueue)
        {
            pkg.current = Mathf.MoveTowards(pkg.current, moveTowardsTarget, this.bonePlacementSpeed * Time.deltaTime);

            pkg.objectToLerp.transform.position = Vector3.Lerp(pkg.startPosition, pkg.targetPosition, pkg.current);

            Vector3 lerpedRotation = Vector3.Lerp(pkg.startRotation, pkg.targetRotation, pkg.current);
            pkg.objectToLerp.transform.rotation = Quaternion.Euler(lerpedRotation);

            if (pkg.current == 1)
            {
                dequeueCount++;
                pkg.objectToLerp.GetComponent<Rigidbody>().isKinematic = true;
                pkg.objectToLerp.GetComponent<Bone>().IsEnabled = false;
            }
        }

        for (int i = 0; i < dequeueCount; i++)
        {
            this.lerpPackageQueue.Dequeue();
        }
    }
}
