using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private CharacterController charController;
    
    [Header("Movement")] 
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    
    [Header("Gravity")]
    private float verticalVelocity;
    [SerializeField] private float gravity;

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
        ApplyAcceleration();
        ApplyGravity();
    }

    #region Moving Player
    
    // Checks if the Player wants to move through the Player Input Component on the Player and applies the direction
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    
    // Moves the Player in the given direction through the Character Controller on the Player and rotates the Player accordingly
    void MovePlayer()
    {
        charController.Move(new Vector3(lastMoveDirection.x, verticalVelocity, lastMoveDirection.y) * currentSpeed * Time.deltaTime);
        
        var lookDirection = Quaternion.LookRotation(new Vector3(lastMoveDirection.x, 0, lastMoveDirection.y), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, 720 * Time.deltaTime);
    }

    #endregion
    
    #region Applying Forces
    
    // Accelerates and Decelerates the Player when starting or stopping movement
    void ApplyAcceleration()
    {
        if (moveDirection != Vector2.zero)
        {
            currentSpeed += acceleration * Time.deltaTime;
            lastMoveDirection = moveDirection;  // Storing the last moveDirection in order to decelerate correctly
        }
        else
            currentSpeed -= deceleration * Time.deltaTime;
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }
    
    // Applies Gravity in order to fall down
    private void ApplyGravity()
    {
        if (charController.isGrounded && verticalVelocity < 0)
            verticalVelocity = 0f;
        verticalVelocity -= gravity * Time.deltaTime;
    }
    
    #endregion
}
