using System.Collections;
using System.Collections.Generic;
using LerpData;
using UFO_PlayerStuff;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ToiletFlusher : MonoBehaviour, I_Interactable
{

    private AudioSource audioSource;

    private bool bowlFull = true;

    private bool ineractionInProgress = false;

    private float emptySpeed = 0.7f;

    private float fillSpeed = 2f;

    [SerializeField] private GameObject water;

    [SerializeField] private GameObject topPosition;

    [SerializeField] private GameObject bottomPosition;

    [SerializeField] private UnityEvent<string> flush;
    
    public new void HandleInteraction(CameraForwardsSampler sampler)
    {
        //check the player is interacting with me
        if (sampler.InteractableObjectInRangeRef != this.gameObject)
            return;

        if (this.ineractionInProgress)
            return;

        if (this.bowlFull)
        {
            this.Flush();
            this.flush?.Invoke(this.gameObject.name);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Flush()
    {
        this.audioSource.Play();
        this.ineractionInProgress = true;
        
        this.topPosition.transform.position.LerpTo(this.bottomPosition.transform.position,
            emptySpeed,
            value =>
            {
                this.water.transform.position = value;
            },
            pkg =>
            {
                this.bowlFull = false;
                StartCoroutine(this.waitBeforeRefil(pkg));
            }
            );
        
        this.audioSource.Play();
    }

    private void Refill(LerpPackage pkg)
    {
        (pkg.start, pkg.target) = (pkg.target, pkg.start);
        pkg.ResetTiming();
        pkg.timeToLerp = this.fillSpeed;
        pkg.finalCallback = pkg =>
        {
            this.bowlFull = true;
            this.ineractionInProgress = false;
        };
        
        GlobalProcessorHandler.AddLerpPackage(pkg);
    }

    public IEnumerator waitBeforeRefil(LerpPackage pkg)
    {
        yield return new WaitForSeconds(5);
        this.Refill(pkg);
    }
}
