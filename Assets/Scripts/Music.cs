//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}