using UnityEngine;
using UnityEngine.UIElements;

public class OpenLinkButton : MonoBehaviour
{
    void OnEnable()
    {
        // Obtener la raíz del UIDocument
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Buscar el botón por su nombre
        Button restoreButton = root.Q<Button>("RestoreButton");

        // Verificar que lo encontró y agregar la acción de clic
        if (restoreButton != null)
        {
            restoreButton.clicked += () =>
            {
                Application.OpenURL("https://www.cryptohorizongame.org/forgot"); // Ruta web para reestablecer contraseña
            };
        }
        else
        {
            Debug.LogWarning("No se encontró el botón con name 'RestoreButton'");
        }
    }
}

