using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class leaderboardscript : MonoBehaviour
{
    [SerializeField]
    private GameObject leaderboardEntryPrefab;

    [SerializeField]
    private Transform contentContainer;
    
    [SerializeField]
    private ScrollRect scrollRect;

    private void ClearLeaderboard()
    {
        // Destroy all existing entry objects
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void loadLeaderboard(){
        ClearLeaderboard();
        NetworkManager.Instance.GetLeaderboard((response) => {
            if (response.done)
            {
                response.leaderboard.Sort((a, b) => a.Ranking.CompareTo(b.Ranking));

                for (int i = 0; i < response.leaderboard.Count; ++i)
                {
                    GameObject entryObject = Instantiate(leaderboardEntryPrefab, contentContainer);
                    TextMeshProUGUI rankingText = entryObject.transform.Find("Ranking").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI usernameText = entryObject.transform.Find("Username").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI scoreText = entryObject.transform.Find("TotalScore").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI timeText = entryObject.transform.Find("TotalTime").GetComponent<TextMeshProUGUI>();

                    rankingText.text = response.leaderboard[i].Ranking.ToString();
                    usernameText.text = response.leaderboard[i].Username;
                    scoreText.text = response.leaderboard[i].TotalScore.ToString();
                    timeText.text = response.leaderboard[i].TotalTime.ToString();
                }
            }
            else
            {
                Debug.Log("Error loading leaderboard: " + response.message);
            }
        });
    }

    private void Start()
    {
        Invoke("loadLeaderboard", 0.1f);
    }
}
