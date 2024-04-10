using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Vector2 inputVector = Vector2.zero;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float gravityMultiplier = 5;
    CharacterController characterController;
    float velocityY = 0;
    bool jumpPressed = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calculate movement
        Vector3 movement = transform.right * inputVector.x
        + transform.forward * inputVector.y;
        movement *= speed;

        if (characterController.isGrounded)
        {
            velocityY = -1f;
            if(jumpPressed)
            {
                velocityY = jumpForce;
            }
        }

        // Calulcate gravity
        velocityY += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        movement.y = velocityY;

        characterController.Move(movement * Time.deltaTime);
        jumpPressed = false;
    }

    void OnMove(InputValue value) => inputVector = value.Get<Vector2>();
        
    void OnJump(InputValue value) => jumpPressed = true;
}
