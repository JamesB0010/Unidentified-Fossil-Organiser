using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO_PickupStuff{
    [ExecuteInEditMode]
public class Bone : UFO_PickupStuff.Pickupable
{
    #region Attributes
    
    private AudioSource audioSource;
    private ParticleSystem particleSystem;

    private bool isEnabled = true;

    [SerializeField] private string SkeletonStandBoneName;

    private bool hasCollidedBefore = false;
    
    #endregion

    #region Methods
    private void Start()
    {
        GetComponentReferences();
        this.particleSystem.Stop();
    }
    
    private void GetComponentReferences()
    {
        this.audioSource = gameObject.GetComponent<AudioSource>();
        this.particleSystem = gameObject.GetComponent<ParticleSystem>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //ignore the first collision the object has because thats it just falling to the ground when the game starts
        if (!this.hasCollidedBefore)
        {
            this.hasCollidedBefore = true;
            return;
        }
        this.audioSource.Play();
        this.particleSystem.Play();
    }
    
    
    public string GetSkeletonStandBoneName()
    {
        return SkeletonStandBoneName;
    }

    public bool IsEnabled
    {
        get
        {
            return this.isEnabled;
        }
        set
        {
            this.isEnabled = value;
        }
    }

    private void OnValidate()
    {
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

        meshCollider.sharedMesh = this.GetComponent<MeshFilter>().sharedMesh;
    }

}

#endregion
}
