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
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input.Normalize();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * velocidad;
    }
}
