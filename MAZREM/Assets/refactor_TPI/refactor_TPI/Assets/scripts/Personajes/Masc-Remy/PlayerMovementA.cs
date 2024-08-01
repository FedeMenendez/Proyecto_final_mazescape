using UnityEngine;

public class PlayerMovementA : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;  // Velocidad para caminar normal
    public float crouchSpeed = 2f;  // Velocidad para caminar agachado
    private bool isCrouching = false;

    private void Update()
    {
        // Alternar estado de agachado
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                animator.SetTrigger("StandUpTrigger"); // Activar trigger para levantarse
                animator.SetBool("IsCrouching", false);
                animator.SetTrigger("CrouchWalkTrigger"); // Desactivar trigger de caminar agachado
            }
            else
            {
                animator.SetTrigger("CrouchTrigger"); // Activar trigger para agacharse
                animator.SetBool("IsCrouching", true);
                animator.SetTrigger("CrouchWalkTrigger"); // Activar trigger de caminar agachado
            }
            isCrouching = !isCrouching;
        }

        // Obtener entrada del jugador
        float VelX = Input.GetAxis("Horizontal");
        float VelY = Input.GetAxis("Vertical");

        // Calcular la velocidad total
        float currentSpeed = isCrouching ? crouchSpeed : speed;
        Vector2 velocity = new Vector2(VelX, VelY);
        float speedMagnitude = velocity.magnitude * currentSpeed;

        // Actualizar parámetros del Animator
        animator.SetFloat("VelX", VelX);
        animator.SetFloat("VelY", VelY);
    }
}
