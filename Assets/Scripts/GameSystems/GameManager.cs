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

    private IEnumerator ExitToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }
    public void AllBonesCollected()
    {
        StartCoroutine(this.ExitToMainMenu());
    }
}
