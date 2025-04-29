using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidad = 5f;
    private Vector2 input;
    public bool enMov => input.magnitude > 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Obtener la entrada del jugador para moverlo
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        // Solo guardar la posición cuando el jugador se mueva
        if (enMov)
        {
            gameManager.Instance.GuardarPosicionJugador(transform.position);
        }
    }

    private void FixedUpdate()
    {
        // Movimiento del jugador basado en la física
        rb.linearVelocity = input * velocidad;
    }

    // Método para restaurar la posición cuando se cambia de escena (sin actualización al inicio)
    private void OnLevelWasLoaded(int level)
    {
        // Restaurar la posición guardada solo cuando se cargue una nueva escena
        gameManager.Instance.RestaurarPosicionJugador(gameObject);
    }

    // Método para restablecer la posición al detener la simulación
    private void OnApplicationQuit()
    {
        // Si deseas resetear la posición cuando se cierre la aplicación, puedes hacerlo aquí
        // Por ejemplo, restaurando la posición inicial al comienzo de la simulación
        transform.position = Vector3.zero;
    }
}
