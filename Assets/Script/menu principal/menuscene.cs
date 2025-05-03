using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuscene : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 1f;

    [SerializeField] private AudioClip buttonClickSound; 
    private AudioSource audioSource;

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Leaderboard;
    [SerializeField] private GameObject About;

    private void Start()
    {
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
        StartCoroutine(PlaySoundThen(() =>
        {
            MainMenu.SetActive(false);
            Leaderboard.SetActive(true);
        }));
    }

    public void about()
    {
        StartCoroutine(PlaySoundThen(() =>
        {
            MainMenu.SetActive(false);
            About.SetActive(true);
        }));
    }

    public void exit()
    {
        StartCoroutine(PlaySoundThen(() =>
        {
            if (SceneM.Instance != null)
            {
                StartCoroutine(SceneM.Instance.logOut());
            }
        }));
    }

    public void link(string url)
    {
        Application.OpenURL(url); 
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
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(levelIndex);
    }

    private IEnumerator PlaySoundThen(System.Action action)
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
            yield return new WaitForSeconds(buttonClickSound.length);
        }

        action?.Invoke();
    }
}
