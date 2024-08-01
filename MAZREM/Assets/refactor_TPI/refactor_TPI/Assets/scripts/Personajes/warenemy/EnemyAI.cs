using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Asigna el objeto jugador aqu� en el Inspector
    public float speed = 3f; // Velocidad de movimiento del enemigo
    public float detectionRadius = 10f; // Radio de detecci�n para seguir al jugador
    public float attackRadius = 2f; // Radio en el cual el enemigo atacar�
    public float patrolRadius = 25f; // Radio de patrullaje alrededor de la posici�n inicial
    public float changeDirectionTime = 2f; // Tiempo antes de cambiar de direcci�n

    public Animator animator; // Asigna el Animator aqu� en el Inspector

    private Rigidbody rb;
    private Vector3 patrolCenter;
    private Vector3 moveDirection;
    private float timeSinceLastChange;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        patrolCenter = transform.position; // Configura el centro de patrullaje como la posici�n inicial
        timeSinceLastChange = 0f;
        SetRandomDirection();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToPatrolCenter = Vector3.Distance(transform.position, patrolCenter);

        if (distanceToPlayer < detectionRadius)
        {
            // Persigue al jugador si est� dentro del rango de detecci�n
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;

            // Actualiza el par�metro Speed para el Animator
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Est� movi�ndose
                if (distanceToPlayer <= attackRadius)
                {
                    animator.SetBool("IsAttacking", true); // Activa la animaci�n de ataque
                }
                else
                {
                    animator.SetBool("IsAttacking", false); // No est� atacando
                }
            }

            rb.MovePosition(transform.position + movement);
        }
        else if (distanceToPatrolCenter <= patrolRadius)
        {
            // Patrulla si el jugador est� fuera del rango de detecci�n
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= changeDirectionTime) // Cambia de direcci�n cada 'changeDirectionTime' segundos
            {
                SetRandomDirection();
                timeSinceLastChange = 0f;
            }

            Vector3 movement = moveDirection * speed * Time.deltaTime;

            // Aseg�rate de que el enemigo permanezca dentro del radio de patrullaje
            Vector3 directionToCenter = patrolCenter - transform.position;
            if (directionToCenter.magnitude > patrolRadius)
            {
                transform.position = patrolCenter + directionToCenter.normalized * patrolRadius;
            }

            rb.MovePosition(transform.position + movement);

            // Actualiza el par�metro Speed para el Animator
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Est� movi�ndose
                animator.SetBool("IsAttacking", false); // No est� atacando
            }
        }
        else
        {
            // Det�n el movimiento si est� demasiado cerca del centro de patrullaje
            if (animator != null)
            {
                animator.SetBool("Speed", false); // No est� movi�ndose
                animator.SetBool("IsAttacking", false); // No est� atacando
            }
        }
    }

    private void SetRandomDirection()
    {
        // Genera una direcci�n aleatoria dentro del radio de patrullaje
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));
    }
}
