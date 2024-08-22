using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public Enemies enemiesData;
    public float Speed;
    public float Damage;
    public float Life;
    public float RotationSpeed;
    private bool RangeDetec;
    private Transform _compTransform;
    public MovePlayers Player;
    public bool isCooldownActive;
    private void Awake()
    {
        Life = enemiesData.Life;
        Speed = enemiesData.speed;
        Damage = enemiesData.damage;
        RotationSpeed = enemiesData.Rotationspeed;

    }

    public void Update()
    {
        if (RangeDetec && _compTransform != null)
        {
            MoveEnemi();
        }
    }

    public void MoveEnemi()
    {
        RotatePlayer();
        Vector3 direction = (_compTransform.position - transform.position).normalized;
        transform.position += direction * enemiesData.speed * Time.deltaTime;
    }
    public void RotatePlayer()
    {
        Vector3 direction = _compTransform.position - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _compTransform = other.transform;
            RangeDetec = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RangeDetec = false;
        }
    }
    IEnumerator AfterCollision()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(0.5f);
        isCooldownActive = false;
    }
    public void TakeDamage(float damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }
        if (collision.gameObject.tag == "Player")
        {
            if (_compTransform != null && !isCooldownActive)
            {
                Player.ChangeLife(-enemiesData.damage);
                Vector3 pushDirection = collision.transform.position - transform.position;              
                StartCoroutine(AfterCollision());
            }
        }
    }
}
