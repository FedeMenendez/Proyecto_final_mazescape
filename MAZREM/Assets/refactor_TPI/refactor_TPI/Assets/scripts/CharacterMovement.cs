using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 700f;

    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the Player object.");
        }
    }

    private void Update()
    {
        // Movimiento del personaje
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(movement * speed * Time.deltaTime);

        // Rotaci�n de la c�mara y el personaje
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        if (firstPersonCamera.gameObject.activeSelf)
        {
            // Rotaci�n de la c�mara en primera persona
            firstPersonCamera.transform.Rotate(-mouseY, 0, 0);
            transform.Rotate(0, mouseX, 0);
        }
        else if (thirdPersonCamera.gameObject.activeSelf)
        {
            // Rotaci�n de la c�mara en tercera persona
            thirdPersonCamera.transform.Rotate(-mouseY, 0, 0);
            transform.Rotate(0, mouseX, 0);
        }
    }
}

