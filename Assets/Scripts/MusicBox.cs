using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MusicBox : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    private AudioSource audioSource;

    private int currentClip;

    private static MusicBox musicBoxReference = null;

    public float Volume
    {
        set
        {
            this.audioSource.volume = value;
        }
        get
        {
            return this.audioSource.volume;
        }
    }

    private IEnumerator PlayNextSongAfterThisOne(float lengthOfCurrentSong)
    {
        yield return new WaitForSeconds(lengthOfCurrentSong);
        this.currentClip = (this.currentClip + 1) % 2;
        this.PlayNewSong();

    }
    // Start is called before the first frame update
    void Awake()
    {
        if (MusicBox.musicBoxReference == null)
            MusicBox.musicBoxReference = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Start()
    {
        this.audioSource = gameObject.GetComponent<AudioSource>();
        //select a random song
        this.currentClip = (int)Mathf.Floor(UnityEngine.Random.Range(0, 2));

        this.audioSource.clip = this.audioClips[this.currentClip]; ;

        this.PlayNewSong();
    }

    void PlayNewSong()
    {
        this.audioSource.Play();
        StartCoroutine(this.PlayNextSongAfterThisOne(this.audioClips[this.currentClip].length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
