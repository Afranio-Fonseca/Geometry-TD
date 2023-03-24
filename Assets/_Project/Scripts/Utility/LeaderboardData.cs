using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardData : ScriptableObject
{
    public List<LeaderboardEntry> leaderboardData;


    public void AddEntry(LeaderboardEntry entry)
    {
        leaderboardData.Add(entry);
        SortData();
        if (leaderboardData.Count > 5)
        {
            leaderboardData.RemoveAt(5);
        }
    }

    void SortData()
    {
        for(int c = 0; c < leaderboardData.Count; c++)
        {
            for(int i = c + 1; i < leaderboardData.Count; i++)
            {
                if(leaderboardData[c].score < leaderboardData[i].score)
                {
                    LeaderboardEntry aux = leaderboardData[i];
                    leaderboardData[i] = leaderboardData[c];
                    leaderboardData[c] = aux;
                }
            }
        }
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public int score;

        public LeaderboardEntry(string _name, int _score)
        {
            name = _name;
            score = _score;
        }
    }
}
