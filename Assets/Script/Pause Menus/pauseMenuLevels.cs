using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class pauseMenuLevels : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject pauseButton;

    public Animator transition;

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }

    public void BackToIslands()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(2));
    }

    public void Main()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
