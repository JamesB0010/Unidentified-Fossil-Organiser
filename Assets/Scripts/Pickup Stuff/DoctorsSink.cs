using System.Collections;
using System.Collections.Generic;
using LerpData;
using UFO_PickupStuff;
using UFO_PlayerStuff;
using UnityEngine;

public class DoctorsSink : MonoBehaviour, I_Interactable
{
    private bool sinkFull = false;

    private LerpPackageProcessor<GameObject> lerpProcessor;

    [SerializeField]
    private GameObject water;

    [SerializeField]
    private GameObject topPosition;

    [SerializeField]
    private GameObject bottomPosition;

    private bool interactionInProgress = false;
    public new void HandleInteraction(CameraForwardsSampler playerCamSampler)
    {
        //check the player is interacting with me
        if (playerCamSampler.InteractableObjectInRangeRef != this.gameObject)
            return;

        if (this.interactionInProgress)
            return;
        
        
        if (this.sinkFull = false)
            this.FillSink();

        if (this.sinkFull == true)
            this.DrainSink();
    }

    private void FillSink()
    {
        this.interactionInProgress = true;
        PositionRotationPair start = new PositionRotationPair
        {
            position = this.bottomPosition.transform.position,
            rotation = this.bottomPosition.transform.rotation.eulerAngles
        };

        //define a end/target position and rotation
        PositionRotationPair end = new PositionRotationPair()
        {
            position = this.topPosition.transform.position,
            rotation = this.topPosition.transform.rotation.eulerAngles
        };
        
        //finally create a new LerpPackage and add it to the queue
        this.lerpProcessor.AddPackage(new ObjectLerpPackage<GameObject>(this.water, start, end));
    }

    private void DrainSink()
    {
        this.interactionInProgress = true;
        PositionRotationPair start = new PositionRotationPair
        {
            position = this.topPosition.transform.position,
            rotation = this.topPosition.transform.rotation.eulerAngles
        };

        //define a end/target position and rotation
        PositionRotationPair end = new PositionRotationPair()
        {
            position = this.bottomPosition.transform.position,
            rotation = this.bottomPosition.transform.rotation.eulerAngles
        };
        
        //finally create a new LerpPackage and add it to the queue
        this.lerpProcessor.AddPackage(new ObjectLerpPackage<GameObject>(this.water, start, end));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
