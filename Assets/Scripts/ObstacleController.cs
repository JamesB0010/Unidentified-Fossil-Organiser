using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            GlobalProcessorHandler.AddLerpPackage(new Vector3LerpPackage(
                obstacle.transform.rotation.eulerAngles, new Vector3(Random.Range(0, 180),Random.Range(0, 180), Random.Range(0, 180)),
                (value) =>
                {
                    obstacle.gameObject.transform.parent.transform.rotation = Quaternion.Euler(value);
                },
                pkg =>
                {
                    //swap start and end 
                    (pkg.start, pkg.target) = (pkg.target, pkg.start);
                    pkg.current = 0.0f;
                    pkg.elapsedTime = 0.0f;
                    pkg.target = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

                    //finally create a new LerpPackage and add it to the queue

                    GlobalProcessorHandler.AddLerpPackage(pkg);
                },
                2
                ));
            
            obstacle.transform.position.LerpTo(obstacle.leftRightAnchors[obstacle.MovingTowards].position, obstacle.TimeToLerp,
                (value) =>
                {
                    obstacle.transform.position = value;
                },
                pkg =>
                {
                    (pkg.start, pkg.target) = (pkg.target, pkg.start);
                    pkg.elapsedTime = 0;
                    GlobalProcessorHandler.AddLerpPackage(pkg);
                }
            );
            
        }

        foreach (var light in FindObjectsOfType<Light>())
        {
            light.intensity.LerpTo(light.intensity * 2, 5, (value) =>
            {
                light.intensity = value;
            },
                pkg =>
                {
                    (pkg.start, pkg.target) = (pkg.target, pkg.start);
                    pkg.elapsedTime = 0;
                    GlobalProcessorHandler.AddLerpPackage(pkg);
                });
           
            GlobalProcessorHandler.AddLerpPackage(
                new Vector3LerpPackage(
                    new Vector3(light.color.r, light.color.g, light.color.b), new Vector3(Random.Range(0,255), Random.Range(0,255), Random.Range(0,255)),
                    (val) =>
                    {
                        Color color = new Color(val.x, val.y, val.z);
                        light.color = color;
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
    }
}
