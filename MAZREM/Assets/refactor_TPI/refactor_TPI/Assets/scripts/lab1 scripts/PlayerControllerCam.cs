using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    private void Start()
    {
        Debug.Log("Inicializando PlayerController");
        bool isFirstPerson = PlayerPrefs.GetInt("IsFirstPerson", 1) == 1;
        Debug.Log($"Modo de vista inicial (Primera Persona): {isFirstPerson}");
        SetViewMode(isFirstPerson);
    }

    public void SetViewMode(bool isFirstPerson)
    {
        Debug.Log($"Cambiando a {(isFirstPerson ? "primera" : "tercera")} persona");
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }

    public void SwitchToFirstPerson()
    {
        Debug.Log("Cambio solicitado a primera persona");
        SetViewMode(true);
        PlayerPrefs.SetInt("IsFirstPerson", 1);
    }

    public void SwitchToThirdPerson()
    {
        Debug.Log("Cambio solicitado a tercera persona");
        SetViewMode(false);
        PlayerPrefs.SetInt("IsFirstPerson", 0);
    }
}
