using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit for headbob https://www.youtube.com/watch?v=5MbR2qJK8Tc for tutorial
//adapted by James
public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    private bool walking = false;
    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.002f;
    [SerializeField, Range(0, 30)] private float frequency = 10.0f;
    [SerializeField] private float sideBobAmplitude = 0.2f;

    private bool resetCamera = false;
    [SerializeField] private Transform camera = null;
    [SerializeField] private Transform cameraHolder = null;
    
    private Vector3 startPos;

    private void Awake()
    {
        this.startPos = this.camera.localPosition;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * this.frequency) * this.amplitude;
        pos.x += Mathf.Sin(Time.time * this.frequency / 2) * this.amplitude * 0.2f;
        return pos;
    }

    private void ResetPosition()
    {
        if (this.camera.localPosition == this.startPos)
        {
            this.resetCamera = false;
            return;
        }
        this.camera.localPosition = Vector3.Lerp(this.camera.localPosition, this.startPos, 3 * Time.deltaTime);
    }

    public void StartWalking()
    {
        this.walking = true;
        this.resetCamera = false;
    }

    public void StopWalking()
    {
        this.walking = false;
        this.resetCamera = true;
        this.ResetPosition();
    }

    private void PlayMotion(Vector3 motion)
    {
        this.camera.localPosition += motion;
    }
    private void Update()
    {
        if (!this.enable)
            return;

        if(this.resetCamera)
            ResetPosition();
        
        if (!this.walking)
            return;
        
        this.PlayMotion(this.FootStepMotion());
    }
}
