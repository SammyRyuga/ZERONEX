//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class ReversePickup : MonoBehaviour
{
    public float reverseDuration = 5f; // 5 sec rev

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.StartReverseControls(reverseDuration);
                Destroy(gameObject); 
            }
        }
    }
}