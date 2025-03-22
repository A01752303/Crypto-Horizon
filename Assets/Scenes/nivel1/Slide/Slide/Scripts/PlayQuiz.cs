using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Playquiz : MonoBehaviour
{
    private UIDocument container;
    private Button playquiz;

    void OnEnable()
    {
        container = GetComponent<UIDocument>();
        var root = container.rootVisualElement;
        playquiz = root.Q<Button>("PLAY");
        playquiz.RegisterCallback<ClickEvent, string>(CargarEscena, "GameScene");
    }

    private void CargarEscena(ClickEvent evt, string escena){
        SceneManager.LoadScene(escena);
    }
}