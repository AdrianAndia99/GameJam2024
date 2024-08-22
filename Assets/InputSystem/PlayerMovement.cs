using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float moveSpeed = 5f; // Ajusta la velocidad de movimiento aquí
    [SerializeField] Vector2 move;
    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        // Lee el valor del Vector2 del input
        Vector2 movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();

        // Calcula el movimiento en el eje X y Z
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed;

        // Aplica el movimiento al Rigidbody
        _rigidbody.velocity = move;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
}