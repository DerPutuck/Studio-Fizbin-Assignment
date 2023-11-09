using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public Transform player;
    
    [Header("CameraFollow")]
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        MoveCamera();
    }

    // Follows the Player with a given delay (smoothTime) at the distance and angle stated in the "Main Camera" Object 
    void MoveCamera()
    {
        if (player != null)
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, smoothTime);
    }
}
