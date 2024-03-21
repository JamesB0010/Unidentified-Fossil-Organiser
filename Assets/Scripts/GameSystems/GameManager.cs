using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
    }

    public void AllBonesCollected()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<timeManager>().IsGameOver = true;
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        }
    }
}
