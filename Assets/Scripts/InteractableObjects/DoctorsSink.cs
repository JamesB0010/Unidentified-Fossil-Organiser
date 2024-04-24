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
        
        
        if (this.sinkFull == false)
            this.FillSink();

        if (this.sinkFull == true)
            this.DrainSink();
    }

    private void FillSink()
    {
        this.interactionInProgress = true;
        this.bottomPosition.transform.position.LerpTo(this.topPosition.transform.position,
            4,
            value =>
            {
                this.water.transform.position = value;
            },
            pkg =>
            {
                this.interactionInProgress = false;
                this.sinkFull = true;
                this.OnSinkFull?.Invoke();
            });

        this.audioSource.clip = this.fillSound;
        this.audioSource.Play();
    }

    private void DrainSink()
    {
        this.interactionInProgress = true;
        
        this.topPosition.transform.position.LerpTo(this.bottomPosition.transform.position,
            6,
            value =>
            {
                this.water.transform.position = value;
            },
            pkg =>
            {
                this.interactionInProgress = false;
                this.sinkFull = false;
                this.OnSinkEmpty?.Invoke();
            });

        this.audioSource.clip = this.emptySound;
        this.audioSource.Play();
    }
}
