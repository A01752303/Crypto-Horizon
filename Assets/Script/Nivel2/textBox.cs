using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdvancedTextSceneSwitcher : MonoBehaviour
{
    public Animator transition;
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public Button switchButton;
    public Button finalButton;
    public Button resetButton;

    public GameObject[] animatedObjects; // Solo 3 objetos (0,1,2)
    private int index = 0;
    private int currentScene = 0;

    private string[] titles = {
        "What is Web 1.0?", "What is Web 1.0?",
        "What is Web 2.0?", "What is Web 2.0?",
        "Web 3.0: The Future", "Web 3.0: The Future",
        "You finished!"
    };

    private string[] bodies = {
        "Imagine browsing the web like flipping through a paper catalog: everything is static, you can't touch or change anything. That's basically Web 1.0, a place where pages were like digital posters, full of information but no interaction.", 
        "<b>Characteristics of WEB 1.0:</b> \n- Static pages \n- Limited interactivity \n- Read-only content \n- Basic HTML and CSS \n- No buttons to comment, share, or interact \n- Emmerged in the 90s",
        "Imagine the web turning into a bustling social hub where everyone can share, comment, and create. That's Web 2.0, a place where pages became dynamic and interactive, transforming from static posters into a lively conversation. It was a shift to user-generated content, allowing users to participate in shaping it.", 
        "<b>Disadvantages of Web 2.0:</b> \n- Privacy concerns with personal data. \n- Centralized control by big corporations. \n- Intrusive ads and targeted marketing. \n- Spread of misinformation \n- Potential for social media addiction.",
        "Imagine a web that's smarter, more intuitive, and gives you full control over your data. That's Web 3.0, where decentralization, blockchain, and artificial intelligence transform how we experience the internet. It's a web where your personal data belongs to you, and the experience is tailored to your preferences.", 
        "<b>Characteristics of WEB 3.0:</b> \n- Decentralized applications \n- Blockchain and cryptocurrency integration \n- Digital goods with blockchain \n- Smarter and more intuitive interfaces \n- AI-driven personalization",
        "Great job! You've completed the first part of Level 2. Good luck on the upcoming quiz, finish it to receive the SECOND KEY!\n \n<b>(You can restart the sequence anytime using the 'Start Again' button.)</b>"
    };

    void Start()
    {
        switchButton.onClick.AddListener(() => StartCoroutine(SwitchScene()));
        finalButton.onClick.AddListener(GoToNextScene);
        resetButton.onClick.AddListener(RestartFlow);

        finalButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);

        for (int i = 0; i < animatedObjects.Length; i++)
            animatedObjects[i].SetActive(i == 0);

        UpdateText(index);
    }

    IEnumerator SwitchScene()
    {
        if (index >= titles.Length - 1)
            yield break;

        int nextIndex = index + 1;
        int nextScene = Mathf.Min(nextIndex / 2, 2); // 0,1,2 como máximo

        if (nextScene != currentScene)
        {
            if (currentScene != 2)
            {
                Animator currentAnim = animatedObjects[currentScene].GetComponent<Animator>();
                if (currentAnim != null)
                    currentAnim.SetTrigger("End");

                yield return new WaitForSeconds(1f);
            }

            foreach (GameObject obj in animatedObjects)
                obj.SetActive(false);

            GameObject nextObj = animatedObjects[nextScene];
            nextObj.SetActive(true);

        Animator nextAnim = nextObj.GetComponent<Animator>();
        if (nextAnim != null)
            nextAnim.SetTrigger("Start");


            currentScene = nextScene;
        }

        index++;
        UpdateText(index);

        if (index == titles.Length - 1)
        {
            yield return new WaitForSeconds(0.5f);
            switchButton.gameObject.SetActive(false);
            finalButton.gameObject.SetActive(true);
            resetButton.gameObject.SetActive(true); // Mostramos el botón de reset
        }
    }

    void UpdateText(int i)
    {
        titleText.text = titles[i];
        bodyText.text = bodies[i];
    }

    void GoToNextScene()
    {
        StartCoroutine(LoadLevel(6));
    }

    void RestartFlow()
    {
        // Reiniciar variables
        index = 0;
        currentScene = 0;

        // Reset UI
        switchButton.gameObject.SetActive(true);
        finalButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);

        // Desactivar todos y activar solo el primero
        for (int i = 0; i < animatedObjects.Length; i++)
            animatedObjects[i].SetActive(i == 0);

        UpdateText(index);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(levelIndex);
    }
}
