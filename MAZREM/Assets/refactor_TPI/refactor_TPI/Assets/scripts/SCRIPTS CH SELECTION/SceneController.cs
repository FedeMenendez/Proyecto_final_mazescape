using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Método para comenzar el juego y cargar la escena del nivel 1
    public void StartGame()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", -1);
        if (selectedCharacterIndex != -1)
        {
            Debug.Log("Character selected. Starting game...");
            SceneManager.LoadScene(2); // Cambia a la escena de nivel 1
        }
        else
        {
            Debug.LogWarning("No character selected! Cannot start the game.");
        }
    }

    // Método para cambiar de escena
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Método para volver al menú principal
    public void BackToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene(0); // Cambia a la escena del menú principal
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para la edición en Unity
#else
        Application.Quit(); // Para la ejecución en el juego
#endif
    }

    // Método para confirmar la selección del personaje
    public void ConfirmSelection()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", -1);
        if (selectedCharacterIndex != -1)
        {
            PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
            Debug.Log("Character selection saved.");
            // Aquí solo guardamos la selección, no cargamos la escena.
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }
    }
}
