using UnityEngine;

public class EnemyMovementPatrol : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador aquí en el Inspector
    public float speed = 3f; // Velocidad de movimiento del enemigo (ajustable en el Inspector)
    public float changeDirectionTime = 2f; // Tiempo antes de cambiar de dirección
    public float patrolRadius = 10f; // Radio de patrullaje alrededor de la posición inicial
    public Vector3 patrolCenter; // Posición central de patrullaje

    private Animator animator;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float timeSinceLastChange;

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
        rb = GetComponent<Rigidbody>(); // Obtiene el componente Rigidbody
        patrolCenter = transform.position; // Configura el centro de patrullaje como la posición inicial
        timeSinceLastChange = 0f;
        SetRandomDirection();
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changeDirectionTime)
        {
            SetRandomDirection();
            timeSinceLastChange = 0f;
        }

        // Mueve el enemigo
        Vector3 movement = moveDirection * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Asegúrate de que el enemigo permanezca dentro del radio
        Vector3 directionToCenter = patrolCenter - transform.position;
        if (directionToCenter.magnitude > patrolRadius)
        {
            transform.position = patrolCenter + directionToCenter.normalized * patrolRadius;
        }

        // Actualiza parámetros del Animator para el Blend Tree
        animator.SetFloat("VelX", moveDirection.x * speed);
        animator.SetFloat("VelY", moveDirection.z * speed);
    }

    private void SetRandomDirection()
    {
        // Genera una dirección aleatoria dentro del radio de patrullaje
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));
    }
}
