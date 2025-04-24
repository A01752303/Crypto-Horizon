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
    public bool animacionFence1YaMostrada;
    public bool animacionFence2YaMostrada;
    private gameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<gameManager>();

        if (gameManager == null)
        {
            Debug.LogError("gameManager no encontrado en la escena.");
            return;
        }

        animacionFence1YaMostrada = PlayerPrefs.GetInt("fence1AnimHecha", 0) == 1;
        animacionFence2YaMostrada = PlayerPrefs.GetInt("fence2AnimHecha", 0) == 1;

        // Nivel 1 completo
        if (gameManager.nivel1Completo)
        {
            if (!animacionFence1YaMostrada)
            {
                StartCoroutine(CambiarCamarasConDelay(1.5f, 1));
            }
            else if (fenceObject1 != null)
            {
                fenceObject1.SetActive(false); // Ya se destruyó previamente
            }
        }

        // Nivel 2 completo
        if (gameManager.nivel2Completo)
        {
            if (!animacionFence2YaMostrada)
            {
                StartCoroutine(CambiarCamarasConDelay(1.5f, 2));
            }
            else if (fenceObject2 != null)
            {
                fenceObject2.SetActive(false); // Ya se destruyó previamente
            }
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

        // Ocultar cerca y marcar animación como hecha
        if (nivel == 1 && fenceObject1 != null)
        {
            fenceObject1.SetActive(false);
            PlayerPrefs.SetInt("fence1AnimHecha", 1);
        }
        else if (nivel == 2 && fenceObject2 != null)
        {
            fenceObject2.SetActive(false);
            PlayerPrefs.SetInt("fence2AnimHecha", 1);
        }

        PlayerPrefs.Save();

        yield return new WaitForSeconds(3f);

        // Restaurar cámara original
        cinemachine.SetActive(true);
        camera2.SetActive(false);
        camera3.SetActive(false);
    }

    private void AnimationFence(int nivel)
    {
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
}
