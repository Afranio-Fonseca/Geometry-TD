using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    [SerializeField] LeaderboardData leaderboardData;
    [SerializeField] TextMeshProUGUI playerScores;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TMP_InputField namePrompt;
    [SerializeField] Button submitButton;
    [SerializeField] Button retryButton;

    int finalScore;

    public void OpenLeaderboards(int _finalScore)
    {
        finalScore = _finalScore;
        currentScoreText.text = "Your score: " + finalScore;
        PrintLeaderboard();
        if(leaderboardData.leaderboardData.Count < 5 || finalScore > leaderboardData.leaderboardData[4].score)
        {
            submitButton.gameObject.SetActive(true);
            namePrompt.gameObject.SetActive(true);
            submitButton.onClick.AddListener(SubmitScore);
        } else
        {
            retryButton.gameObject.SetActive(true);
            retryButton.onClick.AddListener(StartRestart);
        }
    }

    private void PrintLeaderboard()
    {
        string text = "";
        foreach(LeaderboardData.LeaderboardEntry entry in leaderboardData.leaderboardData)
        {
            text += entry.name + " - " + entry.score + "\n";
        }
        playerScores.text = text;
    }

    void EnableRetryButton()
    {
        retryButton.gameObject.SetActive(true);
        retryButton.onClick.AddListener(StartRestart);
    }

    public void SubmitScore()
    {
        leaderboardData.AddEntry(new LeaderboardData.LeaderboardEntry(namePrompt.text, finalScore));
        namePrompt.gameObject.SetActive(false);
        submitButton.onClick.RemoveAllListeners();
        submitButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        retryButton.onClick.AddListener(StartRestart);
        PrintLeaderboard();
    }

    void StartRestart()
    {
        GameManager.instance.RestartGame();
    }

}
