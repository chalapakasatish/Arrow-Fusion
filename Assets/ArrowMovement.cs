using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Adjust the speed as needed

    void Update()
    {
        // Move the player left or right based on input
        MovePlayer();
    }

    void MovePlayer()
    {
        // Get the horizontal input axis (left/right arrow keys or A/D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

        // Move the player
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }
}
