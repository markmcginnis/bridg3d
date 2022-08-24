using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement variables
    [SerializeField]
    private float mouseSensitivity = 1f;
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private float sprintModifier = 1.5f;
    [SerializeField]
    private float sprintCapacity = 5f;
    [SerializeField]
    private const float gravity = 9.8f;
    [SerializeField]
    private float jumpHeight = 2f;

    //variables for use within movement calculations
    float moveForward;
    float moveSide;
    float moveUp;
    bool isGrounded;
    float velocity;
    private Rigidbody rb;
    float sprintTime;

    //camera variables
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam;

    

    void Start(){
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        sprintTime = sprintCapacity;
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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //calculate angles
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
 
        //rotate camera vertically
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
        //rotate body horizontally
        transform.eulerAngles = new Vector3(0f, yRotation, 0f);



        //player movement
        if(Input.GetAxis("Sprint") > 0){
            sprintTime = Mathf.Clamp(sprintTime - Time.deltaTime, 0, sprintCapacity);
            if(sprintTime > 0){
                velocity = movementSpeed * sprintModifier;
            }
            else{
                velocity = movementSpeed;
            }
        }
        else{
            sprintTime = Mathf.Clamp(sprintTime + Time.deltaTime, 0, sprintCapacity);
            velocity = movementSpeed;
        }

        moveForward = Input.GetAxis("Vertical") * velocity;
        moveSide = Input.GetAxis("Horizontal") * velocity;
        moveUp = Input.GetAxis("Jump") * jumpHeight;
    }

    private void FixedUpdate() {
        rb.velocity = (transform.forward * moveForward) + (transform.right * moveSide) + (transform.up * rb.velocity.y);
        if(isGrounded && moveUp != 0){
            rb.AddForce(transform.up * moveUp, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground"){
            isGrounded = true;
        }
    }
}
