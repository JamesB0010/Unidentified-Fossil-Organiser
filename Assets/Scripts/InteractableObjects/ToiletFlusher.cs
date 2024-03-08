using System.Collections;
using System.Collections.Generic;
using LerpData;
using UFO_PlayerStuff;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ToiletFlusher : MonoBehaviour, I_Interactable
{

    private AudioSource audioSource;

    private bool bowlFull = true;

    private bool ineractionInProgress = false;

    private float emptySpeed = 0.7f;

    private float fillSpeed = 0.15f;

    private LerpPackageProcessor<GameObject> _lerpProcessor = new LerpPackageProcessor<GameObject>();

    [SerializeField] private GameObject water;

    [SerializeField] private GameObject topPosition;

    [SerializeField] private GameObject bottomPosition;
    
    public new void HandleInteraction(CameraForwardsSampler sampler)
    {
        //check the player is interacting with me
        if (sampler.InteractableObjectInRangeRef != this.gameObject)
            return;

        if (this.ineractionInProgress)
            return;

        /*if (this.bowlFull)
            this.Flush();*/
        
        this.audioSource.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    /*private void Flush()
    {*/
        /*this.ineractionInProgress = true;
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
            this.bowlFull = false;
            StartCoroutine(this.waitBeforeRefil());
        }, this.emptySpeed));
        
        this.audioSource.Play();
    }

    private void Refill()
    {
        this.ineractionInProgress = true;
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
            this.ineractionInProgress = false;
            this.bowlFull = true;
        }, this.fillSpeed));
    }*/

    /*public IEnumerator waitBeforeRefil()
    {
        /*yield return new WaitForSeconds(5);
        this.Refill();#1#
    }*/

    // Update is called once per frame
    void Update()
    {
        this._lerpProcessor.ProcessLerpPackageList();
    }
}
