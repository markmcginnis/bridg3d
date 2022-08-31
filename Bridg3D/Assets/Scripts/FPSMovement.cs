using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public float sprintModifier = 1.5f;
    public float sprintCapacity = 5f;

    float sprintTime;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDist = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    void Start(){
        sprintTime = sprintCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        //use object at feet to see if i am on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        //ensure feet don't just leave the floor
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        //get key axes inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //move according to local rotation versus global position
        Vector3 move = transform.right * x + transform.forward * z;

        //current speed is same as it should be
        float speedModifier = 1f;

        //if trying to sprint
        if(Input.GetAxis("Sprint") > 0){
            //make sure time is not negative and adjust as needed
            sprintTime = Mathf.Clamp(sprintTime - Time.deltaTime, 0, sprintCapacity);
            //if we have sprint time left
            if(sprintTime > 0){
                //we go faster
                speedModifier = sprintModifier;
            }
        }
        //not trying to sprint
        else{
            //regenerate sprint time but don't go over capacity
            sprintTime = Mathf.Clamp(sprintTime + Time.deltaTime, 0, sprintCapacity);
        }

        //use character controller to move according to speed and adjust for framerate
        controller.Move(move * speed * Time.deltaTime * speedModifier);

        //if jump button hit and currently on ground
        if(Input.GetButtonDown("Jump") && isGrounded){
            //using physics to jump up specific height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //apply gravity over given time
        velocity.y += gravity * Time.deltaTime;

        //move according to gravity and adjust for framerate
        controller.Move(velocity * Time.deltaTime);
    }
}
