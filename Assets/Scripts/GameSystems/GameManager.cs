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
    public bool resetLeaderboardOnPlay = false;


    public void Start()
    {
        if(leaderboardTexts != null)
        {
            AddToLeaderboard(PlayerPrefs.GetFloat("playerTime"));
            if(resetLeaderboardOnPlay)
            {
                PlayerPrefs.DeleteKey("leaderboardScore1");
                PlayerPrefs.DeleteKey("leaderboardScore2");
                PlayerPrefs.DeleteKey("leaderboardScore3");
            }
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
            PlayerPrefs.GetFloat("leaderboardScore1", 1000),
            PlayerPrefs.GetFloat("leaderboardScore2", 1000),
            PlayerPrefs.GetFloat("leaderboardScore3", 1000)
        };

        fullList.Sort();

        PlayerPrefs.SetFloat("leaderboardScore1", fullList[0]);
        PlayerPrefs.SetFloat("leaderboardScore2", fullList[1]);
        PlayerPrefs.SetFloat("leaderboardScore3", fullList[2]);

        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        leaderboardTexts[1].text = "1. " + PlayerPrefs.GetFloat("leaderboardScore1", 1000).ToString("F2") + "s";
        leaderboardTexts[2].text = "2. " + PlayerPrefs.GetFloat("leaderboardScore2", 1000).ToString("F2") + "s";
        leaderboardTexts[3].text = "3. " + PlayerPrefs.GetFloat("leaderboardScore3", 1000).ToString("F2") + "s";
    }



}
