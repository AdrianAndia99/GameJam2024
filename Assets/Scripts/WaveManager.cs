using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class EnemyWave
{
    public GameObject prefabs;
    public int Count;
    public int Index;
}

[System.Serializable]
public class Wave
{
    public List<EnemyWave> ListEnemyWave = new List<EnemyWave>();
    public int Index = 0;

    public void InstantiateEnemy(Transform point)
    {
        if (Index < ListEnemyWave.Count && ListEnemyWave[Index].Index < ListEnemyWave[Index].Count)
        {
            GameObject.Instantiate(ListEnemyWave[Index].prefabs, point.position, Quaternion.identity);
            ListEnemyWave[Index].Index++;
        }
        else
        {
            Index++;
        }
    }
    public bool IsWaveComplete()
    {
        return Index >= ListEnemyWave.Count;
    }
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public List<Transform> ListSpawnPoint = new List<Transform>();
    public List<Wave> ListWave = new List<Wave>();
    public TMP_Text waveText;
    public TMP_Text enemiesRemainingText;

    private int currentWaveIndex = 0;
    private int enemiesRemaining;
    private int spawnPointIndex = 0;
    private bool waveInProgress = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    void Update()
    {
        if (waveInProgress && enemiesRemaining <= 0)
        {
            waveInProgress = false;
            StartCoroutine(StartNextWave());
        }
    }

    private IEnumerator StartNextWave()
    {
        if (currentWaveIndex < ListWave.Count)
        {
            waveText.text = "Oleada " + (currentWaveIndex + 1) + " comienza en 10 segundos...";
            yield return new WaitForSeconds(10f);

            waveText.text = "Wave " + (currentWaveIndex + 1);
            Wave currentWave = ListWave[currentWaveIndex];
            enemiesRemaining = GetTotalEnemiesInWave(currentWave);
            UpdateEnemiesRemainingText();

            while (!currentWave.IsWaveComplete())
            {
                currentWave.InstantiateEnemy(ListSpawnPoint[spawnPointIndex]);
                spawnPointIndex = (spawnPointIndex + 1) % ListSpawnPoint.Count;
                yield return new WaitForSeconds(1f); // Ajusta el tiempo entre apariciones
            }

            waveInProgress = true;
            currentWaveIndex++;
        }
        else
        {
            waveText.text = "Oleada superada!";
        }
    }

    private int GetTotalEnemiesInWave(Wave wave)
    {
        int total = 0;
        foreach (EnemyWave enemyWave in wave.ListEnemyWave)
        {
            total += enemyWave.Count;
        }
        return total;
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
        UpdateEnemiesRemainingText();
    }

    private void UpdateEnemiesRemainingText()
    {
        enemiesRemainingText.text = "Enemigos restantes: " + enemiesRemaining;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var item in ListSpawnPoint)
        {
            Gizmos.DrawSphere(item.position, 1f);
        }
    }
}