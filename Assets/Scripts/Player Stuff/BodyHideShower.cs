using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHideShower : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer bodyMeshRenderer;
    
    public void ReactToLookingUp()
    {
        this.bodyMeshRenderer.enabled = false;
    }

    public void ReactToLookingDown()
    {
        this.bodyMeshRenderer.enabled = true;
    }
}
