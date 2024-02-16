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
        private bool interactableObjectInRange = false;

        //Events
        //You can add your own listeners to these events in the unity editor
        [SerializeField] public UnityEvent PickupableObjectInRangeUnityEvent = new UnityEvent();

        [SerializeField] private UnityEvent PickupableObjectOutOfRangeUnityEvent = new UnityEvent();

        [SerializeField] private UnityEvent InteractableObjectInRangeEvent = new UnityEvent();

        [SerializeField] private UnityEvent InteractableObjectOutOfRange = new UnityEvent();

        
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
        private void Update()
        {
            AreYouInRangeToPickupAnObject();
        }
        private void AreYouInRangeToPickupAnObject()
        {
            //setup variables
            this.PickupableObjectInRange = false;
            this.InteractableObjectInRange = false;
            UnityEngine.RaycastHit hit;

            bool raycastCollision =
                Physics.Raycast(this.camera.transform.position, this.camera.transform.forward, out hit);

            if (this.ReadyToPickupObject(hit, raycastCollision))
                StageObjectPickup(hit);
            if(this.ReadyToInteractWithObject(hit, raycastCollision))
                StageObjectInteraction(hit);
            else
            {
                this.checkIfInteractableObjectIsOtherSideOfPickedUpObject(hit);
            }
            
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

        private bool InteractableObjectInRange
        {
            set
            {
                if (this.interactableObjectInRange == value)
                    return;

                if (value == true)
                    this.InteractableObjectInRangeEvent.Invoke();
                else
                {
                    this.InteractableObjectOutOfRange.Invoke();
                }

                this.interactableObjectInRange = value;
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

            if (!hit.rigidbody.gameObject.TryGetComponent(out UFO_PickupStuff.I_Pickupable pickupable))
                return false;

            return true;
        }

        private bool ReadyToInteractWithObject(RaycastHit hit, bool raycastCollision)
        {
            if (!raycastCollision)
                return false;

            if (!hit.collider.gameObject.TryGetComponent(out I_Interactable interactable))
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


        private void StageObjectInteraction(RaycastHit hit)
        {
            this.objectInRange = hit.collider.gameObject;
            this.InteractableObjectInRange = true;
            Debug.Log(hit.collider.gameObject.name);
        }

        private void checkIfInteractableObjectIsOtherSideOfPickedUpObject(RaycastHit hit)
        {
            //find the point where the raycast exited the mesh
            
        }
    }
}

