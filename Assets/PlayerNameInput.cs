using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    private string name = "";

    private string magicWord = "RESETLEADERBOARD";

    private TMP_InputField inputField;

    public string Name
    {
        get => name;

        set
        {
            if (value.Length >= 3)
            {
                if(value[value.Length -1] != magicWord[value.Length - 1])
                    return;
            }
            this.name = value;
            this.inputField.text = value;
        }
    }

    private void Start()
    {
        this.inputField = FindObjectOfType<TMP_InputField>();
    }
    
    private void Update()
    {
        if (Input.GetAxis("Submit") == 1.0f)
        {
            this.inputField.text = "";
        }

        this.inputField.text = this.name;
    }
}
