using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    public Transform[] leftRightAnchors;

    [SerializeField] private float timeToLerp;

    [SerializeField] public AnimationCurve animCurve;

    [SerializeField]
    private float pauseTimeBetweenLerps = 0;
    
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

    
    public IEnumerator PingPongPackageAfterSeconds(LerpPackage pkg, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        (pkg.start, pkg.target) = (pkg.target, pkg.start);
        pkg.ResetTiming();
        GlobalProcessorHandler.AddLerpPackage(pkg);
    }
    private void Start()
    {
        this.transform.position.LerpTo(this.leftRightAnchors[this.MovingTowards].position, this.TimeToLerp,
            (Vector3 value) =>
            {
                this.transform.position = value;
            },
            pkg =>
            {
                StartCoroutine(PingPongPackageAfterSeconds(pkg, this.pauseTimeBetweenLerps));
            },
            this.animCurve
        );
    }
    
}
