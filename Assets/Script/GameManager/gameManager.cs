using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    public bool nivel1Completo = false;
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

    public void CompletarNivel1()
    {
        nivel1Completo = true;
        llaves++;
    }
}