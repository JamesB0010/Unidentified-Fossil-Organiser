using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UFO
{
    [System.Serializable]
    public class UiAlert : UnityEvent
    {
        
    };

    [RequireComponent(typeof(Camera))]
public class CameraForwardsSampler : MonoBehaviour
{
    private UnityEngine.Camera camera;
    public UiAlert PickupableObjectInRangeUnityEvent;
    public UiAlert PickupableObjectOutOfRangeUnityEvent;

    private bool pickupableObjectInRange = false;

    private bool PickupableObjectInRange
    {
        set
        {
            if (this.pickupableObjectInRange == value)
                return;

            if (value == true)
            {
                this.PickupableObjectInRangeUnityEvent.Invoke();
            }
            else
            {
                this.PickupableObjectOutOfRangeUnityEvent.Invoke();
            }
            
            pickupableObjectInRange = value;
        }
    }

    public void Awake()
    {
        this.PickupableObjectInRangeUnityEvent.Invoke();
        this.PickupableObjectOutOfRangeUnityEvent.Invoke();
    }

    void Start()
    {
        
        camera = gameObject.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        this.PickupableObjectInRange = false;
        UnityEngine.RaycastHit hit;
        bool raycastCollision = Physics.Raycast(this.camera.transform.position, this.camera.transform.forward, out hit);

        if (!raycastCollision)
            return;

        if (!hit.rigidbody)
            return;

        if (!hit.rigidbody.gameObject is UFO.I_Pickupable)
            return;

        this.PickupableObjectInRange = true;
    }
}
    
}
