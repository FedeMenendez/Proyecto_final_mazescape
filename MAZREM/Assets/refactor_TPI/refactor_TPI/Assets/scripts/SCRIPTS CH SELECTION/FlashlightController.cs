using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public Camera cam;  
    private bool isOn = true;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        if (flashlight == null)
        {
            Debug.LogError("No Light component found on the flashlight.");
        }

        if (cam == null)
        {
            cam = Camera.main; // Opcional: asigna la cámara principal si no se asigna manualmente
        }

        if (cam == null)
        {
            Debug.LogError("No Camera component assigned or found.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
