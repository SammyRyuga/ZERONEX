//Made by Samanyu Pattanayak (SammyRyuga)
//Do not copy without permission

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public int currentLane = 2; // 1L 2M 3R
    public float laneWidth = 35f;
    public float laneShiftSpeed = 50f;
    public float jumpForce = 700f;
    public float xDestination;
    public bool isMoving = false;
    public bool isGrounded = true;
    public float xVelocity = 0f;

    // Super Jump
    public float superJumpForce = 1500f;
    private bool superJumpReady = false;
    
    // Reverse Controls
    private bool controlsReversed = false;
    private float reverseTimer = 0f;
    
    // Sounds
    public AudioClip jumpSound;
    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        xDestination = transform.position.x;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        HandleLaneSwitch();
        HandleJumpInput();
        HandleLaneMovement();
        if (controlsReversed)
        {
            reverseTimer -= Time.deltaTime;
            if (reverseTimer <= 0f)
            {
                controlsReversed = false;
                Debug.Log("Controls back to normal, phew!");
            }
        }

    }

    void HandleLaneSwitch()
    {
        if (!isMoving)
        {
            if (!controlsReversed)
            {
                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane < 3)
                {
                    currentLane++;
                    xDestination += laneWidth;
                    isMoving = true;
                }
                else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane > 1)
                {
                    currentLane--;
                    xDestination -= laneWidth;
                    isMoving = true;
                }
            }
            else // controls are reversed
            {
                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 1)
                {
                    currentLane--;
                    xDestination -= laneWidth;
                    isMoving = true;
                }
                else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < 3)
                {
                    currentLane++;
                    xDestination += laneWidth;
                    isMoving = true;
                }
            }
        }
    }


    void HandleJumpInput()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;

            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }


    void HandleLaneMovement()
    {
        if (isMoving)
        {
            float step = laneShiftSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(transform.position.x, xDestination, step);
            transform.position = newPosition;

            if (Mathf.Abs(transform.position.x - xDestination) < 0.1f)
            {
                transform.position = new Vector3(xDestination, transform.position.y, transform.position.z);
                isMoving = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 customGravity = new Vector3(0, -50f, 0);
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (superJumpReady)
            {
                rb.AddForce(Vector3.up * superJumpForce);
                isGrounded = false;
                superJumpReady = false;
            }
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit");
        }
    }

    public void ActivateSuperJump()
    {
        Debug.Log("Super Jump Activated");

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * superJumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
    
    public void StartReverseControls(float duration)
    {
        controlsReversed = true;
        reverseTimer = duration;
        Debug.Log("Controls reversed");
    }
}