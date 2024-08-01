using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage; // Arrastra aquí la imagen de desvanecimiento desde el Inspector
    public float fadeDuration = 1f; // Duración del efecto de desvanecimiento en segundos
    public Collider exitTrigger; // Asigna el collider de la salida desde el Inspector
    public int nextSceneIndex = 3; // Índice de la escena a cargar

    private bool isTransitioning = false;

    void Start()
    {
        // Asegúrate de que la imagen esté completamente transparente al inicio
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
        }
    }

    void Update()
    {
        // No hacer nada si ya estamos en una transición
        if (isTransitioning)
        {
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOutAndLoadScene(nextSceneIndex));
        }
    }

    private IEnumerator FadeOutAndLoadScene(int sceneIndex)
    {
        isTransitioning = true;

        // Fade out
        yield return StartCoroutine(FadeToAlpha(1));

        // Load the scene
        SceneManager.LoadScene(sceneIndex);

        // Fade in
        yield return StartCoroutine(FadeToAlpha(0));

        isTransitioning = false;
    }

    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Asegúrate de que el color final sea el objetivo
        Color finalColor = fadeImage.color;
        finalColor.a = targetAlpha;
        fadeImage.color = finalColor;
    }
}
