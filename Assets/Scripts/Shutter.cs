using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private BoxCollider BoxCollider;

    private void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void OpenShutters()
    {
        if (this.animator == null)
            return;
        
        this.animator.SetTrigger("Open");
        this.BoxCollider.enabled = false;
    }

    public void CloseShutters()
    {
        if (this.animator == null)
            return;
        
        this.animator.SetTrigger("Close");
        this.BoxCollider.enabled = true;
    }
}
