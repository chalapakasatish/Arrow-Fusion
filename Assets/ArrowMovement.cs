using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Adjust the speed as needed
    private Touch touch;
    private float speedMofifier;
    public Animator playerAnimator;
    private void Start()
    {
        speedMofifier = 0.01f;
    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5f, 5f), transform.position.y, transform.position.z);


        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if(touch.deltaPosition.x > 0)
                {
                    Debug.Log("Forward");
                    playerAnimator.SetBool("isForward", true);
                    playerAnimator.SetBool("isBackward", false);
                    playerAnimator.SetBool("isIdle", false);
                }
                if (touch.deltaPosition.x < 0)
                {
                    Debug.Log("Backward");
                    playerAnimator.SetBool("isForward", false);
                    playerAnimator.SetBool("isBackward", true);
                    playerAnimator.SetBool("isIdle", false);
                }
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedMofifier, transform.position.y, transform.position.z);
            }
            //if (touch.phase == TouchPhase.Stationary)
            //{
            //    Debug.Log("Idle");
            //    playerAnimator.SetBool("isForward", false);
            //    playerAnimator.SetBool("isBackward", false);
            //    playerAnimator.SetBool("isIdle", true);
            //}
        }
        else
        {
            Debug.Log("Idle");
            playerAnimator.SetBool("isForward", false);
            playerAnimator.SetBool("isBackward", false);
            playerAnimator.SetBool("isIdle", true);
        }
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
