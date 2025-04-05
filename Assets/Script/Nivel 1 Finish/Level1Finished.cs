using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level1Finished : MonoBehaviour
{
    public Animator transition;
    public void completeQuiz()
    {
        gameManager.Instance.CompletarNivel(1);
        StartCoroutine(LoadLevel(1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
