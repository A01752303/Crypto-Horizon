using UnityEngine;
using TMPro;

public class updateKeyCountUI : MonoBehaviour
{
    public TMP_Text countText; 

    private void Start()
    {
        UpdateKeyCount(); 
    }

    private void Update()
    {
        if (gameManager.Instance != null) 
        {
            UpdateKeyCount();
        }
    }

    public void UpdateKeyCount()
    {
        if (countText != null)
        {
            countText.text = "x" + gameManager.Instance.llaves.ToString();
        }
    }
}
