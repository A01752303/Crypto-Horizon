using UnityEngine;
using System.Collections;

public class FenceAnim : MonoBehaviour
{
    public Animator fence1;
    public Animator flash;
    public GameObject fenceObject1;
    public Animator fence2;
    public GameObject fenceObject2;
    public GameObject cinemachine;
    public GameObject camera2;
    public GameObject camera3;

    private gameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<gameManager>();

        // Revisar estado guardado de destrucción
        bool fence1Destruida = PlayerPrefs.GetInt("fence1Destruida", 0) == 1;
        bool fence2Destruida = PlayerPrefs.GetInt("fence2Destruida", 0) == 1;

        // Si ya estaba destruida, ocultarla
        if (fence1Destruida && fenceObject1 != null)
            fenceObject1.SetActive(false);

        if (fence2Destruida && fenceObject2 != null)
            fenceObject2.SetActive(false);

        if (gameManager != null)
        {
            if (gameManager.nivel1Completo && !gameManager.nivel2Completo && !fence1Destruida)
            {
                StartCoroutine(CambiarCamarasConDelay(1.5f, 1));
            }
            else if (gameManager.nivel1Completo && gameManager.nivel2Completo && !fence2Destruida)
            {
                StartCoroutine(CambiarCamarasConDelay(1.5f, 2));
            }
        }
        else
        {
            Debug.LogError("gameManager no encontrado en la escena.");
        }
    }

    private IEnumerator CambiarCamarasConDelay(float delayTime, int nivel)
    {
        yield return new WaitForSeconds(delayTime);

        // Cambiar cámaras
        cinemachine.SetActive(false);
        camera2.SetActive(nivel == 1);
        camera3.SetActive(nivel == 2);

        yield return new WaitForSeconds(2f);

        AnimationFence(nivel);

        yield return new WaitForSeconds(1f);

        // Ocultar y marcar como destruida
        if (nivel == 1 && fenceObject1 != null)
        {
            fenceObject1.SetActive(false);
            PlayerPrefs.SetInt("fence1Destruida", 1);
        }
        else if (nivel == 2 && fenceObject2 != null)
        {
            fenceObject2.SetActive(false);
            PlayerPrefs.SetInt("fence2Destruida", 1);
        }

        PlayerPrefs.Save(); // Guardar cambios en disco

        yield return new WaitForSeconds(3f);

        // Restaurar cámara original
        cinemachine.SetActive(true);
        camera2.SetActive(false);
        camera3.SetActive(false);
    }

    private void AnimationFence(int nivel)
    {
        bool yaDestruida = (nivel == 1 && PlayerPrefs.GetInt("fence1Destruida", 0) == 1) ||
                           (nivel == 2 && PlayerPrefs.GetInt("fence2Destruida", 0) == 1);

        if (yaDestruida) return;

        Animator fence = (nivel == 1) ? fence1 : fence2;

        if (fence != null)
        {
            fence.SetTrigger("Startfence");
        }
        else
        {
            Debug.LogError("Animator de la cerca no encontrado.");
        }

        if (flash != null)
        {
            flash.SetTrigger("Startflash");
        }
        else
        {
            Debug.LogError("Animator del flash no encontrado.");
        }
    }

    // Método para restablecer los valores cuando termina la simulación
    private void OnApplicationQuit()
    {
        // Restablecer los valores de PlayerPrefs a su estado por defecto
        PlayerPrefs.SetInt("fence1Destruida", 0);
        PlayerPrefs.SetInt("fence2Destruida", 0);

        // Asegúrate de guardar los cambios
        PlayerPrefs.Save();

        Debug.Log("Valores de PlayerPrefs restablecidos a los valores por defecto.");
    }
}
