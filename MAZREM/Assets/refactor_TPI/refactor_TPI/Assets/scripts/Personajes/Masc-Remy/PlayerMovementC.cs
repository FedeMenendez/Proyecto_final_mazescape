using UnityEngine;

public class PlayerMovementC: MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 10f; // Fuerza de salto
    private bool isGrounded; // Para verificar si el jugador está en el suelo

    private Rigidbody rb;
    private Animator anim;

    // Parámetros de velocidad
    public float velx = 0f;
    public float vely = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");

        // Actualiza los parámetros de velocidad
        velx = moveInputHorizontal * speed;
        vely = rb.velocity.y;

        // Aplica el movimiento
        Vector3 movement = new Vector3(velx, 0.0f, moveInputVertical * speed);
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Control de la animación de correr
        anim.SetBool("IsRunning", Mathf.Abs(moveInputHorizontal) > 0.1f || Mathf.Abs(moveInputVertical) > 0.1f);

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Z)) // Usar tecla Z para saltar
        {
            anim.SetTrigger("Jump");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Aplicar fuerza de salto
            isGrounded = false; // El jugador ya no está en el suelo
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el jugador está tocando el suelo
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
