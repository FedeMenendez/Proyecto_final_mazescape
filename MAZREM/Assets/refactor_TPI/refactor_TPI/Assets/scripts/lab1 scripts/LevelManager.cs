using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Asigna los prefabs desde el Inspector
    public GameObject projectilePrefab; // Prefab de la bolita (proyectil)
    public float projectileSpeed = 40f; // Velocidad del proyectil

    private GameObject playerCharacter;

    void Start()
    {
        // Obtiene el índice del personaje seleccionado desde PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0); // Valor predeterminado 0 si no hay selección

        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            // Instanciar el personaje seleccionado en la posición deseada
            playerCharacter = Instantiate(characterPrefabs[selectedCharacterIndex], Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character index or no character selected.");
        }
    }

    void Update()
    {
        // Verifica si la tecla Shift se está presionando
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (playerCharacter != null && projectilePrefab != null)
        {
            // Crear la bolita
            GameObject projectile = Instantiate(projectilePrefab, playerCharacter.transform.position, Quaternion.identity);

            // Obtener el Rigidbody2D del proyectil para aplicarle fuerza
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calcular la dirección del disparo
                Vector2 direction = playerCharacter.transform.right; // Puedes ajustar esta dirección según sea necesario
                rb.velocity = direction * projectileSpeed;
            }
        }
    }
}
