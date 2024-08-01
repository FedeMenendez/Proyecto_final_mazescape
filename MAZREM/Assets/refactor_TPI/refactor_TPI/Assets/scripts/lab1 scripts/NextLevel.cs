using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextSceneIndex = 3; // Índice de la escena a cargar 

    private bool playerInExitTrigger = false; // Bandera para saber si el jugador está en el trigger de salida

    void Update()
    {
        // Verifica si la bandera de entrada en el trigger es verdadera
        if (playerInExitTrigger)
        {
            Debug.Log("Player is in exit trigger. Loading next level...");
            LoadNextLevel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered exit trigger.");
            playerInExitTrigger = true; // Marca que el jugador está en el área de salida
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que ha salido del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited exit trigger.");
            playerInExitTrigger = false; // Marca que el jugador ha salido del área de salida
        }
    }

    void LoadNextLevel()
    {
        // Carga la siguiente escena usando el índice
        Debug.Log("Loading scene index: " + nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
