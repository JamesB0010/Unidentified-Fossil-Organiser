using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO{
    [ExecuteInEditMode]
public class Bone : MonoBehaviour, UFO.I_Pickupable
{
    private AudioSource audioSource;
    private void OnValidate()
    {
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

        meshCollider.sharedMesh = this.GetComponent<MeshFilter>().sharedMesh;
    }

    private void Start()
    {
        this.audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        this.audioSource.Play();
    }
}
}
