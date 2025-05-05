using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Controla las transiciones entre escenas con efectos animados.
// Implementa el patrón Singleton para asegurar que solo exista una instancia.
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

    // Realiza la transición a la siguiente escena en el índice de construcción.
    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (transition != null)
        {
            // Activa la animación de transición
            transition.SetTrigger("Start");

            // Espera a que la animación se complete
            yield return new WaitForSeconds(transitionTime);
        }
        // Carga la escena objetivo
        SceneManager.LoadScene(levelIndex);
    }
}
