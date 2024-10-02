using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int initialWaveSize = 1;
    public float waveDelay = 5f;
    public float spawnDelay = 1f;
    private int currentWave = 1;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveDelay);

            for (int i = 0; i < initialWaveSize * currentWave; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                yield return new WaitForSeconds(spawnDelay);
            }

            currentWave++;
        }
    }
}