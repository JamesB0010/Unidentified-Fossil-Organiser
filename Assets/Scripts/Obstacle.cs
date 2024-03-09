using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    public Transform[] leftRightAnchors;

    [SerializeField] private float timeToLerp;

    [SerializeField] public AnimationCurve animCurve;

    public float TimeToLerp
    {
        get
        {
            return this.timeToLerp;
        }
    }

    private int movingTowards = 0;

    public int MovingTowards
    {
        get
        {
            return this.movingTowards;
        }
        set
        {
            this.movingTowards = value;
        }
    }
}
