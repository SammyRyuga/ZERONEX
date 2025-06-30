// Made by Samanyu Pattanayak (SammyRyuga)
// Do not copy without permission

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0; // Private backing field
    private int multiplier = 1; // Private backing field
    private const int MaxMultiplier = 10; // Cap 
    private TMP_Text scoreText; // Private 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneName == "MainMenu" || sceneName.StartsWith("GameScene"))
        {
            ResetScore();
            AssignScoreText();
        }
        else if (sceneName == "EndScene")
        {
            AssignScoreText(); // Show final score without resetting
            SaveHighScore(); // Persist score for high score tracking
        }
    }

    private void AssignScoreText()
    {
        // Find score text in the scene
        scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<TMP_Text>();
        if (scoreText == null)
        {
            Debug.LogWarning("ScoreManager: No GameObject with 'ScoreText' tag found in the scene.");
        }
        UpdateUI();
    }

    public void AddDodgePoint()
    {
        score += multiplier;
        if (multiplier < MaxMultiplier)
        {
            multiplier++;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score} (x{multiplier})"; 
        }
    }

    public void ResetScore()
    {
        score = 0;
        multiplier = 1;
        UpdateUI();
    }

    public void AddMultiplier(int value)
    {
        multiplier = Mathf.Clamp(multiplier + value, 1, MaxMultiplier); // Clamp multiplier
        UpdateUI();
    }

    private void SaveHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log($"New high score saved: {score}");
        }
    }
    
    public int Score => score;
    public int Multiplier => multiplier;
}