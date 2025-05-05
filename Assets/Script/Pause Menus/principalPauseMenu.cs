using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class principalPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject keys;
    [SerializeField] private AudioClip buttonClickSound; 
    private AudioSource audioSource;

    public Animator transition;
    
    private void Start()
    {
    audioSource = GetComponent<AudioSource>();

    if (audioSource == null)
        {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        }
    }
    public void Pause()
    {
        PlayButtonSound();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        keys.SetActive(false);
 
    }
    public void Continue()
    {
        PlayButtonSound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        keys.SetActive(true);
    }

    public void Main()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(1));
    }

    public void PlayButtonSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
