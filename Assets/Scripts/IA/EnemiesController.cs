using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum State
{
    MoverEnemigo,
    Atacar,
    Patrullar,
    Esperar,
    Muerto,
    None
}
public class EnemiesController : MonoBehaviour
{
    
    public State state;
    public float FrameRate = 0;
    public float Rate = 1;
    public Enemies enemiesData;
    public float Speed;
    public int Damage;
    public float Life;
    public float RotationSpeed;
    public float RadioScanear;
    public LayerMask layerenemy;
    public Vector3 RandomPosition;
    public HealthPlayer Player;
    //public bool isCooldownActive;
    private NavMeshAgent Agent;
    public float RadioPatrullaje;
    public LayerMask layerwall;
    private bool canAttack = true;

    HealthIA health;
    private void Awake()
    {
        Life = enemiesData.Life;
        // Speed = enemiesData.speed;
        Damage = enemiesData.damage;
        RotationSpeed = enemiesData.Rotationspeed;
        Agent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthIA>();

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
                //Die();
                break;

            case State.None:
                break;
            default:
                break;
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
            if (Distance < 1.5f)
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
    public void RotatePlayer()
    {
        if (Player != null)
        {
            RotateToPosition(Player.transform.position);
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
    bool InSigh(Transform AimOffSetEnemy)
    {
        
        Vector3 start = AimOffSetEnemy.position;
        Vector3 end = health.AimOffSet.position;

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
            HealthPlayer p = agente.GetComponent<HealthPlayer>();
            if (p != null && InSigh(p.AimOffSet))
            {
                Player = p;

            }

        }
    }
     
    //public void Die()
    //{
    //    if (WaveManager.Instance != null)
    //    {
    //        Debug.Log("Enemy destroyed, notifying WaveManager.");
    //        WaveManager.Instance.EnemyDestroyed();
    //    }
    //    else
    //    {
    //        Debug.LogError("WaveManager.Instance is null. Make sure WaveManager is in the scene.");
    //    }
    //    Destroy(this.gameObject);
      
    //}
     
    void Atacar()
    {
        if (Player != null && canAttack) // Solo atacar si se puede atacar
        {
            RotatePlayer();
            Debug.Log("Atacar");
            float Distance = (Player.transform.position - transform.position).magnitude;

            if (Distance <= 1.5f) // Asegurarse de que el jugador esté al alcance
            {
                if (!Player.IsDead) // Si el jugador no está muerto
                {
                    Player.Damage(Damage); // Infligir el daño
                    StartCoroutine(AttackCooldown()); // Iniciar el cooldown
                }
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
        yield return new WaitForSeconds(1f); // Esperar 2 segundos
        canAttack = true; // Reactivar la capacidad de atacar
        state = State.MoverEnemigo; // Volver a mover o atacar después del cooldown
    }
    void Patrullar()
    {

        if (Player != null)
        {
            state = State.MoverEnemigo;
            return;
        }

        float Distance = (RandomPosition - transform.position).magnitude;
        if (Distance <= 2f)
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
        // Intenta encontrar una posición válida en el NavMesh
        Vector3 randomPosition = Vector3.zero;
        NavMeshHit navMeshHit;

        // Número máximo de intentos para encontrar una posición válida
        int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            // Genera una posición aleatoria dentro del radio especificado
            randomPosition = transform.position + Random.insideUnitSphere * RadioPatrullaje;
            randomPosition.y = transform.position.y; // Asegúrate de que la posición esté en el plano horizontal (opcional, si tu terreno es plano)

            // Verifica si la posición aleatoria está dentro del NavMesh
            if (NavMesh.SamplePosition(randomPosition, out navMeshHit, RadioPatrullaje, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }
        }

        // Si no se encuentra una posición válida, devuelve el centro
        Debug.LogWarning("No valid position found within the NavMesh after " + maxAttempts + " attempts. Returning the center position.");
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
    //public void TakeDamage(float damage)
    //{
    //    Life -= damage;

    //    if (Life <= 0)
    //    {
    //        Die();
    //    }
    //}
 

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {

    //        TakeDamage(2);
    //        Destroy(collision.gameObject);
    //    }
    //}
}