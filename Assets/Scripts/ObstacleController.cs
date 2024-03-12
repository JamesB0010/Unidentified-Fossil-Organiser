using System;
using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;
using Random = UnityEngine.Random;


public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private Light light1;

    private float test = 0;
    
    [SerializeField] private Light light2;
    // Start is called before the first frame update
    void Start()
    {
        light1.intensity.LerpTo(light1.intensity * 50, 5, (value) =>
            {
                light1.intensity = value;
            },
            pkg =>
            {
                (pkg.start, pkg.target) = (pkg.target, pkg.start);
                pkg.elapsedTime = 0;
                GlobalProcessorHandler.AddLerpPackage(pkg);
            });
            

        
        GlobalProcessorHandler.AddLerpPackage(
            new Vector3LerpPackage(
                new Vector3(light2.color.r, light2.color.g, light2.color.b), new Vector3(Random.Range(0,255), Random.Range(0,255), Random.Range(0,255)),
                (val) =>
                {
                    Color color = new Color(val.x, val.y, val.z);
                    light2.color = color;
                },
                pkg =>
                {
                    (pkg.start, pkg.target) = (pkg.target, pkg.start);
                    pkg.current = 0.0f;
                    pkg.elapsedTime = 0;
                    pkg.target = new Vector3(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
                    GlobalProcessorHandler.AddLerpPackage(pkg);
                },
                2f
            )
        );
    }

    private void Update()
    {
        Debug.Log(this.test);
    }
}
