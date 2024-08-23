using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float moveSpeed = 5f; // Ajusta la velocidad de movimiento aquí
    [SerializeField] private float rotationSpeed = 20f; // Ajusta la velocidad de rotación aquí
    //[SerializeField] private CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual de Cinemachine
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool run;
    private bool jump;
    private const float _threshold = 0.01f;

    public  GameObject FollowCamera ;
    public float CameraAngleOverride;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    public float BottomClamp;
    public float TopClamp;
    public float SencitivityMouse;

    public  Transform Arm;
    public Transform Aim;
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    public bool isGrounded; // Si el jugador está en el suelo
    public float jumpForce = 10f; // Fuerza de salto
    public float groundCheckDistance = 0.2f; // Distancia para verificar si estamos en el suelo
    public LayerMask groundLayer; // Capa para verificar el suelo
    public float Stamina = 50;
    private void Awake()
    {
        m_Cam = FollowCamera.transform;
    }
    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }
    IEnumerator RunStamina()
    {
        while (Stamina>0)
        {
            Stamina--;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    private void FixedUpdate()
    {

        Arm.LookAt(Aim);

        if (m_Cam != null)
        {
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = moveInput.y * m_CamForward + moveInput.x * m_Cam.right;

        }



        Quaternion rot = Quaternion.LookRotation(m_CamForward);
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation,rot,Time.deltaTime * rotationSpeed);

        // Verifica si estamos en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);


        //Vector3 moveVector = new Vector3( moveInput.x, 0, moveInput.y) * moveSpeed;
        if (m_Move != Vector3.zero)
        {

            if(!jump && isGrounded)
            {
                float runSpped = run ? moveSpeed * 2 : moveSpeed;
                _rigidbody.velocity = new Vector3(m_Move.x, _rigidbody.velocity.y, m_Move.z) * runSpped;

            }

            // Manejo del salto
            if (jump && isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jump = false; // Resetea el salto
            }
        }

        

    }     
    private void LateUpdate()
    {
         
        CameraRotation();
    }
    private void CameraRotation()
    {
        
        // if there is an input and camera position is not fixed
        if (lookInput.sqrMagnitude >= _threshold )
        {
            //Don't multiply mouse input by Time.deltaTime;
            _cinemachineTargetYaw += lookInput.x * Time.deltaTime * SencitivityMouse;
            _cinemachineTargetPitch -= lookInput.y * Time.deltaTime * SencitivityMouse;
           // Debug.Log("_cinemachineTargetYaw:" + _cinemachineTargetYaw+ " _cinemachineTargetPitch: "+ _cinemachineTargetPitch);
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
        
        // Cinemachine will follow this target
        FollowCamera.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
     
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context) 
    {
        lookInput = context.ReadValue<Vector2>();
  
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed)
        run = true;
        else
            if (context.canceled)
            run = false;


    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jump = true;
    }
}