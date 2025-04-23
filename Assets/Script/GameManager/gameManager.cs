using UnityEngine;
using TMPro;

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

        CargarProgreso();
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

    private void CargarProgreso()
    {
        nivel1Completo = PlayerPrefs.GetInt("nivel1Completo", 0) == 1;
        nivel2Completo = PlayerPrefs.GetInt("nivel2Completo", 0) == 1;
        nivel3Completo = PlayerPrefs.GetInt("nivel3Completo", 0) == 1;

        llaves = (nivel1Completo ? 1 : 0) + (nivel2Completo ? 1 : 0) + (nivel3Completo ? 1 : 0);

        if (nivel1Completo && checklvl1 != null) checklvl1.SetActive(true);
        if (nivel2Completo && checklvl2 != null) checklvl2.SetActive(true);
        if (nivel3Completo && checklvl3 != null) checklvl3.SetActive(true);

        if (PlayerPrefs.GetInt("objetoLlave1Activo", 0) == 1 && objetoConLlave1 != null)
            objetoConLlave1.SetActive(true);
        if (PlayerPrefs.GetInt("objetoLlave2Activo", 0) == 1 && objetoConLlave2 != null)
            objetoConLlave2.SetActive(true);
        if (PlayerPrefs.GetInt("objetoLlave3Activo", 0) == 1 && objetoConLlave3 != null)
            objetoConLlave3.SetActive(true);

        if (PlayerPrefs.GetInt("objetoTrofeoGold1Activo", 0) == 1 && objetoconTrofeo1 != null)
            objetoconTrofeo1.SetActive(true);
        if (PlayerPrefs.GetInt("objetoTrofeoGold2Activo", 0) == 1 && objetoconTrofeo2 != null)
            objetoconTrofeo2.SetActive(true);
        if (PlayerPrefs.GetInt("objetoTrofeoGold3Activo", 0) == 1 && objetoconTrofeo3 != null)
            objetoconTrofeo3.SetActive(true);
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
