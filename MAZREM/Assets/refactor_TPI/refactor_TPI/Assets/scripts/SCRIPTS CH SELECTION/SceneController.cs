using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // M�todo para comenzar el juego y cargar la escena del nivel 1
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

    // M�todo para cambiar de escena
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // M�todo para volver al men� principal
    public void BackToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene(0); // Cambia a la escena del men� principal
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para la edici�n en Unity
#else
        Application.Quit(); // Para la ejecuci�n en el juego
#endif
    }

    // M�todo para confirmar la selecci�n del personaje
    public void ConfirmSelection()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", -1);
        if (selectedCharacterIndex != -1)
        {
            PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
            Debug.Log("Character selection saved.");
            // Aqu� solo guardamos la selecci�n, no cargamos la escena.
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }
    }
}
