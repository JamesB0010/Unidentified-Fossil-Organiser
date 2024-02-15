using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO_PickupStuff{
    [ExecuteInEditMode]
public class Bone : MonoBehaviour, UFO_PickupStuff.I_Pickupable
{
    private AudioSource audioSource;

    private ParticleSystem particleSystem;
    private void OnValidate()
    {
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

        meshCollider.sharedMesh = this.GetComponent<MeshFilter>().sharedMesh;
    }

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
        this.audioSource.Play();
        this.particleSystem.Play();
    }
}
}
