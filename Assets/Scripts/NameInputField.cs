using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour
{
    [SerializeField] private GameObject inputField;

    [SerializeField] private GameObject PlayButton;

    [SerializeField] private GameObject CreditsButton;

    private EventSystem eventSystem;

    private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        this.eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("VerticalMenu");
        if (EventSystem.current.currentSelectedGameObject == inputField)
        {
            //move off input field
            if (verticalInput < -0.2f)
            {
                this.eventSystem.SetSelectedGameObject(this.PlayButton);
            }

            if (Input.GetAxis("Submit") == 1.0f)
            {
                inputField.transform.position.LerpTo(new Vector3(960, -100 + 1080, 0), 0.6f, value =>
                {
                    inputField.transform.position = value;
                }, pkg =>
                {
                    
                },
                    this.animCurve);
                Debug.Log("start typing");
            }
        }
    }
}
