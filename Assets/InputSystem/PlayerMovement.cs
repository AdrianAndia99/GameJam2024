using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    private CinemachineVirtualCamera _virtualCamera; // Referencia a la c�mara del jugador
    [SerializeField] private float moveSpeed = 5f; // Ajusta la velocidad de movimiento aqu�
    [SerializeField] private float rotationSpeed = 700f; // Ajusta la velocidad de rotaci�n aqu�
    private Vector2 moveInput;
    private Vector2 rotationInput;
    private void OnValidate()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }
    private void FixedUpdate()
    {
            // Obt�n la direcci�n hacia adelante y hacia los lados de la c�mara virtual
            Vector3 cameraForward = _virtualCamera.transform.forward;
            Vector3 cameraRight = _virtualCamera.transform.right;

            // Solo usa el plano horizontal para el movimiento
            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calcula la direcci�n del movimiento en el espacio de la c�mara
            Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;
            Vector3 moveVector = moveDirection * moveSpeed;

            // Aplica la velocidad al Rigidbody
            _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);

            // Calcula y aplica la rotaci�n en base al input del stick derecho
            if (rotationInput.magnitude > 0)
            {
                float yaw = rotationInput.x * rotationSpeed * Time.fixedDeltaTime;
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