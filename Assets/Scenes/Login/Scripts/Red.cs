using UnityEngine;
using UnityEngine.UIElements;

public class Red : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private SceneM sceneM;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        var loginContainer = root.Q<VisualElement>("Login");
        var registerContainer = root.Q<VisualElement>("Register");

        var login_RegisterButton = loginContainer.Q<Button>("RegisterButton");
        var register_LoginButton = registerContainer.Q<Button>("LoginButton");

        if (login_RegisterButton != null)
            login_RegisterButton.clicked += sceneM.ShowRegister;

        if (register_LoginButton != null)
            register_LoginButton.clicked += sceneM.ShowLogin;
    }
}