using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace  UFO{
    
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed = 5f;
    private float rotationSpeed = 1300f;
    private Rigidbody rigidbody;

    private AudioSource footstepSource;

    [SerializeField] 
    private UnityEvent StartedWalking = new UnityEvent();

    [SerializeField] 
    private UnityEvent StoppedWalking = new UnityEvent();
    
    private bool isWalking = false;
    private bool IsWalking
    {
        set
        {
            //if nothing has changed then oh well
            if (isWalking == value)
                return;
            
            //stopped walking
            if (value == false)
            {
                this.footstepSource.Stop();
                this.StoppedWalking.Invoke();
                isWalking = value;
            }

            if (value == true)
            {
                this.footstepSource.Play();
                this.StartedWalking.Invoke();
                isWalking = value;
            }
        }
    }

    //MonoBehaviour Method
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        ConfigureMouse();

        AudioSource source = gameObject.GetComponent<AudioSource>();
        if (!source)
        {
            this.footstepSource = new AudioSource();
        }
        else
        {
            this.footstepSource = source;
        }
    }

    private static void ConfigureMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        SetPlayerVelocityDrivenByInput();
        SetPlayerYRotationDrivenByMouseInput();
    }

    private void SetPlayerYRotationDrivenByMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * this.rotationSpeed;
        this.rigidbody.MoveRotation(this.rigidbody.rotation * Quaternion.Euler(0, mouseX * Time.deltaTime, 0));
    }
    

    private void SetPlayerVelocityDrivenByInput()
    {
        float leftRightInput = Input.GetAxis("Horizontal");
        float forwardsBackInput = Input.GetAxis("Vertical");

        Vector3 velocityForward = new Vector3();
        velocityForward = CalculateInputtedForwardVelocity(forwardsBackInput);

        Vector3 velocityRight = CalculateInputtedRightVelocity(leftRightInput);

        this.IsWalking = (velocityForward + velocityRight).magnitude > 0 ? true : false;

        this.rigidbody.velocity = velocityForward + velocityRight;
    }


    private Vector3 CalculateInputtedForwardVelocity(float forwardsBackInput)
    {
        return this.walkSpeed * forwardsBackInput * transform.forward;
    }
    private Vector3 CalculateInputtedRightVelocity(float leftRightInput)
    {
        return this.walkSpeed * leftRightInput * transform.right;
    }
}
}
