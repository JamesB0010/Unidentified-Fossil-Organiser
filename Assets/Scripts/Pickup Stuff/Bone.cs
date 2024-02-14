using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO{
    [ExecuteInEditMode]
public class Bone : MonoBehaviour, UFO.I_Pickupable
{
    private void OnValidate()
    {
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

        meshCollider.sharedMesh = this.GetComponent<MeshFilter>().mesh;
    }
}
}
