using Unity.VisualScripting;
using UnityEngine;

public class audio : MonoBehaviour
{
    public static audio instance;
    private void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
