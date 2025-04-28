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
    public static int correctAnswerCount = 0;
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

    private int currentLevel = 0; // NUEVO: nivel actual (0, 1 o 2)
        // Add this with your other static variables
    public static float completionTime = 0;
    public int CurrentLevel { get => currentLevel; } // NUEVO: propiedad para acceder al nivel actual

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

    private int GetCurrentLevel()
    {
        string category = currentCategory.ToLower();

        if (category.Contains("blockchain and cryptos introduction"))
            return 1;
        else if (category.Contains("web technology"))
            return 2;
        else if (category.Contains("blocks and hashes"))
            return 3;
        else
            return -1;
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

    public void ActualizarTrofeo(ref bool bronce, ref bool plata, ref bool oro, float tiempo)
    {
        int nivel = gameManager.Instance.llaves;

        if (oro)
        {
            quizGameUI.nextButtonPanelWin.gameObject.SetActive(false);
            quizGameUI.nextButtonPanelWinKey.gameObject.SetActive(false);
            quizGameUI.backtoislandPanelWin.gameObject.SetActive(true);
            quizGameUI.backtoislandPanelWinKey.gameObject.SetActive(true);
            return;
        }

        if (tiempo <= 20f)
        {
            oro = true;
            plata = false;
            bronce = false;

            quizGameUI.nextButtonPanelWin.gameObject.SetActive(true);
            quizGameUI.nextButtonPanelWinKey.gameObject.SetActive(true);
            quizGameUI.backtoislandPanelWin.gameObject.SetActive(false);
            quizGameUI.backtoislandPanelWinKey.gameObject.SetActive(false);
            quizGameUI.TrophyText.text = "You got the GOLD time trophy!";

            PlayerPrefs.SetInt($"trofeogoldnivel{nivel}", 1);
            PlayerPrefs.SetInt($"trofeoplatanivel{nivel}", 0);
            PlayerPrefs.SetInt($"trofeobroncenivel{nivel}", 0);

        }
        else if (tiempo <= 40f)
        {
            if (!plata && !oro)
            {
                plata = true;
                bronce = false;

                quizGameUI.nextButtonPanelWin.gameObject.SetActive(true);
                quizGameUI.nextButtonPanelWinKey.gameObject.SetActive(true);
                quizGameUI.backtoislandPanelWin.gameObject.SetActive(false);
                quizGameUI.backtoislandPanelWinKey.gameObject.SetActive(false);
                quizGameUI.TrophyText.text = "You got the SILVER time trophy!";

                PlayerPrefs.SetInt($"trofeoplatanivel{nivel}", 1);
                PlayerPrefs.SetInt($"trofeobroncenivel{nivel}", 0);

                
            }
        }
        else if (tiempo <= 60f)
        {
            if (!bronce && !plata && !oro)
            {
                bronce = true;
                quizGameUI.nextButtonPanelWin.gameObject.SetActive(true);
                quizGameUI.nextButtonPanelWinKey.gameObject.SetActive(true);
                quizGameUI.backtoislandPanelWin.gameObject.SetActive(false);
                quizGameUI.backtoislandPanelWinKey.gameObject.SetActive(false);
                quizGameUI.TrophyText.text = "You got the BRONZE trophy!";
                PlayerPrefs.SetInt($"trofeobroncenivel{nivel}", 1);
                
                
            }
        }
        else
        {
            quizGameUI.nextButtonPanelWin.gameObject.SetActive(false);
            quizGameUI.nextButtonPanelWinKey.gameObject.SetActive(false);
            quizGameUI.backtoislandPanelWin.gameObject.SetActive(true);
            quizGameUI.backtoislandPanelWinKey.gameObject.SetActive(true);
        
        }

        gameManager.Instance.GuardarTrofeos();
        PlayerPrefs.Save();
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
        quizGameUI.winScreenPanel.SetActive(true);

        float elapsedTime = timeInSeconds - currentTime;
        completionTime = elapsedTime;

    // Obtener el nivel actual usando la función GetCurrentLevel
    int currentLevel = GetCurrentLevel();

    // Referencias a los trofeos según el nivel
    ref bool bronce = ref gameManager.Instance.trofeobroncenivel1;
    ref bool plata = ref gameManager.Instance.trofeoplatanivel1;
    ref bool oro = ref gameManager.Instance.trofeogoldnivel1;

    // Verificar el nivel actual y asignar los trofeos correspondientes
    switch (currentLevel)
    {
        case 1:
            bronce = ref gameManager.Instance.trofeobroncenivel1;
            plata = ref gameManager.Instance.trofeoplatanivel1;
            oro = ref gameManager.Instance.trofeogoldnivel1;
            break;
        case 2:
            bronce = ref gameManager.Instance.trofeobroncenivel2;
            plata = ref gameManager.Instance.trofeoplatanivel2;
            oro = ref gameManager.Instance.trofeogoldnivel2;
            break;
        case 3:
            bronce = ref gameManager.Instance.trofeobroncenivel3;
            plata = ref gameManager.Instance.trofeoplatanivel3;
            oro = ref gameManager.Instance.trofeogoldnivel3;
            break;
        default:
            // Si no estamos en un nivel válido, no se asignan trofeos
            break;
    }


        ActualizarTrofeo(ref bronce, ref plata, ref oro, elapsedTime);
        quizGameUI.TotalTimeText.text = "Your time: " + elapsedTime.ToString("F2") + " seconds";

        // Mostrar el panel correcto según si mejoró o no
        if (currentLevel == 1 && !gameManager.Instance.nivel1Completo || currentLevel == 2 && !gameManager.Instance.nivel2Completo || currentLevel == 3 && !gameManager.Instance.nivel3Completo)
        {
            quizGameUI.PanelWin.SetActive(true);
            quizGameUI.PanelWinKey.SetActive(false);
            quizGameUI.PanelTrophy.SetActive(false);
        }
        else
        {
            quizGameUI.PanelWinKey.SetActive(true);
            quizGameUI.PanelWin.SetActive(false);
            quizGameUI.PanelTrophy.SetActive(false);
        }

        // Mostrar gráficamente la copa obtenida
        quizGameUI.trophybronze.SetActive(elapsedTime <= 60f && elapsedTime > 40f);
        quizGameUI.trophysilver.SetActive(elapsedTime <= 40f && elapsedTime > 20f);
        quizGameUI.trophygold.SetActive(elapsedTime <= 20f);

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
