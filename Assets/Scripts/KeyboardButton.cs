using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour
{
    private char character;

    [SerializeField] protected TMP_InputField inputField;


    private void Awake()
    {
        this.character = this.gameObject.name[0];
        GetComponent<Button>().onClick.AddListener(this.OnPressed);
    }

    public void OnPressed()
    {
        this.inputField.text += this.character;
    }
    
}
