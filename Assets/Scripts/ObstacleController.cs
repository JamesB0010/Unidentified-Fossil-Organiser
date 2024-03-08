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
            this.packageProcessor.AddPackage(new Vector3LerpPackage<Obstacle>(obstacle.transform.position,obstacle.leftRightAnchors[obstacle.MovingTowards].position, obstacle.Speed, obstacle.gameObject));
            this.packageProcessor.AddPackage(new Vector3LerpPackage<Obstacle>(obstacle.transform.rotation.eulerAngles,obstacle.leftRightAnchors[obstacle.MovingTowards].rotation.eulerAngles, obstacle.Speed, obstacle.gameObject));
        }
    }

    private void AddGameObjectBackToPackageProcessor(LerpData.ObjectLerpPackage<Obstacle> pkg)
    {
        pkg.customComponent.MovingTowards = (pkg.customComponent.MovingTowards + 1) % 2;
        
        
        //finally create a new LerpPackage and add it to the queue
        this.packageProcessor.AddPackage(new Vector3LerpPackage<Obstacle>(pkg.objectToLerp.transform.position,pkg.customComponent.leftRightAnchors[pkg.customComponent.MovingTowards].position, pkg.lerpSpeed, pkg.objectToLerp));
        this.packageProcessor.AddPackage(new Vector3LerpPackage<Obstacle>(pkg.objectToLerp.transform.rotation.eulerAngles,pkg.customComponent.leftRightAnchors[pkg.customComponent.MovingTowards].rotation.eulerAngles, pkg.lerpSpeed, pkg.objectToLerp));
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
