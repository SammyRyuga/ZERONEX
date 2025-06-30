//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    public List<GameObject> shipPrefabs;
    public float rotationSpeed = 50.0f;
    public Transform shipSpawn;
    public GameObject mapSelectionPanel;

    private int currentIndex = 0;
    private GameObject currentShip;

    void Start()
    {
        SpawnShip(currentIndex);
        mapSelectionPanel.SetActive(false); // Hide map 
    }

    void Update()
    {
        if (currentShip != null)
            currentShip.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void NextShip()
    {
        currentIndex = (currentIndex + 1) % shipPrefabs.Count;
        SpawnShip(currentIndex);
    }

    public void PreviousShip()
    {
        currentIndex = (currentIndex - 1 + shipPrefabs.Count) % shipPrefabs.Count;
        SpawnShip(currentIndex);
    }

    void SpawnShip(int index)
    {
        if (currentShip != null)
            Destroy(currentShip);

        currentShip = Instantiate(shipPrefabs[index], shipSpawn.position, Quaternion.identity);
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("SelectedShip", currentIndex);
        PlayerPrefs.Save();
        Debug.Log("Ship selected: " + currentIndex);
        mapSelectionPanel.SetActive(true); // Show map
    }

    // Called by map buttons
    public void LoadMap1()
    {
        SceneManager.LoadScene("GameScene1"); 
    }

    public void LoadMap2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void LoadMap3()
    {
        SceneManager.LoadScene("GameScene3");
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }
}