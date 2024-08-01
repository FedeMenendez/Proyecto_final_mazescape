using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para acceder a los componentes UI

public class GameUIManager : MonoBehaviour
{
    public Button exitButton; // Asigna el bot�n de salir desde el Inspector
    public Button pauseButton; // Asigna el bot�n de pausar desde el Inspector

    private bool isPaused = false;

    void Start()
    {
        // Aseg�rate de que los botones llamen a las funciones correctas
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitToMainMenu);
        }
        else
        {
            Debug.LogWarning("Exit button is not assigned!");
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
        else
        {
            Debug.LogWarning("Pause button is not assigned!");
        }
    }

    void ExitToMainMenu()
    {
        // Cargar la escena principal (aseg�rate de que el nombre de la escena sea correcto)
        SceneManager.LoadScene("MainMenu");
    }

    void TogglePause()
    {
        if (isPaused)
        {
            // Si est� pausado, reanudar el juego
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            // Si est� en juego, pausar el juego
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
