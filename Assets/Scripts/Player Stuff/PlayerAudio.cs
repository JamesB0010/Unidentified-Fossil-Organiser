using System;
using UnityEngine;
using System.Collections.Generic;

namespace UFO_PlayerStuff
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudio : MonoBehaviour
    {
        public enum PlayOverideModes
        {
            NO_OVERIDE,
            OVERRIDE
        };
        protected AudioSource source;

        [SerializeField] 
        protected List<AudioClip> audioClips = new List<AudioClip>();

        protected Dictionary<string, AudioClip> clipNames = new Dictionary<string, AudioClip>();

        public void Awake()
        {
            this.source = GetComponent<AudioSource>();

            foreach (AudioClip clip in audioClips)
            {
                clipNames.Add(clip.name, clip);
            }
        }

        /// <summary>
        /// This function will play a sound at the given index of its audioClips list
        /// it will throw an argument out of range exception if the index given is negative
        /// or the same size as or bigger than the list of audio clips on the player audio obeject
        /// </summary>
        /// <param name="index">The index of the sound to be played</param>
        public void PlaySound(int index, PlayOverideModes overideMode = PlayOverideModes.NO_OVERIDE)
        {
            bool notEligible = this.source.isPlaying && overideMode == PlayOverideModes.NO_OVERIDE;
            if (notEligible)
                return;
            bool invalidIndex = index < 0 || index >= this.audioClips.Count;
            if (invalidIndex)
                Debug.LogError("Sound requested invalid. " +
                               "This may be because the index given is negative " +
                               "or out of bounds of the list of sounds on the player audio object");
            
            this.source.clip = audioClips[index];
            source.Play();
        }
        

        public void PlaySound(string soundName, PlayOverideModes overideMode = PlayOverideModes.NO_OVERIDE)
        {
            bool notEligible = this.source.isPlaying && overideMode == PlayOverideModes.NO_OVERIDE;
            if (notEligible)
                return;
            try
            {
                this.source.clip = this.clipNames[soundName];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("Sound requested invalid. " +
                "This is because the name given was not found in the player audio object");
            }
            source.Play();
        }
    }
}
