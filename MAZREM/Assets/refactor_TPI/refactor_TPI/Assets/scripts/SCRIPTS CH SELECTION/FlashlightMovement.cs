using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlashlightMovement : MonoBehaviour
{
    public float rotationSpeed = 200f; // Velocidad de rotación de la linterna
    public float rayDistance = 10f;    // Distancia máxima del rayo
    public LayerMask characterLayer;   // Capa que contiene los personajes

    private Camera cam;                // Cámara para lanzar el rayo

    void Start()
    {
        cam = Camera.main; // Obtener la cámara principal
        if (cam == null)
        {
            Debug.LogError("No se encontró la cámara principal.");
        }
    }

    void Update()
    {
        // Rotación de la linterna
        if (Input.GetMouseButton(1)) // Botón derecho del mouse
        {
            float mouseX = Input.GetAxis("Mouse X");

            Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
            transform.Rotate(rotation);
        }

        // Lanzar rayo al hacer clic izquierdo
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, characterLayer))
            {
                Debug.Log("Personaje seleccionado: " + hit.collider.gameObject.name);
                SelectCharacter(hit.collider.gameObject);
            }
        }
    }

    void SelectCharacter(GameObject character)
    {
        // lógica para seleccionar el personaje
        //  almacenar el personaje seleccionado en PlayerPrefs o en un script de gestión
        PlayerPrefs.SetString("SelectedCharacter", character.name);
        Debug.Log("Personaje seleccionado almacenado: " + character.name);
    }
}


