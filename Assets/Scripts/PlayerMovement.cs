using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed = 200f;
    private float rotationSpeed = 1300f;
    private Rigidbody rigidbody;

    //MonoBehaviour Method
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        ConfigureMouse();
    }

    private static void ConfigureMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //MonoBehaviour Method
    private void Update()
    {
        SetPlayerYRotationDrivenByMouseInput();
    }

    private void SetPlayerYRotationDrivenByMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * this.rotationSpeed;
        this.rigidbody.MoveRotation(this.rigidbody.rotation * Quaternion.Euler(0, mouseX * Time.deltaTime, 0));
    }

    //MonoBehaviour Method
    private void FixedUpdate()
    {
        SetPlayerVelocityDrivenByInput();
    }

    private void SetPlayerVelocityDrivenByInput()
    {
        float leftRightInput = Input.GetAxis("Horizontal");
        float forwardsBackInput = Input.GetAxis("Vertical");

        Vector3 velocityForward = new Vector3();
        velocityForward = CalculateInputtedForwardVelocity(forwardsBackInput);

        Vector3 velocityRight = CalculateInputtedRightVelocity(leftRightInput);

        this.rigidbody.velocity = velocityForward + velocityRight;
    }


    private Vector3 CalculateInputtedForwardVelocity(float forwardsBackInput)
    {
        return this.walkSpeed * Time.deltaTime * forwardsBackInput * transform.forward;
    }
    private Vector3 CalculateInputtedRightVelocity(float leftRightInput)
    {
        return this.walkSpeed * Time.deltaTime * leftRightInput * transform.right;
    }
}
