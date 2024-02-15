using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace UFO_PlayerStuff
{
    [RequireComponent(typeof(Camera))]
    
    //dear reader methods have been ordered in a way that it makes most sense
    //to read this class from top to bottom as the order of method declarations 
    //is the same order the methods are called in
    public class CameraForwardsSampler : MonoBehaviour
    {
        #region Attributes
        private UnityEngine.Camera camera;
        private bool pickupableObjectInRange = false;

        //Events
        //You can add your own listeners to these events in the unity editor
        [SerializeField] public UnityEvent PickupableObjectInRangeUnityEvent = new UnityEvent();

        [SerializeField] private UnityEvent PickupableObjectOutOfRangeUnityEvent = new UnityEvent();

        
        private GameObject objectInRange;
        public GameObject ObjectInRange
        {
            get
            {
                return this.objectInRange;
            }
        }
        #endregion
        
        
        void Start()
        {
            AssignCameraReference();
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

            bool raycastCollision =
                Physics.Raycast(this.camera.transform.position, this.camera.transform.forward, out hit);

            if (this.ReadyToPickupObject(hit, raycastCollision))
                StageObjectPickup(hit);
        }

        private bool PickupableObjectInRange
        {
            set
            {
                //if the value hasn't changed from last time then do nothing
                if (this.pickupableObjectInRange == value)
                    return;

                if (value == true)
                {
                    //tell listeners that your ready to pick up object
                    this.DispachObjectInRangeEvent();
                }
                else
                {
                    //tell listerners that your not ready to pick up object
                    this.PickupableObjectOutOfRangeUnityEvent.Invoke();
                }

                //finally set the value
                pickupableObjectInRange = value;
            }
        }
        private void DispachObjectInRangeEvent()
        {
            this.PickupableObjectInRangeUnityEvent.Invoke();
        }

        private bool ReadyToPickupObject(UnityEngine.RaycastHit hit, bool raycastCollision)
        {
            if (!raycastCollision)
                return false;

            if (!hit.rigidbody)
                return false;

            if (!hit.rigidbody.gameObject is UFO_PickupStuff.I_Pickupable)
                return false;

            return true;
        }

        /// <summary>
        /// This involves assigning the variables assosiated with picking up an object
        /// and calling the correct functions
        /// </summary>
        private void StageObjectPickup(RaycastHit hit)
        {
            this.objectInRange = hit.rigidbody.gameObject;
            this.PickupableObjectInRange = true;
        }

    }
}

