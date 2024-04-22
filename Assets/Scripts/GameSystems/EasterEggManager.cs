using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class EasterEggManager : MonoBehaviour
{
    private DoctorsSink[] Sinks;

    private bool shuttersOpen = false;

    public int fullSinkCount = 0;

    public int toiletsFlushed = 0;

    public int sinksFilled = 0;

    public int secretRoomsFound = 0;

    public UnityEvent OpenShuttersEvent = new UnityEvent();

    public UnityEvent CloseShuttersEvent = new UnityEvent();


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += (arg0, scene) =>
        {
            EndScreenUiScript endScreen = FindObjectOfType<EndScreenUiScript>();
            endScreen.setGameStats(this.toiletsFlushed, this.sinksFilled, this.secretRoomsFound);
        };
        this.Sinks = FindObjectsOfType<DoctorsSink>();

        foreach (DoctorsSink sink in this.Sinks)
        {
            sink.OnSinkFull += ReactToSinkFull;
            sink.OnSinkEmpty += ReactToSinkEmpty;

        }
    }

    public void reactToToiletFlushed()
    {
        this.toiletsFlushed = this.toiletsFlushed < 5 ? this.toiletsFlushed + 1 : 5;
    }

    public void ReactToSinkFull()
    {
        this.fullSinkCount++;
        this.sinksFilled = this.sinksFilled < 3? sinksFilled + 1: 3;
        if(this.fullSinkCount >= this.Sinks.Length)
            this.AllSinksFull();
    }

    public void ReactToSinkEmpty()
    {
        this.fullSinkCount--;
        if (this.shuttersOpen)
            this.CloseShutters();
    }

    private void AllSinksFull()
    {
        this.OpenShuttersEvent.Invoke();
        this.shuttersOpen = true;
    }

    private void CloseShutters()
    {
        this.shuttersOpen = false;
        this.CloseShuttersEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.secretRoomsFound = 1;

        GetComponent<BoxCollider>().enabled = false;
    }
}
