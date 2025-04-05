using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public bool nivel1Completo = false;
    public bool nivel2Completo = false;
    public bool nivel3Completo = false;
    public int llaves = 0;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompletarNivel(int nivel)
    {
        if (llaves == nivel)
        {
            return;
        }

        switch (nivel)
        {
            case 1:
                nivel1Completo = true;
                break;
            case 2:
                nivel2Completo = true;
                break;
            case 3:
                nivel3Completo = true;
                break;
            default:
                Debug.LogWarning("Nivel inválido: " + nivel);
                return;
        }
        
        llaves++;
    }

}