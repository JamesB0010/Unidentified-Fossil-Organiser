using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UFO_PlayerStuff;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class GravGunSpline : MonoBehaviour
{
    [FormerlySerializedAs("MySpline")] [FormerlySerializedAs("spline")] [SerializeField] private SplineContainer SplineContainer;

    private Spline spline;

    [SerializeField] private CameraForwardsSampler forwardsSampler;

    private bool objectIsPickedUp = false;

    [SerializeField] private Transform targetTransform;

    private SplineExtrude spineExtrudeComp;

    private Transform lastHeldObjectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        this.spineExtrudeComp = this.SplineContainer.gameObject.GetComponent<SplineExtrude>();
        this.spineExtrudeComp.Range = new Vector2(0, 0);
        this.spineExtrudeComp.Capped = false;
        this.spline = this.SplineContainer.Spline;
    }


    private void Update(){
        
        var firstKnot = spline.ToArray()[0];
        

        firstKnot.Position = this.SplineContainer.transform.InverseTransformPoint(targetTransform.position);
        
        spline.SetKnot(0, firstKnot);

        if (this.objectIsPickedUp)
        {
            var thirdKnot = spline.ToArray()[2];
            var secondKnot = spline.ToArray()[1];
            secondKnot.Position = (firstKnot.Position + thirdKnot.Position) / 2;
            secondKnot.Position.y -= 0.1f;
            secondKnot.Position.x -= 0.1f;
            spline.SetKnot(1, secondKnot);

            
            thirdKnot.Position =
                this.SplineContainer.transform.InverseTransformPoint(this.forwardsSampler.ObjectInRange.transform
                    .position);

            //thirdKnot.Position.y = firstKnot.Position.y;
            
            
            
            spline.SetKnot(2, thirdKnot);
        }
    }
    
    public void OnPickup()
    {
        this.objectIsPickedUp = true;
        this.lastHeldObjectTransform = this.forwardsSampler.ObjectInRange.transform;

        this.spineExtrudeComp.Capped = true;
        0.0f.LerpTo(1, 0.3f, value =>
        {
            this.spineExtrudeComp.Range = new Vector2(0, value);
        });
    }

    public void OnDrop()
    {
        this.objectIsPickedUp = false;
        
        
        var thirdKnot = spline.ToArray()[2];
        
        0.0f.LerpTo(1.0f, 0.3f, value =>
        {
            this.spineExtrudeComp.Range = new Vector2(value, 1);
            
            thirdKnot.Position =
                this.SplineContainer.transform.InverseTransformPoint(this.lastHeldObjectTransform.position);
            
            spline.SetKnot(2, thirdKnot);
        }, 
            pkg =>
            {
                this.spineExtrudeComp.Capped = false;
            });
    }
}
