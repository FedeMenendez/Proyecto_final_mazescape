using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Asigna el objeto jugador aquí en el Inspector
    public float speed = 3f; // Velocidad de movimiento del enemigo
    public float detectionRadius = 10f; // Radio de detección para seguir al jugador
    public float attackRadius = 2f; // Radio en el cual el enemigo atacará
    public float patrolRadius = 25f; // Radio de patrullaje alrededor de la posición inicial
    public float changeDirectionTime = 2f; // Tiempo antes de cambiar de dirección

    public Animator animator; // Asigna el Animator aquí en el Inspector

    private Rigidbody rb;
    private Vector3 patrolCenter;
    private Vector3 moveDirection;
    private float timeSinceLastChange;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        patrolCenter = transform.position; // Configura el centro de patrullaje como la posición inicial
        timeSinceLastChange = 0f;
        SetRandomDirection();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToPatrolCenter = Vector3.Distance(transform.position, patrolCenter);

        if (distanceToPlayer < detectionRadius)
        {
            // Persigue al jugador si está dentro del rango de detección
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;

            // Actualiza el parámetro Speed para el Animator
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Está moviéndose
                if (distanceToPlayer <= attackRadius)
                {
                    animator.SetBool("IsAttacking", true); // Activa la animación de ataque
                }
                else
                {
                    animator.SetBool("IsAttacking", false); // No está atacando
                }
            }

            rb.MovePosition(transform.position + movement);
        }
        else if (distanceToPatrolCenter <= patrolRadius)
        {
            // Patrulla si el jugador está fuera del rango de detección
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= changeDirectionTime) // Cambia de dirección cada 'changeDirectionTime' segundos
            {
                SetRandomDirection();
                timeSinceLastChange = 0f;
            }

            Vector3 movement = moveDirection * speed * Time.deltaTime;

            // Asegúrate de que el enemigo permanezca dentro del radio de patrullaje
            Vector3 directionToCenter = patrolCenter - transform.position;
            if (directionToCenter.magnitude > patrolRadius)
            {
                transform.position = patrolCenter + directionToCenter.normalized * patrolRadius;
            }

            rb.MovePosition(transform.position + movement);

            // Actualiza el parámetro Speed para el Animator
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Está moviéndose
                animator.SetBool("IsAttacking", false); // No está atacando
            }
        }
        else
        {
            // Detén el movimiento si está demasiado cerca del centro de patrullaje
            if (animator != null)
            {
                animator.SetBool("Speed", false); // No está moviéndose
                animator.SetBool("IsAttacking", false); // No está atacando
            }
        }
    }

    private void SetRandomDirection()
    {
        // Genera una dirección aleatoria dentro del radio de patrullaje
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));
    }
}
