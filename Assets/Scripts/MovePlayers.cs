using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class MovePlayers : MonoBehaviour
{
    private Rigidbody _compRigidbody;
    public float moveSpeed;
    public float health = 100f;
    public float rotationSpeed;
    private float move;
    private float rotation;
    public TextMeshProUGUI healthText;
    private void Awake()
    {
        UpdateHealthText();
        _compRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
        Move(move);
        Rotation(rotation);
    }

    public void Move(float input )
    {
        Vector3 forwardMovement = transform.forward * input * moveSpeed;
        _compRigidbody.velocity = forwardMovement;
    }
    public void Rotation(float input)
    {
        float rotation = input * rotationSpeed * Mathf.Deg2Rad; 
        Vector3 angularVelocity = new Vector3(0.0f, rotation, 0.0f);
        _compRigidbody.angularVelocity = angularVelocity;
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<float>();
    }
    public void OnRotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<float>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthText();
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthText()
    {
        healthText.text = "Vida: " + health.ToString(); 
    }
    private void Die()
    {
        SceneManager.LoadScene("Level2");
        Debug.Log("Jugador ha muerto");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {

            TakeDamage(4);
            Destroy(collision.gameObject);
        }
    }
}
