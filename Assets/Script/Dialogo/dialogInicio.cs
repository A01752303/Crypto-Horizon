using UnityEngine;
using cherrydev;

public class DialogInicio : MonoBehaviour
{
    public MovimientoJugador moveCharacter;
    public enterLevel enterE;
    [SerializeField] private DialogBehaviour dialogBehavior;
    [SerializeField] private DialogNodeGraph dialogGraph;

    private void Start()
    {
        dialogBehavior.StartDialog(dialogGraph);
        
        if (moveCharacter != null)
        {
            moveCharacter.enabled = false;
        }
        if (enterE != null)
        {
            enterE.enabled = false;
        }

        dialogBehavior.BindExternalFunction("turnOnMoveCharacter", turnOnMoveCharacter);
        dialogBehavior.BindExternalFunction("turnOnEKey", turnOnEKey);
    }

    public void turnOnMoveCharacter()
    {
        if (moveCharacter != null)
        {
            moveCharacter.enabled = true;
            Debug.Log("Script Activado");
        }

    }

    public void turnOnEKey()
    {
        if (enterE != null)
        {
            enterE.enabled = true;
            Debug.Log("Script Activado");
        }

    }
}
