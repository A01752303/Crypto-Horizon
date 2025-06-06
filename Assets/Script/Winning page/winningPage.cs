using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinningPage : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 1f;
    private Button button;
    [SerializeField] private AudioClip buttonClickSound; 
    private AudioSource audioSource;

    private void Start()
    {
    audioSource = GetComponent<AudioSource>();

    if (audioSource == null)
        {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        }
    }
    public void ActivateButton()
    {
        GameObject canvasObj = GameObject.Find("UI button"); // nombre del Canvas
        if (canvasObj != null)
        {
            // Busca el Button en los hijos del Canvas
            button = canvasObj.GetComponentInChildren<Button>(true); // true incluye inactivos
            if (button != null)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se encontró ningún Button dentro del Canvas 'UI Button'.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró un Canvas llamado 'UI Button'.");
        }
    }

    public void LoadLevel()
    {
        PlayButtonSound();
        StartCoroutine(RegresarMenu());
    }

    public void PlayButtonSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    private IEnumerator RegresarMenu()
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene("inicial map");
    }
}
