using UnityEngine;

public class PlayerMovementB : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;  // Velocidad para caminar normal
    public float crouchSpeed = 2f;  // Velocidad para caminar agachado
    private bool isCrouching = false;

    public CapsuleCollider playerCollider; // Cambiar a CapsuleCollider
    public float crouchColliderHeight = 0.5f;
    public Vector3 crouchColliderCenter = new Vector3(0, 0.25f, 0);
    public float normalColliderHeight = 2f;
    public Vector3 normalColliderCenter = new Vector3(0, 0, 0);

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Alternar estado de agachado
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                animator.SetTrigger("StandUpTrigger");
                SetCollider(normalColliderHeight, normalColliderCenter);
            }
            else
            {
                animator.SetTrigger("CrouchTrigger");
                SetCollider(crouchColliderHeight, crouchColliderCenter);
            }
            isCrouching = !isCrouching;
        }

        // Obtener entrada del jugador
        float VelX = Input.GetAxis("Horizontal");
        float VelY = Input.GetAxis("Vertical");

        // Calcular la velocidad total
        float currentSpeed = isCrouching ? crouchSpeed : speed;
        Vector3 movement = new Vector3(VelX, 0, VelY) * currentSpeed * Time.deltaTime;

        // Actualizar parámetros del Animator
        animator.SetFloat("VelX", VelX);
        animator.SetFloat("VelY", VelY);
        animator.SetBool("IsCrouching", isCrouching);

        // Controlar el trigger de caminar agachado
        if (isCrouching && (VelX != 0 || VelY != 0))
        {
            animator.SetTrigger("CrouchWalkTrigger");
        }
        else if (isCrouching)
        {
            animator.ResetTrigger("CrouchWalkTrigger");
        }

        // Manejar caminar agachado con la tecla X
        if (isCrouching && Input.GetKey(KeyCode.X))
        {
            animator.SetBool("IsCrouchWalking", true);
        }
        else
        {
            animator.SetBool("IsCrouchWalking", false);
        }

        // Controlar la velocidad de movimiento usando Rigidbody
        rb.MovePosition(transform.position + movement);
    }

    private void SetCollider(float height, Vector3 center)
    {
        if (playerCollider != null)
        {
            playerCollider.height = height;
            playerCollider.center = center;
        }
    }
}

