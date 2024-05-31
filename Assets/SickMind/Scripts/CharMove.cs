using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public LayerMask groundLayer; // LayerMask to identify the ground

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        RotateToMouse();
    }

    void Move()
    {
        // Get input from keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }

    void RotateToMouse()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        // Check if the ray hits the ground
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // Get the hit point
            Vector3 targetPosition = hit.point;

            // Keep the y-coordinate the same as the player's to prevent vertical rotation
            targetPosition.y = transform.position.y;

            // Calculate the direction to look towards the target position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Calculate the rotation angle
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Apply the rotation
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed));
        }
    }
}

