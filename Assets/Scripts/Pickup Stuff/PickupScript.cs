using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UFO.CameraForwardsSampler))]
public class PickupScript : MonoBehaviour
{
    [SerializeField]
    private Transform holdItemLocation;

    private bool readyToPickup = false;

    private float throwScalar = 7f;

    private float upThrowAddVec = 2f;

    private bool interactButtonActive;

    private GameObject objectInRange;

    private UFO.CameraForwardsSampler spaceSampler;

    private bool holdingObject = false;

    [SerializeField]
    private float dampingFactorScalar = 5f;
    [SerializeField]
    private float speedScalar = 6.13f;

    private float droppedObjectTimeStamp = 0;

    private void Start()
    {
        this.spaceSampler = gameObject.GetComponent<UFO.CameraForwardsSampler>();
    }

    private bool InteractButtonActive
    {
        set
        {
            if (this.interactButtonActive == true)
            {
                if (value == false)
                {
                    if (this.holdingObject)
                    {
                        DropObject();
                        return;
                    }
                    //Let go of interact button
                    if (this.readyToPickup == true && Time.time - this.droppedObjectTimeStamp > 0.1)
                    {
                        this.PickupObject();
                    }
                }
            }

            this.interactButtonActive = value;
        }
    }
    
    private void Update()
    {
        int pickupObject =  (int)Input.GetAxis("Pickup Object");
        this.InteractButtonActive = pickupObject == 1 ? true : false;
    }

    private void FixedUpdate()
    {
        if (this.holdingObject)
        {
            MoveObjectToHoldPoint();
        }
    }

    private void MoveObjectToHoldPoint()
    {
        Rigidbody objectRb = this.objectInRange.GetComponent<Rigidbody>();
        
        Vector3 forceVector = this.holdItemLocation.position - objectRb.position;
        
        //Calculate the distance to the target position
        float distanceToTarget = forceVector.magnitude;
        
        //apply damping factor based on distance
        float dampingFactor = Mathf.Clamp01(distanceToTarget / this.dampingFactorScalar);

        //calculate desired velocity
        Vector3 desiredVelocity = forceVector.normalized * this.speedScalar;
        
        //calculate the change in velocity needed
        Vector3 deltaVelocity = (desiredVelocity - objectRb.velocity) * dampingFactor;
        
        //calculate the force needed based on change in velocity
        Vector3 force = deltaVelocity / Time.fixedDeltaTime;
        
        objectRb.AddForce(force, ForceMode.Force);
    }


    public void UpdateItemToBePickedUp()
    {
        if (this.holdingObject)
            return;
        this.readyToPickup = true;
        this.objectInRange = this.spaceSampler.ObjectInRange;
    }

    public void ObjectToBePickedUpOutOfRange()
    {
        this.readyToPickup = false;
    }

    private void PickupObject()
    {
        this.holdingObject = true;
        this.readyToPickup = false;
    }

    private void DropObject()
    {
        this.readyToPickup = true;
        this.holdingObject = false;
        this.droppedObjectTimeStamp = Time.time;
        this.objectInRange.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward.normalized * this.throwScalar + new Vector3(0, this.upThrowAddVec, 0), ForceMode.Impulse);
    }
}
