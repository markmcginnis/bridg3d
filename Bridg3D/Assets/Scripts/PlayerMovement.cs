using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement variables
    [SerializeField]
    private float horizontalSpeed = 1f;
    [SerializeField]
    private float verticalSpeed = 1f;
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private const float gravity = 9.8f;
    [SerializeField]
    private float jumpHeight = 2f;
    private float velocity = 0f;
    private Rigidbody rb;

    //camera variables
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam;

    

    void Start(){
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
 
    void Update()
    {
        //lock mouse cursor on click
        if(Input.GetButtonDown("Fire1")){
            Cursor.lockState = CursorLockMode.Locked;
        }

        //unlock mouse cursor on escape
        if(Input.GetButtonDown("Cancel")){
            Cursor.lockState = CursorLockMode.None;
        }



        //camera axes
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        //calculate angles
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
 
        //rotate camera vertically
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
        //rotate body horizontally
        // transform.eulerAngles = cam.transform.eulerAngles;
        transform.eulerAngles = new Vector3(0f, yRotation, 0f);


        // //player axes
        // float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        // float vertical = Input.GetAxis("Vertical") * movementSpeed;
        // //characterController.Move((Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime);
        // characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
 
        // //gravity
        // if(characterController.isGrounded){
        //     velocity = 0;
        //     if(Input.GetButtonDown("Jump")){
        //         Debug.Log("grounded and trying to jump");
        //         velocity = Mathf.Sqrt(jumpHeight * -2f * GRAVITY);
        //     }
        // }
        // { //standard movement
        //     velocity -= GRAVITY * Time.deltaTime;
        //     characterController.Move(new Vector3(0, velocity, 0));
        // }
    }
}
