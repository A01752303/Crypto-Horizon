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

        // Cargar los datos guardados al iniciar
        CargarProgreso();
    }

    // Método para guardar la posición del jugador
    public void GuardarPosicionJugador(Vector3 nuevaPosicion)
    {
        jugadorPosicion = nuevaPosicion;
    }

    // Método para completar niveles y ganar llaves
    public void CompletarNivel(int nivel)
    {
        if (nivel == 1 && !nivel1Completo)
        {
            nivel1Completo = true;
            llaves++; // Solo sumar la llave si el nivel no fue completado antes
            PlayerPrefs.SetInt("nivel1Completo", 1);  // Guardar estado de nivel 1
        }
        else if (nivel == 2 && !nivel2Completo)
        {
            nivel2Completo = true;
            llaves++; // Solo sumar la llave si el nivel no fue completado antes
            PlayerPrefs.SetInt("nivel2Completo", 1);  // Guardar estado de nivel 2
        }
        else if (nivel == 3 && !nivel3Completo)
        {
            nivel3Completo = true;
            llaves++; // Solo sumar la llave si el nivel no fue completado antes
            PlayerPrefs.SetInt("nivel3Completo", 1);  // Guardar estado de nivel 3
        }

        PlayerPrefs.Save(); // Guardar todos los cambios en PlayerPrefs
    }

    // Método para cargar el progreso guardado
    private void CargarProgreso()
    {
        nivel1Completo = PlayerPrefs.GetInt("nivel1Completo", 0) == 1;
        nivel2Completo = PlayerPrefs.GetInt("nivel2Completo", 0) == 1;
        nivel3Completo = PlayerPrefs.GetInt("nivel3Completo", 0) == 1;

        // Calcular las llaves en función de los niveles completos
        llaves = (nivel1Completo ? 1 : 0) + (nivel2Completo ? 1 : 0) + (nivel3Completo ? 1 : 0);
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

        // Limpiar el progreso de los niveles y llaves al salir
        PlayerPrefs.DeleteKey("nivel1Completo");
        PlayerPrefs.DeleteKey("nivel2Completo");
        PlayerPrefs.DeleteKey("nivel3Completo");
    }
}
