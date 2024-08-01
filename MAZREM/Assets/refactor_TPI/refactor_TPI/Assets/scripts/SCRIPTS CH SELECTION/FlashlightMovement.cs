using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlashlightMovement : MonoBehaviour
{
    public float rotationSpeed = 200f; // Velocidad de rotaci�n de la linterna
    public float rayDistance = 10f;    // Distancia m�xima del rayo
    public LayerMask characterLayer;   // Capa que contiene los personajes

    private Camera cam;                // C�mara para lanzar el rayo

    void Start()
    {
        cam = Camera.main; // Obtener la c�mara principal
        if (cam == null)
        {
            Debug.LogError("No se encontr� la c�mara principal.");
        }
    }

    void Update()
    {
        // Rotaci�n de la linterna
        if (Input.GetMouseButton(1)) // Bot�n derecho del mouse
        {
            float mouseX = Input.GetAxis("Mouse X");

            Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
            transform.Rotate(rotation);
        }

        // Lanzar rayo al hacer clic izquierdo
        if (Input.GetMouseButtonDown(0)) // Bot�n izquierdo del mouse
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
        // l�gica para seleccionar el personaje
        //  almacenar el personaje seleccionado en PlayerPrefs o en un script de gesti�n
        PlayerPrefs.SetString("SelectedCharacter", character.name);
        Debug.Log("Personaje seleccionado almacenado: " + character.name);
    }
}


