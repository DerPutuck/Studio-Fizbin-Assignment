using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")] 
    private Vector2 moveDirection;
    [SerializeField] private float speed;
    
    private void Update()
    {
        MovePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    
    void MovePlayer()
    {
        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * speed * Time.deltaTime;
        if (moveDirection != Vector2.zero)
        {
            var rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 720 * Time.deltaTime);
        }
    }
    
    
}
