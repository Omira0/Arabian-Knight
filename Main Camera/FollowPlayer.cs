using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset;   // Offset from the player's position
    [SerializeField] private Transform target; // The player or object to follow
    [SerializeField] private float smoothTime = 0.3f; // Smooth time for the camera movement
    private Vector3 currentVelocity = Vector3.zero;

    [SerializeField] private float yOffset = -2f; // Adjust this value to keep the camera lower

    private void Awake()
    {
        // Set the initial offset
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Adjust the Y-axis offset to lower the camera
        Vector3 targetPosition = target.position + offset + new Vector3(0, yOffset, 0);

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
