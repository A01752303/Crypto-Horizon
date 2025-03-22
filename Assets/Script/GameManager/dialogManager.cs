using UnityEngine;

public class dialogManager : MonoBehaviour
{
    public GameObject dialog; 
    private const string DialogKey = "DialogShown";
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt(DialogKey, 0) == 1)
        {
            Destroy(dialog);
        }
    }

    public void CloseDialog()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(DialogKey, 1);
        PlayerPrefs.Save();
        Destroy(dialog);
    }
}
