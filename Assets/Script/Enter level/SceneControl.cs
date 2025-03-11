using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneControl : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 1f;
    public static SceneControl instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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
