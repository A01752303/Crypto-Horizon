using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public bool nivel1Completo = false;
    public bool nivel2Completo = false;
    public bool nivel3Completo = false;
    public int llaves = 0;

    // Variable para almacenar la posición del jugador
    public Vector3 jugadorPosicion;

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

    // Método para guardar la posición del jugador
    public void GuardarPosicionJugador(Vector3 nuevaPosicion)
    {
        jugadorPosicion = nuevaPosicion;
    }

    // Método para completar niveles y ganar llaves
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

    // Método para restaurar la posición del jugador cuando se recarga la escena
    public void RestaurarPosicionJugador(GameObject jugador)
    {
        if (jugador != null && jugadorPosicion != null)
        {
            jugador.transform.position = jugadorPosicion;
        }
    }


    // Método que se llama cuando la simulación o la aplicación termina
    private void OnApplicationQuit()
    {
        // Restablecer las variables cuando termine la simulación o la aplicación
        nivel1Completo = false;
        nivel2Completo = false;
        nivel3Completo = false;
        llaves = 0;
        jugadorPosicion = Vector3.zero;  // Restablecer la posición guardada
        Debug.Log("Valores del gameManager reseteados.");
    }

    
}
