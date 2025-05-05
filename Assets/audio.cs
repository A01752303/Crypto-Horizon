using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class audio : MonoBehaviour
{
    public static audio instance;
    
    [SerializeField] private AudioSource musicSource;
    
    [Header("Configuración de volumen")]
    [SerializeField] private float menuVolume = 0.35f;
    [SerializeField] private float gameplayVolume = 0.05f;
    [SerializeField] private float transitionDuration = 0.5f;
    
    // Nombre de la escena del menú principal
    [SerializeField] private string menuSceneName = "menu";
    
    // Lista de escenas donde la música debe pausarse
    [SerializeField] private List<string> mutedScenes = new List<string>() {"Slide","GameScene", "Quiz2","Simulation", "dragdrop","quiz","slide"};
    
    private float currentVolume;
    private float targetVolume;
    private bool isTransitioning = false;
    private bool wasMusicPlaying = false;
    
    
    // Inicializa el singleton y asegura que persista entre escenas.
    private void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            
            // Obtener referencia al AudioSource si no está asignado
            if (musicSource == null)
            {
                musicSource = GetComponent<AudioSource>();
            }
            
            // Configuración inicial
            currentVolume = musicSource.volume;
            
            // Registrar para eventos de cambio de escena
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Método llamado cuando una nueva escena es cargada.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar si la escena está en la lista de escenas silenciadas
        if (mutedScenes.Contains(scene.name))
        {
            // Guardar el estado de reproducción actual
            wasMusicPlaying = true;
            
            // Pausar la música
            musicSource.Pause();
        }
        else 
        {
            // Determinar el volumen objetivo basado en la escena
            if (scene.name == menuSceneName)
            {
                targetVolume = menuVolume;
            }
            else 
            {
                targetVolume = gameplayVolume;
            }
            
            // Reanudar la música si estaba reproduciéndose antes
            if (wasMusicPlaying && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
            
            // Iniciar transición de volumen
            isTransitioning = true;
        }
    }
    
    // Actualiza el volumen gradualmente cuando está en transición.
    private void Update()
    {
        if (isTransitioning)
        {
            // Ajustar volumen gradualmente
            currentVolume = Mathf.Lerp(currentVolume, targetVolume, Time.deltaTime / transitionDuration);
            
            // Aplicar el volumen al AudioSource
            if (musicSource != null)
            {
                musicSource.volume = currentVolume;
            }
            
            // Verificar si la transición está completa
            if (Mathf.Approximately(currentVolume, targetVolume))
            {
                isTransitioning = false;
            }
        }
    }
    
    // Limpia los eventos al destruir el objeto.
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // Ajusta el volumen manualmente a un valor específico.
    /// <param name="volume">Volumen objetivo (0.0 a 1.0)</param>
    /// <param name="immediate">Si es verdadero, cambia inmediatamente sin transición</param>
    public void SetVolume(float volume, bool immediate = false)
    {
        targetVolume = Mathf.Clamp01(volume);
        
        if (immediate)
        {
            currentVolume = targetVolume;
            if (musicSource != null)
            {
                musicSource.volume = currentVolume;
            }
            isTransitioning = false;
        }
        else
        {
            isTransitioning = true;
        }
    }
    
    /// Pausa la música de fondo.
    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            wasMusicPlaying = true;
            musicSource.Pause();
        }
    }
    
    // Reanuda la música de fondo si estaba reproduciéndose anteriormente.
    public void ResumeMusic()
    {
        if (musicSource != null && wasMusicPlaying && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}
