using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    public Transform[] leftRightAnchors;

    [SerializeField] private float speed;

    public float Speed
    {
        get
        {
            return this.speed;
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
