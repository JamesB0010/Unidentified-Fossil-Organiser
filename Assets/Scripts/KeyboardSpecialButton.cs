using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardSpecialButton : KeyboardButton
{
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject CreditsButton;
    private Vector3 originalStartButtonPos;
    private Vector3 originalCreditsButtonPos;


    [SerializeField] private GameObject KeyboardContainer;
    private Vector3 KeyboardContainerOriginalPos;

    private Vector3 originalInputFieldLocation;

    private void Start()
    {
        this.playerNameInput = FindObjectOfType<PlayerNameInput>();
        this.character = " ".ToCharArray()[0];
        
        originalStartButtonPos = new Vector3(StartButton.transform.position.x, StartButton.transform.position.y, StartButton.transform.position.z );
        originalCreditsButtonPos = new Vector3(CreditsButton.transform.position.x, CreditsButton.transform.position.y, CreditsButton.transform.position.z);
        this.KeyboardContainerOriginalPos = new Vector3(this.KeyboardContainer.transform.position.x,
            this.KeyboardContainer.transform.position.y, this.KeyboardContainer.transform.position.z);

        this.originalInputFieldLocation = new Vector3(this.inputField.transform.position.x,
            this.inputField.transform.position.y, this.inputField.transform.position.z);
    }

    public void Enter()
    {
        Debug.Log("Enter");
        this.KeyboardContainer.transform.position.LerpTo(this.KeyboardContainerOriginalPos, 0.8f, value =>
        {
            this.KeyboardContainer.transform.position = value;
        },
            pkg =>
            {
                this.inputField.transform.position.LerpTo(this.originalInputFieldLocation, 0.6f, value =>
                {
                    this.inputField.transform.position = value;
                }, pkg =>
                {
                    StartButton.transform.position.LerpTo(originalStartButtonPos, 0.6f, value =>
                    {
                        StartButton.transform.position = value;
                    });
        
                    CreditsButton.transform.position.LerpTo(originalCreditsButtonPos, 0.6f, value =>
                    {
                        CreditsButton.transform.position = value;
                    }, pkg =>
                    {
                        EventSystem.current.SetSelectedGameObject(this.StartButton);
                    }); 
                });
            });
    }

    public void Delete()
    {
        int stringLength = this.playerNameInput.Name.Length;
        if (stringLength > 1)
        {
            
            this.playerNameInput.Name = this.playerNameInput.Name.Substring(0, stringLength - 1);
        }
        else
        {
            this.playerNameInput.Name = "";
        }
    }

}
