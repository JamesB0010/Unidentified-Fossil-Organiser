using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text[] leaderboardTexts;

    public void Start()
    {
        if(leaderboardTexts != null)
        {
            AddToLeaderboard(5);
        }
    }

    public void AllBonesCollected()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }

    public void AddToLeaderboard(float value)
    {
        List<float> fullList = new List<float>
        {
            value,
            PlayerPrefs.GetFloat("leaderboardScore1"),
            PlayerPrefs.GetFloat("leaderboardScore2"),
            PlayerPrefs.GetFloat("leaderboardScore3")
        };

        fullList.Sort();

        LoadLeaderboard(fullList);
    }

    public void LoadLeaderboard(List<float> scores)
    {
        leaderboardTexts[0].text = "1. " + scores[0] + "s";
        leaderboardTexts[1].text = "2. " + scores[1] + "s";
        leaderboardTexts[2].text = "3. " + scores[2] + "s";
    }



}
