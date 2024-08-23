using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float moveSpeed = 5f; // Ajusta la velocidad de movimiento aquí
    [SerializeField] private float rotationSpeed = 700f; // Ajusta la velocidad de rotación aquí
    private Vector2 moveInput;
    private Vector2 rotationInput;
    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        _rigidbody.velocity = moveVector;

        if (rotationInput.magnitude > 0)
        {
            float yaw = rotationInput.x * rotationSpeed * Time.fixedDeltaTime;
            float pitch = rotationInput.y * rotationSpeed * Time.fixedDeltaTime;

            Quaternion deltaRotation = Quaternion.Euler(0, yaw, 0);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnRotation(InputAction.CallbackContext context) 
    {
        rotationInput = context.ReadValue<Vector2>();

    }
}