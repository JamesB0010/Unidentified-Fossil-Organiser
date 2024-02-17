using System;
using System.Collections;
using System.Collections.Generic;
using UFO_PlayerStuff;
using UnityEngine;
using UnityEngine.Serialization;

class ObjectLerpPackage
{

    public ObjectLerpPackage(GameObject objectToLerp, Vector3 startPosition, Vector3 startRotation,
        Vector3 targetPosition, Vector3 targetRotation)
    {
        this.objectToLerp = objectToLerp;
        this.startPosition = startPosition;
        this.startRotation = startRotation;
        this.targetPosition = targetPosition;
        this.targetRotation = targetRotation;
    }
    public GameObject objectToLerp;
    public Vector3 startPosition;
    public Vector3 startRotation;

    public Vector3 targetPosition;
    public Vector3 targetRotation;

    public float current = 0.0f;
}
public class SkeletonStand : MonoBehaviour, I_Interactable
{
    
    [SerializeField] 
    private List<GameObject> skeletonBones = new List<GameObject>();

    [SerializeField] 
    private float bonePlacementSpeed = 1;

    private const float moveTowardsTarget = 1.0f;

    private Dictionary<string, Transform> boneNameTransforms = new Dictionary<string, Transform>();

    private Queue<ObjectLerpPackage> lerpPackages = new Queue<ObjectLerpPackage>();
    public void HandleInteraction(CameraForwardsSampler playerCamSampler)
    {
        GameObject bone = playerCamSampler.ObjectInRange;
        Debug.Log("Skeleton holder handle interaction: " + bone.name);
        this.lerpPackages.Enqueue(new ObjectLerpPackage(bone, bone.transform.position, bone.transform.rotation.eulerAngles, boneNameTransforms["Skull"].position,boneNameTransforms["Skull"].rotation.eulerAngles));
    }

    private void Start()
    {
        foreach (GameObject bone in this.skeletonBones)
        {
            this.boneNameTransforms.Add(bone.name, bone.transform);
        }
    }

    private void Update()
    {
        foreach (ObjectLerpPackage pkg in this.lerpPackages)
        {
            pkg.current = Mathf.MoveTowards(pkg.current, moveTowardsTarget, this.bonePlacementSpeed * Time.deltaTime);

            pkg.objectToLerp.transform.position = Vector3.Lerp(pkg.startPosition, pkg.targetPosition, pkg.current);
            
            Vector3 lerpedRotation = Vector3.Lerp(pkg.startRotation, pkg.targetRotation, pkg.current);
            pkg.objectToLerp.transform.rotation = Quaternion.Euler(lerpedRotation);

            if (pkg.current == 1)
                this.lerpPackages.Dequeue();

        }
    }
}
