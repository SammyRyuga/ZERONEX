//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class SuperJumpPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Super Jump " + other.name);
            other.GetComponent<PlayerMovement>().ActivateSuperJump();
            Destroy(gameObject);
        }
    }
}