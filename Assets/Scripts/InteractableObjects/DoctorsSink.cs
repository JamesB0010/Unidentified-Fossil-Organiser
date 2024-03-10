using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LerpData;
using UFO_PickupStuff;
using UFO_PlayerStuff;
using UnityEngine;

public class DoctorsSink : MonoBehaviour, I_Interactable
{
    private bool sinkFull = false;
    
    private float fillSpeed = 0.22f;

    [SerializeField] private AudioClip fillSound;

    public delegate void SinkStateNotification();

    public event SinkStateNotification OnSinkFull;

    public event SinkStateNotification OnSinkEmpty;

    private float emptySpeed = 0.2f;

    [SerializeField] private AudioClip emptySound;

    private AudioSource audioSource;

    /*private LerpPackageProcessor<GameObject> _lerpProcessor = new LerpPackageProcessor<GameObject>();*/

    [SerializeField]
    private GameObject water;

    [SerializeField]
    private GameObject topPosition;

    [SerializeField]
    private GameObject bottomPosition;

    private void Start()
    {
        this.audioSource = gameObject.GetComponent<AudioSource>();
    }

    private bool interactionInProgress = false;
    public new void HandleInteraction(CameraForwardsSampler playerCamSampler)
    {
        //check the player is interacting with me
        if (playerCamSampler.InteractableObjectInRangeRef != this.gameObject)
            return;

        if (this.interactionInProgress)
            return;
        
        
        /*if (this.sinkFull == false)
            this.FillSink();

        if (this.sinkFull == true)
            this.DrainSink();*/
    }

    /*private void FillSink()
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
        this.lerpProcessor.AddPackage(new ObjectLerpPackage<GameObject>(this.water, start, end, pkg =>
        {
            this.interactionInProgress = false;
            this.sinkFull = true;
            this.OnSinkFull?.Invoke();
        }, this.fillSpeed));

        this.audioSource.clip = this.fillSound;
        this.audioSource.Play();
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
        this.lerpProcessor.AddPackage(new ObjectLerpPackage<GameObject>(this.water, start, end, pkg =>
        {
            this.interactionInProgress = false;
            this.sinkFull = false;
            this.OnSinkEmpty?.Invoke();
        }, this.emptySpeed));

        this.audioSource.clip = this.emptySound;
        this.audioSource.Play();
    }*/

    // Update is called once per frame
    void Update()
    {
        /*this._lerpProcessor.Update();*/
    }
}
