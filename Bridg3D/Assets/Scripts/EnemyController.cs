using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    string targetTag;
    Transform target;

    Quaternion initialRotation;

    CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;
    public float diffToJump = 2f;
    public float followDistance = 1f;
    public Transform groundCheck;
    public float groundDist = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    public int coinValue = 1;

    float attackRange;


    AttackController attackController;
    DefendController defendController;
    HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
        attackController = GetComponent<AttackController>();
        defendController = GetComponent<DefendController>();
        healthController = GetComponent<HealthController>();
        controller = GetComponent<CharacterController>();
        initialRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
        attackRange = attackController.attackRadius;
    }

    // Update is called once per frame 
    void Update()
    {
        Look();
        Vector3 distance = target.position - transform.position;
        if(distance.magnitude > followDistance)
            Move();
        //logic for attacking
        if((target.position - transform.position).magnitude < attackRange){
            attackController.Attack();
        }
        //logic for defending
        if(defendController != null){
            //defendController.ShieldUp();
            //defendController.ShieldDown();
        }
    }

    void Look(){
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y, 0);
        transform.rotation = lookRotation;
    }

    void Move(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        //ensure feet don't just leave the floor
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        //if jump button hit and currently on ground
        if(target.position.y - transform.position.y >= diffToJump && isGrounded){
            //using physics to jump up specific height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //apply gravity over given time
        velocity.y += gravity * Time.deltaTime;

        //decide x and z velocity
        Vector3 directionalMove = (target.position - transform.position).normalized * speed;

        //combine direction and gravity
        Vector3 move = new Vector3(directionalMove.x, velocity.y, directionalMove.z);

        //move according to gravity and adjust for framerate
        controller.Move(move * Time.deltaTime);
    }
}
