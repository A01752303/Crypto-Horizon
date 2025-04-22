using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    public bool nivel1Completo = false;
    public bool nivel2Completo = false;
    public bool nivel3Completo = false;

    public int llaves = 0;

    // Checkmarks de niveles
    public GameObject checklvl1;
    public GameObject checklvl2;
    public GameObject checklvl3;

    // Objetos que se activan con cada llave
    public GameObject objetoConLlave1;
    public GameObject objetoConLlave2;
    public GameObject objetoConLlave3;

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

        // Buscar objetos si no se asignaron manualmente
        if (objetoConLlave1 == null) objetoConLlave1 = GameObject.Find("NombreDelObjeto1");
        if (objetoConLlave2 == null) objetoConLlave2 = GameObject.Find("NombreDelObjeto2");
        if (objetoConLlave3 == null) objetoConLlave3 = GameObject.Find("NombreDelObjeto3");

        if (checklvl1 == null) checklvl1 = GameObject.Find("Checklvl1");
        if (checklvl2 == null) checklvl2 = GameObject.Find("Checklvl2");
        if (checklvl3 == null) checklvl3 = GameObject.Find("Checklvl3");

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
    }

    public void RestaurarPosicionJugador(GameObject jugador)
    {
        if (jugador != null)
        {
            jugador.transform.position = jugadorPosicion;
        }
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
