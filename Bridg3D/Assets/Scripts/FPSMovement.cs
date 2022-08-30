using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDist = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        //get key axes inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //move according to local rotation versus global position
        Vector3 move = transform.right * x + transform.forward * z;

        //use character controller to move according to speed and adjust for framerate
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
