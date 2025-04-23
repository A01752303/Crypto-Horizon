using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level1Finished : MonoBehaviour
{
    public Animator transition;
    public void completeQuiz()
    {
        gameManager.Instance.CompletarNivel(1);
        gameManager.Instance.CompletarTrofeoGold(1);
        StartCoroutine(LoadLevel(2));
    }

    public void completeQuiz2()
    {
        gameManager.Instance.CompletarNivel(2);
        gameManager.Instance.CompletarTrofeoGold(2);
        StartCoroutine(LoadLevel(2));
    }

    public void completeQuiz3()
    {
        gameManager.Instance.CompletarNivel(3);
        gameManager.Instance.CompletarTrofeoGold(3);
        StartCoroutine(LoadLevel(2));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
