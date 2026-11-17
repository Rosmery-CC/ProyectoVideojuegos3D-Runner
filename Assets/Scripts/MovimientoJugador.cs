using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 5f;
    public Transform camara;
    public Vector3 distanciaCamara = new Vector3(0f, 4f, -6f);
    public float limiteBorde = 9.5f;

    private Rigidbody rb;
    private bool enElSuelo = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento con WASD o flechas
        float moverX = Input.GetAxis("Horizontal");
        float moverZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moverX, 0f, moverZ) * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);

        // Salto con Espacio
        if (Input.GetKeyDown(KeyCode.Space) && enElSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enElSuelo = false;
        }

        // Limitar el borde izquierdo y derecho (Eje X)
        Vector3 posicionActual = transform.position;
        posicionActual.x = Mathf.Clamp(posicionActual.x, -limiteBorde, limiteBorde);
        transform.position = posicionActual;
    }

    // LateUpdate se ejecuta después de mover al jugador para que la cámara no tiemble
    void LateUpdate()
    {
        if (camara != null)
        {
            camara.position = transform.position + distanciaCamara;
            camara.LookAt(transform.position + Vector3.up * 1.5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        enElSuelo = true;
    }
}
