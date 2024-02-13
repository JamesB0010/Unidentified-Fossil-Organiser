using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

    [RequireComponent(typeof(Camera))]
public class CameraForwardsSampler : MonoBehaviour
{
    private UnityEngine.Camera camera;
    private bool pickupableObjectInRange = false;
    
    [SerializeField]
    private UFO.UiAlert PickupableObjectInRangeUnityEvent;
    [SerializeField]
    private UFO.UiAlert PickupableObjectOutOfRangeUnityEvent;
    

    private bool PickupableObjectInRange
    {
        set
        {
            //if the value hasn't changed from last time then do nothing
            if (this.pickupableObjectInRange == value)
                return;
            
            if (value == true)
            {
                //tell UI that your ready to pick up object
                this.PickupableObjectInRangeUnityEvent.Invoke();
            }
            else
            {
                //tell Ui that your not ready to pick up object
                this.PickupableObjectOutOfRangeUnityEvent.Invoke();
            }
            
            //finally set the value
            pickupableObjectInRange = value;
        }
    }

    void Start()
    {
        SendFirstUIEvents();
        AssignCameraReference();
    }

    
    //this is necessary becase when these events are first
    //sent the camera tweaks out
    private void SendFirstUIEvents()
    {
        this.PickupableObjectInRangeUnityEvent.Invoke();
        this.PickupableObjectOutOfRangeUnityEvent.Invoke();
    }

    private void AssignCameraReference()
    {
        camera = gameObject.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        AreYouInRangeToPickupAnObject();
    }

    private void AreYouInRangeToPickupAnObject()
    {
        //setup variables
        this.PickupableObjectInRange = false;
        UnityEngine.RaycastHit hit;

        bool raycastCollision = Physics.Raycast(this.camera.transform.position, this.camera.transform.forward, out hit);

        if (this.ReadyToPickupObject(hit, raycastCollision))
            this.PickupableObjectInRange = true;
    }

    private bool ReadyToPickupObject(UnityEngine.RaycastHit hit, bool raycastCollision)
    {
        if (!raycastCollision)
            return false;

        if (!hit.rigidbody)
            return false;

        if (!hit.rigidbody.gameObject is UFO.I_Pickupable)
            return false;

        return true;
    }
}
    

