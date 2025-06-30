//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerupPrefabs;     //fowerup list
    public Transform player;
    public float spawnInterval = 5f;
    public float spawnDistance = 200f;
    public float laneOffset = 35f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPowerup();
            timer = 0f;
        }
    }

    void SpawnPowerup()
    {
        int randomLane = Random.Range(0, 3); 
        float laneX = (randomLane - 1) * laneOffset;
        Vector3 spawnPosition = new Vector3(laneX, 24f, player.position.z + spawnDistance);

        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], spawnPosition, Quaternion.identity);
    }
}