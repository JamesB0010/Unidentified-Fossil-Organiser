using System.Collections;
using System.Collections.Generic;
using UFO_PickupStuff;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(MeshCollider))]
public class GenericPickupableObject : MonoBehaviour, UFO_PickupStuff.I_Pickupable
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshCollider>().convex = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
