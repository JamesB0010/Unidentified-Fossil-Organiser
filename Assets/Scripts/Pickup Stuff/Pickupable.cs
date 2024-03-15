using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO_PickupStuff
{
public abstract class Pickupable: MonoBehaviour
{
    private void Start()
    {
        //yield return new WaitForSeconds(0);
        this.pickupEffect.Stop();
        this.pickupEffect.enableEmission = false;
    }

    [SerializeField] private ParticleSystem pickupEffect;
    public void PickedUp()
    {
        this.pickupEffect.enableEmission = true;
        this.pickupEffect.Play();
    }

    public void Dropped()
    {
        this.pickupEffect.enableEmission = false;
        this.pickupEffect.Stop();
        this.pickupEffect.Clear();
    }
}
    
}
