using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class canvasManager : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public TMP_Text[] leaderBoardScores; 

    // Start is called before the first frame update
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "EndScreen")
        {
            if (PlayerPrefs.GetString("PlayerCurrentName") == "ResetLeaderboard")
            {
                PlayerPrefs.DeleteAll();
            }
            else
            {
                finalScoreText.text = "Congratutions " + PlayerPrefs.GetString("PlayerCurrentName") + "! You succesfully fixed the skeleton in " + PlayerPrefs.GetFloat("playerTime").ToString("F") + " seconds";
                SaveLeaderBoard();
            }
        }
    }

    private void SaveLeaderBoard()
    {
        PlayerPrefs.SetString("P5Stats", PlayerPrefs.GetString("PlayerCurrentName") + ", " + PlayerPrefs.GetFloat("playerTime").ToString("F"));

        LoadLeaderBoard();
    }

    private void LoadLeaderBoard()
    {
        string p1Stats = PlayerPrefs.GetString("P1Stats", "XX, 999.99");
        string p2Stats = PlayerPrefs.GetString("P2Stats", "XX, 999.99");
        string p3Stats = PlayerPrefs.GetString("P3Stats", "XX, 999.99");
        string p4Stats = PlayerPrefs.GetString("P4Stats", "XX, 999.99");
        string p5Stats = PlayerPrefs.GetString("P5Stats", "XX, 999.99");

        string[] inputStrings = {p1Stats, p2Stats, p3Stats, p4Stats, p5Stats};
        List<string> sortedScores = SortScores(inputStrings);

        PlayerPrefs.SetString("P1Stats", sortedScores[0]);
        PlayerPrefs.SetString("P2Stats", sortedScores[1]);
        PlayerPrefs.SetString("P3Stats", sortedScores[2]);
        PlayerPrefs.SetString("P4Stats", sortedScores[3]);
        PlayerPrefs.SetString("P5Stats", sortedScores[4]);

        leaderBoardScores[0].text = "1st - " + sortedScores[0] + " seconds";
        leaderBoardScores[1].text = "2nd - " + sortedScores[1] + " seconds";
        leaderBoardScores[2].text = "3rd - " + sortedScores[2] + " seconds";
        leaderBoardScores[3].text = "4th - " + sortedScores[3] + " seconds";

    }


    static List<string> SortScores(string[] inputStrings)
    {
        // Parse input strings into tuples of name and score
        List<(string name, float score)> scores = new List<(string, float)>();
        foreach (string input in inputStrings)
        {
            string[] parts = input.Split(',');
            string name = parts[0].Trim();
            float score = float.Parse(parts[1].Trim());
            Debug.Log(score);
            scores.Add((name, score));
        }

        // Sort by score (low to high)
        scores.Sort((a, b) => a.score.CompareTo(b.score));

        // Convert sorted tuples back to strings
        List<string> sortedScores = scores.Select(score => $"{score.name}, {score.score}").ToList();

        return sortedScores;
    }
}

