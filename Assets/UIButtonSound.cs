using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSource;

    void Awake()
    {
        // Crear o asignar un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        // Obtener raíz del UIDocument
        var root = uiDocument.rootVisualElement;

        // Obtener los tres botones por su name exacto del UI Builder
        Button loginButton = root.Q<Button>("LoginButton");
        Button restoreButton = root.Q<Button>("RestoreButton");
        Button registerButton = root.Q<Button>("RegisterButton");

        // Verifica y asigna eventos
        if (loginButton != null)
        {
            loginButton.clicked += () =>
            {
                PlayClickSound();
                Debug.Log("Log In clicked");
                // Aquí puedes agregar tu lógica de login
            };
        }

        if (restoreButton != null)
        {
            restoreButton.clicked += () =>
            {
                PlayClickSound();
                Debug.Log("Restore clicked");
                // Lógica de recuperación
            };
        }

        if (registerButton != null)
        {
            registerButton.clicked += () =>
            {
                PlayClickSound();
                Debug.Log("Register clicked");
                // Lógica de registro
            };
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}


