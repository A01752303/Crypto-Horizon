using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizGameUI quizGameUI;                // Referencia a la UI
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    [SerializeField] private float timeInSeconds;
#pragma warning restore 649

    private string currentCategory = "";
    private int correctAnswerCount = 0;
    private List<Question> questions;
    private Question selectedQuetion = new Question();
    private int gameScore;
    private int lifesRemaining;
    private float currentTime;
    private QuizDataScriptable dataScriptable;
    private GameStatus gameStatus = GameStatus.NEXT;
    public GameStatus GameStatus { get { return gameStatus; } }
    public List<QuizDataScriptable> QuizData { get => quizDataList; }

    private int totalQuestions;              // NUEVO: total de preguntas
    private int currentQuestionNumber;       // NUEVO: número actual de pregunta

    public void StartGame(int categoryIndex, string category)
    {
        currentCategory = category;
        correctAnswerCount = 0;
        gameScore = 0;
        lifesRemaining = 3;
        currentTime = timeInSeconds;
        currentQuestionNumber = 0; // NUEVO: reiniciar número actual de pregunta

        questions = new List<Question>();
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);

        totalQuestions = questions.Count; // NUEVO: establecer total de preguntas

        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }

    private void SelectQuestion()
    {
        if (questions.Count == 0) return;

        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuetion = questions[val];
        questions.RemoveAt(val);

        currentQuestionNumber++; // NUEVO: incrementar el contador de pregunta actual

        quizGameUI.SetQuestion(selectedQuetion);

        // NUEVO: actualizar el texto del contador de preguntas
        quizGameUI.QuestionCounterText.text = $"{currentQuestionNumber}/{totalQuestions}";
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);                      
        quizGameUI.TimerText.text = time.ToString("mm':'ss");   
        if (currentTime <= 0)
        {
            GameEnd();
        }
    }

    public bool Answer(string selectedOption)
    {
        bool correct = false;
        if (selectedQuetion.correctAns == selectedOption)
        {
            correctAnswerCount++;
            correct = true;
            gameScore += 200;
            quizGameUI.ScoreText.text = "Score:" + gameScore;
        }
        else
        {
            lifesRemaining--;
            quizGameUI.ReduceLife(lifesRemaining);
            if (lifesRemaining == 0)
            {
                GameEnd();
            }
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
            {
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                win();
            }
        }
        return correct;
    }

    private void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.GameOverPanel.SetActive(true);
        PlayerPrefs.SetInt(currentCategory, correctAnswerCount); 
    }

    private void win()
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.WinScreenPanel.SetActive(true);
        PlayerPrefs.SetInt(currentCategory, correctAnswerCount); 
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;         
    public QuestionType questionType;   
    public Sprite questionImage;        
    public AudioClip audioClip;        
    public UnityEngine.Video.VideoClip videoClip;   
    public List<string> options;      
    public string correctAns;     
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}
