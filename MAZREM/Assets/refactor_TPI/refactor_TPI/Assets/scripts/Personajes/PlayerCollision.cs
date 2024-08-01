using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Asegúrate de que las paredes tengan la etiqueta "Wall"
        {
            // Puedes agregar lógica aquí si quieres hacer algo especial cuando el jugador colisiona con una pared
            Debug.Log("Colisión con pared detectada");
        }
    }
}
