using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CategoryBtnScript : MonoBehaviour
{
    [SerializeField] private Text categoryTitleText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button btn;

    public Button Btn { get => btn; }

    public void SetButton(string title, int totalQuestion)
    {
        categoryTitleText.text = title;
        int playerScore = PlayerPrefs.GetInt(title, 0);

        if (playerScore == 0)
        {
            scoreText.text = "-";
        }
        else
        {
            scoreText.text = playerScore + "/" + totalQuestion;
        }
    }
}
