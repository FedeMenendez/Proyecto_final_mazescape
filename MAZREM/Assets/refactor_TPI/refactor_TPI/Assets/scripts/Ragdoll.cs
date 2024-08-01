using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RagdollController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private bool isRagdoll = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        // Desactivar el modo Ragdoll al inicio
        SetRagdollState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRagdoll = !isRagdoll;
            SetRagdollState(isRagdoll);
        }
    }

    public void SetRagdollState(bool state)
    {
        foreach (var body in ragdollBodies)
        {
            body.isKinematic = !state;
        }

        foreach (var collider in ragdollColliders)
        {
            if (collider != GetComponent<Collider>()) // Dejar el collider principal del personaje activo
            {
                collider.enabled = state;
            }
        }

        animator.enabled = !state; // Desactivar el Animator cuando esté en modo Ragdoll
    }
}

