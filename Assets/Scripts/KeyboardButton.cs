using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour
{
    public char character;

    [SerializeField] protected TMP_InputField inputField;

    protected PlayerNameInput playerNameInput;

    [SerializeField] protected bool Normalkey = true;


    private void Awake()
    {
        this.character = this.gameObject.name[0];
        if(this.Normalkey)
            GetComponent<Button>().onClick.AddListener(this.OnPressed);
        this.playerNameInput = FindObjectOfType<PlayerNameInput>();
    }

    public void OnPressed()
    {
        if (this.Normalkey)
        {
            this.playerNameInput.Name += this.character;
        }
    }
    
}
