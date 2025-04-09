using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject dialog;
    public DialogInicio dialogInicio;
    private const string DialogKey = "DialogShown";

    void Start()
    {
        if (PlayerPrefs.GetInt(DialogKey, 0) == 1)
        {
            Destroy(dialog);
            dialogInicio.turnOnMoveCharacter();
            dialogInicio.turnOnEKey(); 
        }
    }

    public void CloseDialog()
    {
        PlayerPrefs.SetInt(DialogKey, 1);
        PlayerPrefs.Save(); 

        Destroy(dialog);
        dialogInicio.turnOnMoveCharacter(); 
        dialogInicio.turnOnEKey(); 
    }

    void OnApplicationQuit()
    {
        // Reiniciar los valores de PlayerPrefs al finalizar la simulación
        PlayerPrefs.SetInt(DialogKey, 0); 
        PlayerPrefs.Save(); 
        Debug.Log("PlayerPrefs Reiniciado");

    }

}