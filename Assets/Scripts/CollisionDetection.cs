// Made by Samanyu Pattanayak (SammyRyuga)
// Do not copy without permission

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    private bool isColliding = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure Player GameObject is tagged 'Player'");
            return;
        }

        playerRb = player.GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            Debug.LogWarning("Player Rigidbody missing! Player may still move through enemies.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isColliding || !other.CompareTag("Player")) return;

        isColliding = true;

        // Disable movement and physics
        var pm = player.GetComponent<PlayerMovement>();
        if (pm != null) pm.enabled = false;
        if (playerRb != null) playerRb.isKinematic = true;

        // Save 
        if (ScoreManager.Instance != null)
        {
            PlayerPrefs.SetInt("FinalScore", ScoreManager.Instance.Score);
            PlayerPrefs.Save(); 
        }
        else
        {
            Debug.LogWarning("ScoreManager instance not found! Final score will not be saved.");
        }

        StartCoroutine(CollisionEnd());
    }

    IEnumerator CollisionEnd()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ExitScene"); 
    }
}