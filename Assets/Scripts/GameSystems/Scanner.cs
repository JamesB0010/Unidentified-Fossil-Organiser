using System.Collections;
using System.Collections.Generic;
using UFO_PickupStuff;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private List<Vector3> bonesLocations = new List<Vector3>();

    [SerializeField] private float MaxRed;

    [SerializeField] private float MaxGreen;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Bone bone in FindObjectsOfType<Bone>())
        {
            bonesLocations.Add(bone.gameObject.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float closestMagnitude = float.MaxValue;
        for(int i = 0; i < this.bonesLocations.Count; i++)
        {
            float currentMagnitude = (bonesLocations[i] - gameObject.transform.position).magnitude;
            if (currentMagnitude < closestMagnitude)
            {
                closestMagnitude = currentMagnitude;
            }
        }
    }
}
