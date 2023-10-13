using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float _spawnRange = 9;

    public GameObject[] enemyPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject[] powerupPrefabs;


    void Start()
    {
        int randomPowerup = UnityEngine.Random.Range(0,powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(),
            powerupPrefabs[randomPowerup].transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            int randomPowerup = UnityEngine.Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup],
                GenerateSpawnPosition(), 
                powerupPrefabs[randomPowerup].transform.rotation);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++) {
            int randomEnemy = UnityEngine.Random.Range(0,enemyPrefabs.Length);

            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(),
            enemyPrefabs[randomEnemy].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = UnityEngine.Random.Range(-_spawnRange, _spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-_spawnRange, _spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
