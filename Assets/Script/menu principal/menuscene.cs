using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuscene : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 1f;
    [SerializeField] private AudioClip buttonClickSound; 
    private AudioSource audioSource;
    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void play()
    {
        PlayButtonSound(); 
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void leaderboard()
    {
        PlayButtonSound(); 
    }

    public void about()
    {
        PlayButtonSound(); 
    }

    public void exit()
    {     
        PlayButtonSound(); 
        if (SceneM.Instance != null)
        {
            StartCoroutine(SceneM.Instance.logOut());
        }
    }

    public void link(string url)
    {
        PlayButtonSound(); 
        Application.OpenURL(url);
    }

    private void PlayButtonSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(levelIndex);
    }
}