//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class MultiplierPickup : MonoBehaviour
{
    public int bonusMultiplier = 1; // boost multi

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddMultiplier(bonusMultiplier);
            Destroy(gameObject);
        }
    }
}