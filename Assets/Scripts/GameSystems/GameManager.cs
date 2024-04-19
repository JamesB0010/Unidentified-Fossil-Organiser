using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    IEnumerator EndGameAfter4Mins()
    {
        yield return new WaitForSeconds(240);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }
    public void Start()
    {
        StartCoroutine(EndGameAfter4Mins());
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
