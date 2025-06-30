using UnityEngine;

public class ShipLoader : MonoBehaviour
{
    public GameObject[] shipPrefabs; // Assign all ship prefabs in inspector
    public Transform spawnPoint; // Where the ship should appear in the scene

    void Start()
    {
        int selectedShipIndex = PlayerPrefs.GetInt("SelectedShip", 0);
        GameObject shipToSpawn = shipPrefabs[selectedShipIndex];
        Instantiate(shipToSpawn, spawnPoint.position, Quaternion.identity);
    }
}