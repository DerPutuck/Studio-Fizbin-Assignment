using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    private CharacterController charController;
    
    [Header("Movement")] 
    private Vector2 moveDirection;
    [SerializeField] private float speed;
    
    [Header("Gravity")]
    private float verticalVelocity;
    private float gravity = 9.81f;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    // Checks if the Player wants to move through the Player Input Component on the Player and applies the direction
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    // Applies Gravity in order to fall down
    private void ApplyGravity()
    {
        if (charController.isGrounded && verticalVelocity < 0)
            verticalVelocity = 0f;
        verticalVelocity -= gravity * Time.deltaTime;
    }
    
    // Moves the Player in the given direction through the Character Controller on the Player and rotates the Player accordingly
    void MovePlayer()
    {
        charController.Move(new Vector3(moveDirection.x, verticalVelocity, moveDirection.y) * speed * Time.deltaTime);
        
        if (moveDirection != Vector2.zero)
        {
            var rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 720 * Time.deltaTime);
        }
    }
    
    
}
