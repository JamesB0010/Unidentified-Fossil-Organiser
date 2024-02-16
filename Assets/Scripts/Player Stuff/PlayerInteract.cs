using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract :UFO_PickupStuff.PickupScript
{
    private UFO_PickupStuff.AbleToInteractStateData interactStateData = new UFO_PickupStuff.AbleToInteractStateData();

    [SerializeField]
    private UnityEvent OnInteractWithObject = new UnityEvent();
     private bool InteractButtonActive
    {
        set
        {
            if (InteractKeyPreviousStateUp())
            {
                this.interactStateData.interactButtonActive = value;
                return;
            }

            if(value == true)
            {
                this.interactStateData.interactButtonActive = value;
                return;
            }

            this.InteractWithObject();

            this.interactStateData.interactButtonActive = value;
        }
    }

    private void Update()
    {
        int interactObject = (int)Input.GetAxis("Pickup Object");
        if(interactObject == 1)
        {
           this.InteractButtonActive = true;
        }
        else
        {
            this.InteractButtonActive = false;
        }
    }

    private void InteractWithObject()
    {
        this.OnInteractWithObject.Invoke();
        Debug.Log("interact");
    }
}
