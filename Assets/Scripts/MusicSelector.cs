//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;
using UnityEngine.UI;

public class MusicSelector : MonoBehaviour
{
    public Dropdown musicDropdown;

    void Start()
    {
        musicDropdown.onValueChanged.AddListener(delegate {
            ChangeMusic(musicDropdown.value);
        });

        // Load saved selection
        int savedIndex = PlayerPrefs.GetInt("SelectedTrack", 0);
        musicDropdown.value = savedIndex;
    }

    void ChangeMusic(int index)
    {
        AudioManager.Instance.PlayTrack(index);
    }
}