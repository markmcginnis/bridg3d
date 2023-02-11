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

    InputManager input;

    void Start(){
        input = GameObject.FindObjectOfType<InputManager>();
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
        float x = input.GetAxis("Horizontal");
        float z = input.GetAxis("Vertical");

        //move according to local rotation versus global position
        Vector3 move = transform.right * x + transform.forward * z;

        //current speed is same as it should be
        float speedModifier = 1f;

        //if trying to sprint
        if(input.GetButtonDown("Sprint")){
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
        if(input.GetButtonDown("Jump") && isGrounded){
            //using physics to jump up specific height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //apply gravity over given time
        velocity.y += gravity * Time.deltaTime;

        //move according to gravity and adjust for framerate
        controller.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = hit.moveDirection * 3;
    }
}
