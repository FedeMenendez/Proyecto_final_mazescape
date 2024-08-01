using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public float pulseSpeed = 1f; // Velocidad del pulso
    public float pulseAmount = 0.2f; // Cantidad de aumento y disminuci�n

    private Vector3 originalScale;
    private bool pulsing = true;

    void Start()
    {
        // Guarda la escala original del objeto
        originalScale = transform.localScale;

        // Verifica que la escala original sea v�lida
        if (originalScale.x <= 0 || originalScale.y <= 0 || originalScale.z <= 0)
        {
            Debug.LogError("Invalid initial scale detected. Please ensure the object's scale is positive.");
            originalScale = Vector3.one; // Usa una escala por defecto si es necesario
        }
    }

    void Update()
    {
        if (pulsing)
        {
            // Usa Mathf.PingPong para crear una animaci�n de pulso
            float scale = Mathf.PingPong(Time.time * pulseSpeed, pulseAmount) + 1;

            // Verifica que scale no sea NaN y es positivo
            if (!float.IsNaN(scale) && scale > 0)
            {
                transform.localScale = originalScale * scale;
            }
            else
            {
                Debug.LogWarning("Invalid scale value calculated. Scale: " + scale);
            }
        }
    }

    public void StopPulsing()
    {
        pulsing = false;
        transform.localScale = originalScale; // Restaura la escala original
    }

    public void StartPulsing()
    {
        pulsing = true;
    }
}
