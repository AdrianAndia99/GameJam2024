using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnerEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Lista de prefabs de enemigos
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int enemiesPerWave = 10;
    public float waveInterval = 10f;
    public TextMeshProUGUI waveText; // Texto para mostrar la oleada
    public TextMeshProUGUI waveCompletedText; // Texto para mostrar cuando se completa una oleada

    private int currentWave = 1;
    private int enemiesToSpawn;
    private int enemiesRemaining;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            waveText.text = "Wave: " + currentWave;
            enemiesToSpawn = enemiesPerWave;
            enemiesRemaining = enemiesPerWave;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnEnemy();
            }

            yield return new WaitUntil(() => enemiesRemaining <= 0);

            waveCompletedText.text = "Wave " + currentWave + " Completed!";
            yield return new WaitForSeconds(waveInterval);
            waveCompletedText.text = "";

            currentWave++;
            enemiesPerWave += 10;
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation);
    }

    public void OnEnemyKilled()
    {
        enemiesRemaining--;
    }
}

