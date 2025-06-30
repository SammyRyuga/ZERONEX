//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSceneManager : MonoBehaviour
{
    public TMP_Text finalScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + finalScore;
    }

    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.DeleteKey("FinalScore"); 
            SceneManager.LoadScene("MainMenu"); 
        }
    }
}