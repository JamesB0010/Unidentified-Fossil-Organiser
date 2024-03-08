using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private LerpPackageProcessor<Obstacle> obstaclePackageProcessor = new LerpPackageProcessor<Obstacle>();

    private LerpPackageProcessor<Light> lightPackageProccessor = new LerpPackageProcessor<Light>();
    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            this.obstaclePackageProcessor.AddPackage(new Vector3LerpPackage<Obstacle>(obstacle.transform.position,
                obstacle.leftRightAnchors[obstacle.MovingTowards].position,
                (val, obj) => { obj.gameObject.transform.position = new Vector3(val.x, val.y, val.z); },
                pkg => { PingPongObstacle(pkg); },
                obstacle.gameObject,
                obstacle.Speed));
            
            this.obstaclePackageProcessor.AddPackage(new Vector3LerpPackage<Obstacle>(
                obstacle.transform.rotation.eulerAngles, new Vector3(Random.Range(0, 180),Random.Range(0, 180), Random.Range(0, 180)),
                (value, component) =>
                {
                    component.gameObject.transform.rotation = Quaternion.Euler(value);
                },
                pkg =>
                {
                    PingPongObstacle(pkg);
                },
                obstacle.gameObject
                ));
        }

        foreach (var light in FindObjectsOfType<Light>())
        {
            this.lightPackageProccessor.AddPackage(
                new FloatLerpPackage<Light>(
                    light.intensity, light.intensity * 20,
                    (val, obj) =>
                    {
                        obj.intensity = val;
                    },
                    pkg =>
                    {
                        (pkg.start, pkg.target) = (pkg.target, pkg.start);
                        pkg.current = 0.0f;
                        this.lightPackageProccessor.AddPackage(pkg);
                    },
                    light.gameObject,
                    0.3f
                    )
                );
        }
    }


    private void PingPongObstacle(LerpData.ObjectLerpPackage<Obstacle> pkg)

    {

        pkg.customComponent.MovingTowards = (pkg.customComponent.MovingTowards + 1) % 2;

        //swap start and end 
        (pkg.start, pkg.target) = (pkg.target, pkg.start);
        pkg.current = 0.0f;

        //finally create a new LerpPackage and add it to the queue

        this.obstaclePackageProcessor.AddPackage(pkg);
    }


    // Update is called once per frame
    void Update()
    {
        this.obstaclePackageProcessor.ProcessLerpPackageList();
        this.lightPackageProccessor.ProcessLerpPackageList();
    }
}
