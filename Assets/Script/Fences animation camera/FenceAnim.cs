using UnityEngine;
using System.Collections;

public class FenceAnim : MonoBehaviour
{
    public Animator fence;
    public Animator flash;
    public GameObject fenceObject;
    public GameObject cinemachine;
    public GameObject camera2;
    public GameObject camera3;

    private gameManager gameManager;

    void Start()
    {
        // Buscar el gameManager en la escena
        gameManager = FindObjectOfType<gameManager>();

        if (gameManager != null)
        {
            // Si se encuentra el gameManager, realiza la lógica
            if (gameManager.nivel1Completo)
            {
                StartCoroutine(CambiarCamarasConDelay(1.5f, cinemachine, camera2, camera3, 1));
            }
            else if (gameManager.nivel1Completo && gameManager.nivel2Completo)
            {
                StartCoroutine(CambiarCamarasConDelay(1.5f, cinemachine, camera2, camera3, 2));
            }
        }
        else
        {
            Debug.LogError("gameManager no encontrado en la escena.");
        }
    }

    // Corutina que cambia las cámaras después de un retraso
    private IEnumerator CambiarCamarasConDelay(float delayTime, GameObject cinemachine, GameObject camera2, GameObject camera3, int nivel)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delayTime);

        if(nivel == 1)
        {
            // Cambiar las cámaras después del retraso
            cinemachine.SetActive(false);
            camera2.SetActive(true);
            camera3.SetActive(false);
        }
        else if (nivel == 2)
        {
            // Cambiar las cámaras después del retraso
            cinemachine.SetActive(false);
            camera2.SetActive(false);
            camera3.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        AnimationFence();
        yield return new WaitForSeconds(1f);

        // Destruit cerca
        if (fenceObject != null)
        {
            Destroy(fenceObject);
        }
        else
        {
            Debug.LogError("Objeto de la cerca no encontrado.");
        }

        yield return new WaitForSeconds(3f);
    
        // Cambiar las cámaras después del retraso
        cinemachine.SetActive(true);
        camera2.SetActive(false);
        camera3.SetActive(false);
    }

    private void AnimationFence()
    {
        // Iniciar la animación de la cerca
        if (fence != null)
        {
            fence.SetTrigger("Startfence");
        }
        else
        {
            Debug.LogError("Animator de la cerca no encontrado.");
        }

        // Iniciar la animación del flash
        if (flash != null)
        {
            flash.SetTrigger("Startflash");
        }
        else
        {
            Debug.LogError("Animator del flash no encontrado.");
        }
    }
}
