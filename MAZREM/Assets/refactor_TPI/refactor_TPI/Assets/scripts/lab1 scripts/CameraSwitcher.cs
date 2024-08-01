using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera;  // Cámara en primera persona
    public Camera thirdPersonCamera;  // Cámara en tercera persona

    private void Start()
    {
        // Asegura que la cámara en primera persona esté activada al inicio
        Debug.Log("Inicializando CameraSwitcher");
        SetCameraActive(firstPersonCamera);
    }

    public void SwitchCamera()
    {
        Debug.Log("Intentando cambiar la cámara");
        if (firstPersonCamera.gameObject.activeSelf)
        {
            Debug.Log("Cambiando a tercera persona");
            SetCameraActive(thirdPersonCamera);
        }
        else
        {
            Debug.Log("Cambiando a primera persona");
            SetCameraActive(firstPersonCamera);
        }
    }

    private void SetCameraActive(Camera activeCamera)
    {
        Debug.Log($"Activando cámara: {activeCamera.name}");
        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(false);
        activeCamera.gameObject.SetActive(true);
    }
}
