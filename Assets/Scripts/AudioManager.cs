//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> bgmTracks;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        PlayTrack(currentTrackIndex);
    }

    public void PlayTrack(int index)
    {
        if (index >= 0 && index < bgmTracks.Count)
        {
            currentTrackIndex = index;
            audioSource.clip = bgmTracks[index];
            audioSource.loop = true;
            audioSource.Play();
            PlayerPrefs.SetInt("SelectedTrack", index);
        }
    }

    void Start()
    {
        int savedTrack = PlayerPrefs.GetInt("SelectedTrack", 0);
        PlayTrack(savedTrack);
    }
}