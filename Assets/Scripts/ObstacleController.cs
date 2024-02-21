using System.Collections;
using System.Collections.Generic;
using LerpData;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private LerpPackageProcessor<Obstacle> packageProcessor = new LerpPackageProcessor<Obstacle>();
    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            PositionRotationPair start = new PositionRotationPair
            {
                position = obstacle.transform.position,
                rotation = obstacle.transform.rotation.eulerAngles
            };

            //define a end/target position and rotation
            PositionRotationPair end = new PositionRotationPair()
            {
                position = obstacle.leftRightAnchors[obstacle.MovingTowards].position,
                rotation = obstacle.leftRightAnchors[obstacle.MovingTowards].rotation.eulerAngles
            };
        
            //finally create a new LerpPackage and add it to the queue
            this.packageProcessor.AddPackage(new ObjectLerpPackage<Obstacle>(obstacle.gameObject, start, end, obstacle.Speed));
        }
    }

    private void AddGameObjectBackToPackageProcessor(LerpData.ObjectLerpPackage<Obstacle> pkg)
    {
        pkg.customComponent.MovingTowards = (pkg.customComponent.MovingTowards + 1) % 2;
            
        PositionRotationPair start = new PositionRotationPair
        {
            position = pkg.objectToLerp.transform.position,
            rotation = pkg.objectToLerp.transform.rotation.eulerAngles
        };

        //define a end/target position and rotation
        PositionRotationPair end = new PositionRotationPair()
        {
            position = pkg.customComponent.leftRightAnchors[pkg.customComponent.MovingTowards].position,
            rotation = pkg.customComponent.leftRightAnchors[pkg.customComponent.MovingTowards].rotation.eulerAngles
        };
        
        //finally create a new LerpPackage and add it to the queue
        this.packageProcessor.AddPackage(new ObjectLerpPackage<Obstacle>(pkg.objectToLerp, start, end, pkg.customComponent.Speed));
    }

    // Update is called once per frame
    void Update()
    {
        this.packageProcessor.ProcessLerpPackageList(pkg =>
        {
            this.AddGameObjectBackToPackageProcessor(pkg);
        });
    }
}
