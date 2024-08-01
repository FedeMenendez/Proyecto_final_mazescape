using UnityEngine;

public class CatAI : MonoBehaviour
{
    public Transform player; // Asigna el objeto jugador aquí en el Inspector
    public float speed = 2f; // Velocidad de movimiento del gato
    public float detectionRadius = 10f; // Radio de detección para seguir al jugador
    public float patrolRadius = 15f; // Radio de patrullaje alrededor de la posición inicial
    public Vector3 patrolCenter; // Posición central de patrullaje

    public Animator animator; // Asigna el Animator aquí en el Inspector

    private Rigidbody rb;
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
            // Persigue al jugador
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;

            rb.MovePosition(transform.position + movement);

            // Actualiza el parámetro Speed para el Animator
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Está moviéndose
            }
        }
        else
        {
            // Patrulla si el jugador está fuera del rango de detección
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Está moviéndose
            }

            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= 2f) // Cambia de dirección cada 2 segundos
            {
                SetRandomDirection();
                timeSinceLastChange = 0f;
            }

            Vector3 movement = moveDirection * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);

            // Asegúrate de que el gato permanezca dentro del radio de patrullaje
            Vector3 directionToCenter = patrolCenter - transform.position;
            if (directionToCenter.magnitude > patrolRadius)
            {
                transform.position = patrolCenter + directionToCenter.normalized * patrolRadius;
            }

            // Detiene la animación de caminar si el gato está inactivo
            if (animator != null)
            {
                animator.SetBool("Speed", false); // No está moviéndose
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
