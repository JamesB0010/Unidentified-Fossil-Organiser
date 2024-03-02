using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

//Generated By Chat GPT
//Refactored By James
public class PlayerHeadVerticalMovement : MonoBehaviour
{
    public float rotationSensitivity = 5f;
    public float minYRotation = -80f; 
    public float maxYRotation = 80f;

    [SerializeField]
    private UnityEvent lookingUp = new UnityEvent();

    [SerializeField]
    private UnityEvent lookingDown = new UnityEvent();

    private float rotationY = 0f;
    
    void Update()
    { 
        this.RotateYWitinBounds(this.GetMouseYInput());
    }

    private float GetMouseYInput()
    {
        return Input.GetAxis("Mouse Y") * rotationSensitivity;
    }

    private void RotateYWitinBounds(float rotationAmount)
    {
        this.SetRotationYWithinBounds(rotationAmount);

        // Apply rotation to the object's local rotation
        transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }

    /// <summary>
    /// This method sets the rotationY variable and makes sure that it is between a predefined
    /// max and min value
    /// </summary>
    /// <param name="rotationAmount">The rotation amount to be subtracted from rotationY</param>
    private void SetRotationYWithinBounds(float rotationAmount)
    {
        this.rotationY -= rotationAmount;

        if (this.rotationY <= 50)
        {
            lookingUp.Invoke();
        }
        else
        {
            lookingDown.Invoke();
        }

        // Clamp rotationX to the specified range
        this.rotationY = Mathf.Clamp(rotationY, minYRotation, maxYRotation);
    }
}
