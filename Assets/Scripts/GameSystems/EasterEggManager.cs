using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EasterEggManager : MonoBehaviour
{
    private DoctorsSink[] Sinks;

    private bool shuttersOpen = false;

    private int fullSinkCount = 0;

    public UnityEvent OpenShuttersEvent = new UnityEvent();

    public UnityEvent CloseShuttersEvent = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        this.Sinks = FindObjectsOfType<DoctorsSink>();

        foreach (DoctorsSink sink in this.Sinks)
        {
            sink.OnSinkFull += ReactToSinkFull;
            sink.OnSinkEmpty += ReactToSinkEmpty;

        }
    }

    public void ReactToSinkFull()
    {
        this.fullSinkCount++;
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
}
