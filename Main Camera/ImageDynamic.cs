using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDynamic : MonoBehaviour
{
    public Transform cameraTransform;
    public float dynamicSpeed = 0.1f;

    private Vector3 previousCameraPos;

    private void Start()
    {
        previousCameraPos = cameraTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - previousCameraPos;
        transform.position += new Vector3(deltaMovement.x * dynamicSpeed, 0, 0);
        previousCameraPos += cameraTransform.position;
    }
}
