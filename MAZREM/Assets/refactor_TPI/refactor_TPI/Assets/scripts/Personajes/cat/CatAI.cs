using UnityEngine;

public class CatAI : MonoBehaviour
{
    public Transform player; // Asigna el objeto jugador aqu� en el Inspector
    public float speed = 2f; // Velocidad de movimiento del gato
    public float detectionRadius = 10f; // Radio de detecci�n para seguir al jugador
    public float patrolRadius = 15f; // Radio de patrullaje alrededor de la posici�n inicial
    public Vector3 patrolCenter; // Posici�n central de patrullaje

    public Animator animator; // Asigna el Animator aqu� en el Inspector

    private Rigidbody rb;
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
            // Persigue al jugador
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;

            rb.MovePosition(transform.position + movement);

            // Actualiza el par�metro Speed para el Animator
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Est� movi�ndose
            }
        }
        else
        {
            // Patrulla si el jugador est� fuera del rango de detecci�n
            if (animator != null)
            {
                animator.SetBool("Speed", true); // Est� movi�ndose
            }

            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= 2f) // Cambia de direcci�n cada 2 segundos
            {
                SetRandomDirection();
                timeSinceLastChange = 0f;
            }

            Vector3 movement = moveDirection * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);

            // Aseg�rate de que el gato permanezca dentro del radio de patrullaje
            Vector3 directionToCenter = patrolCenter - transform.position;
            if (directionToCenter.magnitude > patrolRadius)
            {
                transform.position = patrolCenter + directionToCenter.normalized * patrolRadius;
            }

            // Detiene la animaci�n de caminar si el gato est� inactivo
            if (animator != null)
            {
                animator.SetBool("Speed", false); // No est� movi�ndose
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
