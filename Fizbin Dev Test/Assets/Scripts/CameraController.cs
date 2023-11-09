using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (player != null)
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, smoothTime);
    }
}
