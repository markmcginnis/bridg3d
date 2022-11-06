using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //mouseSens should be able to be changed in settings menu
    public float mouseSens = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    
    void Start()
    {
        Debug.Log("mouselook start");
        //lock cursor to prevent clicking out
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        //get mouse input and multiply by sens and adjust for framerate
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //properly align rotation
        xRotation -= mouseY;
        //prevent looking "over shoulder" or "through legs"
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate camera up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotate player side to side
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
