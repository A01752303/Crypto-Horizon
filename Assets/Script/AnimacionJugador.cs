using UnityEngine;

public class AnimacionJugador : MonoBehaviour
{

    [SerializeField] private string loadIdle;
    [SerializeField] private string loadCaminar;
    private Animator an;
    private Vector2 direccion;
    private SpriteRenderer sr;
    private MovimientoJugador movimientoJugador;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        an = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarLayer();
        
        direccion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movimientoJugador.enMov == false)
        {
            return;
        }

        an.SetFloat("X", direccion.x);
        an.SetFloat("Y", direccion.y);

        if (direccion.x != 0)
        {
            sr.flipX = direccion.x < 0;
        }
    }

    private void ActivarLayer(string nombreLayer)
    {
        for (int i = 0; i < an.layerCount; i++)
        {
            an.SetLayerWeight(i, 0);
        }

        an.SetLayerWeight(an.GetLayerIndex(nombreLayer), 1);
    }

    private void ActualizarLayer()
    {
        if (movimientoJugador.enMov)
        {
            ActivarLayer(loadCaminar);
        }
        else
        {
            ActivarLayer(loadIdle);
        }
    }
}
