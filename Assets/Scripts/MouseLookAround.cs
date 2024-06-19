using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;
    public float sensitivity = 360f;
    public float minimumX = -90f;
    public float maximumX = 90f;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
        rotationY += Input.GetAxis("Mouse X") * sensitivity ;
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        transform.localEulerAngles = new UnityEngine.Vector3(rotationX, rotationY, 0);
    }
}
