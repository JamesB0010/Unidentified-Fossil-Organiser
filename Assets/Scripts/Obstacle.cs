using System;
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

    private Vector4 test = new Vector4();

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

    private void Start()
    {
        this.test.LerpTo(new Vector4(10, 10, 10, 10), 5);
    }

    private void Update()
    {
        Debug.Log(this.test);
    }
}
