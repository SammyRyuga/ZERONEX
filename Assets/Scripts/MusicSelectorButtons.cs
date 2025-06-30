//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSelectorButtons : MonoBehaviour
{
    public void SelectTrack(int index)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayTrack(index);
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}