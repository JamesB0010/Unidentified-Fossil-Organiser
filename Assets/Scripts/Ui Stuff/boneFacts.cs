using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneFacts : MonoBehaviour
{
    public string boneFact;
    public AudioClip aiVoice;
    private AudioSource aiSource;
    private bool isClipPlayed = false;

    public AudioSource Source
    {
        get
        {
            return aiSource;
        }

        set
        {
           aiSource = value;
        }
    }

    public bool isPlayed
    {
        get
        {
            return isClipPlayed;
        }

        set
        {
            isClipPlayed = value;
        }
    }

    void Start()
    {
        aiSource = GameObject.FindGameObjectWithTag("BonesHolder").GetComponent<AudioSource>();


        if(boneFact == null || boneFact == "")
        {
            boneFact = "No Fact Available";
        }
        
    }
}
