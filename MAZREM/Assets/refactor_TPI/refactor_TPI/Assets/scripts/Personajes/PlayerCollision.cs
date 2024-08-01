using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Aseg�rate de que las paredes tengan la etiqueta "Wall"
        {
            // Puedes agregar l�gica aqu� si quieres hacer algo especial cuando el jugador colisiona con una pared
            Debug.Log("Colisi�n con pared detectada");
        }
    }
}
