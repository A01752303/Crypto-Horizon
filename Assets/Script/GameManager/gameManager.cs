using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public bool nivel1Completo = false;
    public bool nivel2Completo = false;
    public bool nivel3Completo = false;
    public bool trofeobroncenivel1 = false;
    public bool trofeoplatanivel1 = false;
    public bool trofeogoldnivel1 = false;
    public bool trofeobroncenivel2 = false;
    public bool trofeoplatanivel2 = false;
    public bool trofeogoldnivel2 = false;
    public bool trofeobroncenivel3 = false;
    public bool trofeoplatanivel3 = false;
    public bool trofeogoldnivel3 = false;

    public int llaves = 0;

    // Checkmarks de niveles
    public GameObject checklvl1;
    public GameObject checklvl2;
    public GameObject checklvl3;

    // Objetos que se activan con cada llave
    public GameObject objetoConLlave1;
    public GameObject objetoConLlave2;
    public GameObject objetoConLlave3;
    public GameObject checkTrofeo1;
    public GameObject checkTrofeo2;
    public GameObject checkTrofeo3;
    public GameObject objetoconTrofeo1;
    public GameObject objetoconTrofeo2;
    public GameObject objetoconTrofeo3;

    // Posición del jugador
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

        CargarProgresoBD();
    }

    public void GuardarPosicionJugador(Vector3 nuevaPosicion)
    {
        jugadorPosicion = nuevaPosicion;
    }

    public void CompletarNivel(int nivel)
    {
        if (nivel == 1 && !nivel1Completo)
        {
            nivel1Completo = true;
            llaves++;
            PlayerPrefs.SetInt("nivel1Completo", 1);

            checklvl1 = GameObject.Find("Checklvl1");
            if (checklvl1 != null) checklvl1.SetActive(true);

            if (objetoConLlave1 != null)
                objetoConLlave1.SetActive(true);

            PlayerPrefs.SetInt("objetoLlave1Activo", 1);
        }
        else if (nivel == 2 && !nivel2Completo)
        {
            nivel2Completo = true;
            llaves++;
            PlayerPrefs.SetInt("nivel2Completo", 1);

            checklvl2 = GameObject.Find("Checklvl2");
            if (checklvl2 != null) checklvl2.SetActive(true);

            if (objetoConLlave2 != null)
                objetoConLlave2.SetActive(true);

            PlayerPrefs.SetInt("objetoLlave2Activo", 1);
        }
        else if (nivel == 3 && !nivel3Completo)
        {
            nivel3Completo = true;
            llaves++;
            PlayerPrefs.SetInt("nivel3Completo", 1);

            checklvl3 = GameObject.Find("Checklvl3");
            if (checklvl3 != null) checklvl3.SetActive(true);

            if (objetoConLlave3 != null)
                objetoConLlave3.SetActive(true);

            PlayerPrefs.SetInt("objetoLlave3Activo", 1);
        }

        PlayerPrefs.Save();
        
        NetworkManager.Instance.saveLevelCompleted(SceneM.Instance.currentUserId, nivel, QuizManager.correctAnswerCount, QuizManager.completionTime, response =>
        {
            if (response.done)
            {
                Debug.Log("Nivel " + nivel + " guardado correctamente.");
            }
            else
            {
                Debug.LogError("Error al guardar el nivel " + nivel + ": " + response.message);
            }
        });
    }

    public void CompletarTrofeoGold(int nivel)
    {
        if (nivel == 1 && trofeogoldnivel1)
        {
            PlayerPrefs.SetInt("trofeogoldnivel1", 1);

            // Activar el check para el trofeo de oro del nivel 1
            GameObject checkTimeLvl1 = GameObject.Find("Checktimelvl1");
            if (checkTimeLvl1 != null) checkTimeLvl1.SetActive(true);

            if (objetoconTrofeo1 != null)
                objetoconTrofeo1.SetActive(true);

            PlayerPrefs.SetInt("objetoTrofeoGold1Activo", 1);
        }
        else if (nivel == 2 && trofeogoldnivel2)
        {
            PlayerPrefs.SetInt("trofeogoldnivel2", 1);

            // Activar el check para el trofeo de oro del nivel 2
            GameObject checkTimeLvl2 = GameObject.Find("Checktimelvl2");
            if (checkTimeLvl2 != null) checkTimeLvl2.SetActive(true);

            if (objetoconTrofeo2 != null)
                objetoconTrofeo2.SetActive(true);

            PlayerPrefs.SetInt("objetoTrofeoGold2Activo", 1);
        }
        else if (nivel == 3 && trofeogoldnivel3)
        {
            PlayerPrefs.SetInt("trofeogoldnivel3", 1);

            // Activar el check para el trofeo de oro del nivel 3
            GameObject checkTimeLvl3 = GameObject.Find("Checktimelvl3");
            if (checkTimeLvl3 != null) checkTimeLvl3.SetActive(true);

            if (objetoconTrofeo3 != null)
                objetoconTrofeo3.SetActive(true);

            PlayerPrefs.SetInt("objetoTrofeoGold3Activo", 1);
        }

        PlayerPrefs.Save();
    }


// Método encargado de cargar el progreso del usuario desde la base de datos.
// Recupera los niveles completados, llaves obtenidas y trofeos conseguidos.
private void CargarProgresoBD()
{
    // Obtiene el ID del usuario actual desde la instancia de SceneM
    int userId = SceneM.Instance.currentUserId;
    
    // Realiza una solicitud al NetworkManager para obtener el progreso del usuario
    NetworkManager.Instance.LoadUserProgress(SceneM.Instance.currentUserId, response =>
    {
        // Registra información sobre la respuesta recibida en el log de depuración
        Debug.Log($"Respuesta recibida. Éxito: {response.done}, Mensaje: {response.message}");
        
        // Verifica que la solicitud fue exitosa y que hay datos de progreso
        if (response.done && response.progress != null)
        {
            // Itera a través de cada nivel completado en el progreso del usuario
            foreach (var progress in response.progress)
            {
                // Procesa cada nivel según su ID
                switch (progress.level_id)
                {
                    case 1: // Nivel 1
                        // Marca el nivel como completado
                        nivel1Completo = true;
                        llaves++;
                        
                        // Almacena el progreso en PlayerPrefs para persistencia local
                        PlayerPrefs.SetInt("nivel1Completo", 1);
                        PlayerPrefs.SetInt("objetoLlave1Activo", 1);
                        
                        // Actualiza elementos visuales en la interfaz si existen
                        if (checklvl1 != null) checklvl1.SetActive(true);
                        if (objetoConLlave1 != null) objetoConLlave1.SetActive(true);

                        // Verifica si el nivel fue completado con tiempo suficiente para el trofeo de oro
                        if (progress.time <= 20)
                        {
                            // Otorga y registra el trofeo de oro para el nivel 1
                            trofeogoldnivel1 = true;
                            PlayerPrefs.SetInt("trofeogoldnivel1", 1);
                            PlayerPrefs.SetInt("objetoTrofeoGold1Activo", 1);
                            
                            // Actualiza elementos visuales relacionados con el trofeo
                            if (checkTrofeo1 != null) checkTrofeo1.SetActive(true);
                            if (objetoconTrofeo1 != null) objetoconTrofeo1.SetActive(true);
                        }
                        break;
                        
                    case 2: // Nivel 2
                        // Código similar al del nivel 1 pero para el nivel 2
                        // ...
                        break;
                        
                    case 3: // Nivel 3
                        // Código similar al del nivel 1 pero para el nivel 3
                        // ...
                        break;
                }
            }
            
            // Guarda los cambios en PlayerPrefs
            PlayerPrefs.Save();
            
            // Asegura que los trofeos sean guardados correctamente
            GuardarTrofeos();
        }
        else
        {
            // Registra un error si no se pudo cargar el progreso
            Debug.LogError("Error al cargar el progreso: " + response.message);
        }
    });
}


    public void RestaurarPosicionJugador(GameObject jugador)
    {
        if (jugador != null)
        {
            jugador.transform.position = jugadorPosicion;
        }
    }

    public void GuardarTrofeos()
    {
        PlayerPrefs.SetInt("trofeobroncenivel1", trofeobroncenivel1 ? 1 : 0);
        PlayerPrefs.SetInt("trofeoplatanivel1", trofeoplatanivel1 ? 1 : 0);
        PlayerPrefs.SetInt("trofeogoldnivel1",  trofeogoldnivel1 ? 1 : 0);

        PlayerPrefs.SetInt("trofeobroncenivel2", trofeobroncenivel2 ? 1 : 0);
        PlayerPrefs.SetInt("trofeoplatanivel2", trofeoplatanivel2 ? 1 : 0);
        PlayerPrefs.SetInt("trofeogoldnivel2",  trofeogoldnivel2 ? 1 : 0);

        PlayerPrefs.SetInt("trofeobroncenivel3", trofeobroncenivel3 ? 1 : 0);
        PlayerPrefs.SetInt("trofeoplatanivel3", trofeoplatanivel3 ? 1 : 0);
        PlayerPrefs.SetInt("trofeogoldnivel3",  trofeogoldnivel3 ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void ResetProgress()
    {
        // Reset all progress variables
        nivel1Completo = false;
        nivel2Completo = false;
        nivel3Completo = false;
        trofeobroncenivel1 = false;
        trofeoplatanivel1 = false;
        trofeogoldnivel1 = false;
        trofeobroncenivel2 = false;
        trofeoplatanivel2 = false;
        trofeogoldnivel2 = false;
        trofeobroncenivel3 = false;
        trofeoplatanivel3 = false;
        trofeogoldnivel3 = false;
        llaves = 0;
        jugadorPosicion = Vector3.zero;
        
        // Reset UI elements if they exist
        if (checklvl1 != null) checklvl1.SetActive(false);
        if (checklvl2 != null) checklvl2.SetActive(false);
        if (checklvl3 != null) checklvl3.SetActive(false);
        
        if (objetoConLlave1 != null) objetoConLlave1.SetActive(false);
        if (objetoConLlave2 != null) objetoConLlave2.SetActive(false);
        if (objetoConLlave3 != null) objetoConLlave3.SetActive(false);
        
        if (checkTrofeo1 != null) checkTrofeo1.SetActive(false);
        if (checkTrofeo2 != null) checkTrofeo2.SetActive(false);
        if (checkTrofeo3 != null) checkTrofeo3.SetActive(false);
        
        if (objetoconTrofeo1 != null) objetoconTrofeo1.SetActive(false);
        if (objetoconTrofeo2 != null) objetoconTrofeo2.SetActive(false);
        if (objetoconTrofeo3 != null) objetoconTrofeo3.SetActive(false);
        
        Debug.Log("🔄 GameManager progress has been reset");
    }

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        // Borra los datos guardados solo en el editor
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progreso borrado al salir del simulador de Unity.");
#endif
    }
    
}