using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemiesShooterController : MonoBehaviour
{
    public SpawnerEnemy spawnerEnemy;

    public State state;
    public float FrameRate = 0;
    public float Rate = 1;
    public Enemies enemiesData;
    //public float Speed;
    public float Damage;
    public float Life;
    public float RotationSpeed;
    public float RadioScanear;
    public LayerMask layerenemy;
    public Vector3 RandomPosition;
    public MovePlayers Player;
    private NavMeshAgent Agent;
    public float RadioPatrullaje;
    public LayerMask layerwall;
    private bool canAttack = true;

    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto desde donde se disparará la bala
    public float bulletSpeed = 10f; // Velocidad de la bala

    private void Awake()
    {
        Life = enemiesData.Life;
        Damage = enemiesData.damage;
        RotationSpeed = enemiesData.Rotationspeed;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        RandomPosition = CalcularPosicionRandom();
        state = State.Patrullar;
    }

    public void Update()
    {
        UpdateScaner();
        switch (state)
        {
            case State.MoverEnemigo:
                MoveEnemi();
                break;
            case State.Atacar:
                Atacar();
                break;
            case State.Patrullar:
                Patrullar();
                break;
            case State.Esperar:
                Esperar();
                break;
            case State.Muerto:
                Die();
                break;

            case State.None:
                break;
            default:
                break;
        }
    }
    public void RotatePlayer()
    {
        if (Player != null)
        {
            RotateToPosition(Player.transform.position);
        }
    }
    public void MoveEnemi()
    {
        if (Player != null)
        {
            MoveToPosition(Player.transform.position);
            RandomPosition = Player.transform.position;
            Debug.Log("MoveEnemi");
            float Distance = (Player.transform.position - transform.position).magnitude;
            if (Distance < 15f) 
            {
                state = State.Atacar;
                return;
            }
        }
        else
        {
            state = State.Patrullar;
        }
    }

    void UpdateScaner()
    {
        if (FrameRate > Rate)
        {
            FrameRate = 0;
            Scanear();
        }
        FrameRate += Time.deltaTime;
    }

    bool InSigh(Transform agent)
    {
        RaycastHit hit;
        Vector3 start = agent.position + Vector3.up;
        Vector3 end = transform.position + Vector3.up;

        if (Physics.Linecast(start, end, layerwall))
        {
            return false;
        }
        return true;
    }

    void Scanear()
    {
        Player = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, RadioScanear, layerenemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject agente = colliders[i].gameObject;
            MovePlayers p = agente.GetComponent<MovePlayers>();
            if (p != null && InSigh(p.transform))
            {
                Player = p;
            }
        }
    }

    void Atacar()
    {
        if (Player != null && canAttack) // Solo atacar si se puede atacar
        {
            RotatePlayer();
            Debug.Log("Atacar y Disparar");

            // Disparar bala
            ShootBullet();

            float Distance = (Player.transform.position - transform.position).magnitude;

            if (Distance <= 15f) // Asegurarse de que el jugador esté al alcance
            {  
                StartCoroutine(AttackCooldown()); // Iniciar el cooldown  
            }
            else
            {
                state = State.MoverEnemigo; // Si el jugador está fuera de alcance, cambiar al estado de mover
            }
        }
        else if (!canAttack)
        {
            state = State.Esperar; // Cambiar al estado de esperar durante el cooldown
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false; // Desactivar la capacidad de atacar
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        canAttack = true; // Reactivar la capacidad de atacar
        state = State.MoverEnemigo; // Volver a mover o atacar después del cooldown
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instanciar la bala
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                // Dirigir la bala hacia el jugador
                Vector3 direction = (Player.transform.position - bulletSpawnPoint.position).normalized;
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
    }

    void Patrullar()
    {
        if (Player != null)
        {
            state = State.MoverEnemigo;
            return;
        }

        float Distance = (RandomPosition - transform.position).magnitude;
        if (Distance <= 5f)
        {
            RandomPosition = CalcularPosicionRandom();
        }
        else
        {
            MoveToPosition(RandomPosition);
        }
        Debug.Log("Patrullar");
    }

    void Esperar()
    {
        Debug.Log("Esperar");
    }

    void MoveToPosition(Vector3 Position)
    {
        RotatePlayer();
        Agent.SetDestination(Position);
    }

    void RotateToPosition(Vector3 Position)
    {
        Vector3 direction = (Position - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    private Vector3 CalcularPosicionRandom()
    {
        for (int i = 0; i < 30; ++i)
        {
            Vector3 targetPosition = transform.position + Random.insideUnitSphere * RadioPatrullaje;
            targetPosition.y = transform.position.y;
            NavMeshHit hit;

            // Busca el punto más cercano en el NavMesh a la posición del objetivo
            if (NavMesh.SamplePosition(targetPosition, out hit, RadioPatrullaje, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                targetPosition.y = transform.position.y;
                return targetPosition;
            }
        }
        return transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(RandomPosition, 0.5f);
        Gizmos.DrawLine(transform.position, RandomPosition);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioScanear);
        if (Player != null)
        {
            Gizmos.DrawLine(transform.position, Player.transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (WaveManager.Instance != null)
        {
            Debug.Log("Enemy destroyed, notifying WaveManager.");
            WaveManager.Instance.EnemyDestroyed();
        }
        else
        {
            Debug.LogError("WaveManager.Instance is null. Make sure WaveManager is in the scene.");
        }
        
        Destroy(this.gameObject);
    }
  
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(2);
            Destroy(collision.gameObject);
        }
    }
}
