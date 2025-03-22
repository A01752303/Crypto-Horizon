using UnityEngine;
using cherrydev;

public class dialogNivel1 : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehavior;
    [SerializeField] private DialogNodeGraph dialogGraph;

    private void Start()
    {
        dialogBehavior.StartDialog(dialogGraph);    
    }

}
