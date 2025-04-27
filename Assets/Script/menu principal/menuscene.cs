using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuscene : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 1f;

    public void play()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void exit()
    {     
        if (SceneM.Instance != null)
        {
            SceneM.Instance.logOut();
        }
    }

    public void link(string url)
    {
        Application.OpenURL(url);
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