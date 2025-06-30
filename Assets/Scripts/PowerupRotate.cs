//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;

public class PowerupRotate : MonoBehaviour 
{
    public float rotationSpeed = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);   
    }
}