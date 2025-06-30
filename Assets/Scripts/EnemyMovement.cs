//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 20f;
    public float destroyZ = -20f;
    private bool hasInteracted = false;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        
        if (!hasInteracted && transform.position.z > GameObject.FindWithTag("Player").transform.position.z)
        {
            hasInteracted = true;
            ScoreManager.Instance.AddDodgePoint();
        }

        if (transform.position.z > destroyZ)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasInteracted = true;
            PlayerMovement player = other.GetComponent<PlayerMovement>();
        }
    }
}