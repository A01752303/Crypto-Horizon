﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuizGameUI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] public gameManager gameManagerInstance; 
    [SerializeField] private QuizManager quizManager;               
    [SerializeField] private CategoryBtnScript categoryBtnPrefab;
    [SerializeField] private GameObject scrollHolder;
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private Text totalTimeText, trophyText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] public GameObject gameOverPanel, winScreenPanel, mainMenu, gamePanel, PanelWin, PanelWinKey, PanelTrophy;
    [SerializeField] public GameObject trophybronze, trophysilver, trophygold;
    [SerializeField] public Button nextButtonPanelWin, backtoislandPanelWin, nextButtonPanelWinKey, backtoislandPanelWinKey, backtoislandPanelTrophy;
    [SerializeField] private Color correctCol, wrongCol, normalCol; 
    [SerializeField] private Image questionImg;                     
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;   
    [SerializeField] private AudioSource questionAudio;             
    [SerializeField] private Text questionInfoText;  
    [SerializeField] private List<Button> options;   
    [SerializeField] private int optionFontSize = 24;               // Tamaño de texto de las opciones
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;


    public Animator transition;    

    [SerializeField] private Text questionCounterText;              // NUEVO: Texto para mostrar el contador de preguntas tipo "1/10"
#pragma warning restore 649

    private float audioLength;          
    private Question question;          
    private bool answered = false;      
    public Text TimerText { get => timerText; }                  
    public Text ScoreText { get => scoreText; }    
    public Text TotalTimeText { get => totalTimeText; }   
    public Text TrophyText { get => trophyText; }        
    public GameObject GameOverPanel { get => gameOverPanel; }         
    public GameObject WinScreenPanel { get => winScreenPanel; }
    public Text QuestionCounterText { get => questionCounterText; }  // NUEVO: Propiedad para acceder desde QuizManager

    private void Start()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
        CreateCategoryButtons();
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);  
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    
                questionVideo.transform.gameObject.SetActive(false);        
                questionImg.transform.gameObject.SetActive(true);           
                questionAudio.transform.gameObject.SetActive(false);       
                questionImg.sprite = question.questionImage;             
                break;
            case QuestionType.AUDIO:
                questionVideo.transform.parent.gameObject.SetActive(true);  
                questionVideo.transform.gameObject.SetActive(false);        
                questionImg.transform.gameObject.SetActive(false);          
                questionAudio.transform.gameObject.SetActive(true);         
                audioLength = question.audioClip.length;                   
                StartCoroutine(PlayAudio());                                
                break;
            case QuestionType.VIDEO:
                questionVideo.transform.parent.gameObject.SetActive(true);  
                questionVideo.transform.gameObject.SetActive(true);         
                questionImg.transform.gameObject.SetActive(false);          
                questionAudio.transform.gameObject.SetActive(false);        
                questionVideo.clip = question.videoClip;                  
                questionVideo.Play();                                      
                break;
        }
        questionInfoText.text = question.questionInfo;                    
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = $"{(char)('A' + i)}) {ansOptions[i]}";
            options[i].GetComponentInChildren<Text>().fontSize = optionFontSize;
            options[i].name = ansOptions[i];    
            options[i].image.color = normalCol; 
        }
        answered = false;                       
    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }

    IEnumerator PlayAudio()
    {
        if (question.questionType == QuestionType.AUDIO)
        {
            questionAudio.PlayOneShot(question.audioClip);
            yield return new WaitForSeconds(audioLength + 0.5f);
            StartCoroutine(PlayAudio());
        }
        else 
        {
            StopCoroutine(PlayAudio());
            yield return null;
        }
    }

    void OnClick(Button btn)
{
    if (quizManager.GameStatus == GameStatus.PLAYING)
    {
        if (!answered)
        {
            answered = true;
            bool val = quizManager.Answer(btn.name);

            if (val)
            {
                if (sfxSource && correctSound)
                    sfxSource.PlayOneShot(correctSound);

                StartCoroutine(BlinkImg(btn.image));
            }
            else
            {
                if (sfxSource && incorrectSound)
                    sfxSource.PlayOneShot(incorrectSound);

                btn.image.color = wrongCol;
            }
        }
    }
}


    void CreateCategoryButtons()
    {
        for (int i = 0; i < quizManager.QuizData.Count; i++)
        {
            CategoryBtnScript categoryBtn = Instantiate(categoryBtnPrefab, scrollHolder.transform);
            categoryBtn.SetButton(quizManager.QuizData[i].categoryName, quizManager.QuizData[i].questions.Count);
            int index = i;
            categoryBtn.Btn.onClick.AddListener(() => CategoryBtn(index, quizManager.QuizData[index].categoryName));
        }
    }

    private void CategoryBtn(int index, string category)
    {
        quizManager.StartGame(index, category); 
        mainMenu.SetActive(false);              
        gamePanel.SetActive(true);             
    }

    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            img.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RestryButton()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }

    public void NextButton()
    {
        PanelWin.SetActive(false);
        PanelWinKey.SetActive(false);
        PanelTrophy.SetActive(true);
    }
}
