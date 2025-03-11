using UnityEngine;
using cherrydev;

public class DialogInicio : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehavior;
    [SerializeField] private DialogNodeGraph dialogGraph;

    private void Start()
    {
        dialogBehavior.StartDialog(dialogGraph);
    }
}
