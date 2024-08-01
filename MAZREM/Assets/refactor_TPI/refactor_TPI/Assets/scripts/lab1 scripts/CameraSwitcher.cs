using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera;  // C�mara en primera persona
    public Camera thirdPersonCamera;  // C�mara en tercera persona

    private void Start()
    {
        // Asegura que la c�mara en primera persona est� activada al inicio
        Debug.Log("Inicializando CameraSwitcher");
        SetCameraActive(firstPersonCamera);
    }

    public void SwitchCamera()
    {
        Debug.Log("Intentando cambiar la c�mara");
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
        Debug.Log($"Activando c�mara: {activeCamera.name}");
        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(false);
        activeCamera.gameObject.SetActive(true);
    }
}
