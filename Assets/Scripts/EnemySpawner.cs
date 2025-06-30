//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float[] lanes;   // for random lanes
    public GameObject[] enemies;    // for random enemies
    public float initialSpawnInterval = 3f;
    public float spawnZ = 80f;

    private float spawnInterval;
    private float timer = 0f;
    private float timeElapsed = 0f;

    public float difficultyRampRate = 0.1f;
    public int maxEnemiesPerWave = 3;
    public float minSpawnInterval = 0.7f;

    private float lastThreeEnemyTime = 0f;
    public float threeEnemyCooldown = 6f;

    void Start()
    {
        spawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // random intervals
        float baseInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - (timeElapsed * difficultyRampRate));
        spawnInterval = baseInterval + Random.Range(0f, 1.2f);

        // max enims
        maxEnemiesPerWave = Mathf.Min(lanes.Length, 1 + Mathf.FloorToInt(timeElapsed / 10f));

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        List<int> laneIndices = new List<int>();
        for (int i = 0; i < lanes.Length; i++) laneIndices.Add(i);
        Shuffle(laneIndices);

        int enemiesToSpawn = Random.Range(1, maxEnemiesPerWave + 1);

        // Avoid brutal back-to-back 3-enemy waves
        if (enemiesToSpawn == 3)
        {
            if (Time.time - lastThreeEnemyTime < threeEnemyCooldown)
            {
                enemiesToSpawn = Random.Range(1, 3); // reduce to 1 or 2
            }
            else
            {
                lastThreeEnemyTime = Time.time;
            }
        }

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int laneIndex = laneIndices[i];
            GameObject enemyToSpawn = enemies[Random.Range(0, enemies.Length)];
            Vector3 spawnPosition = new Vector3(lanes[laneIndex], 10f, spawnZ);
            GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.LookRotation(Vector3.back));

            // Speed boost w time
            float speedBoost = 10f + timeElapsed * 0.5f;
            if (enemy.TryGetComponent<EnemyMovement>(out var enemyMovement))
            {
                enemyMovement.speed = speedBoost;
            }
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}