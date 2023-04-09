using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour
{
    public enum EnemyState {CHARGE, HESITATE, RETREAT, BLOCK} //maybe ORGANIZE too but that needs lanes

    [SerializeField]
    string targetTag;
    [SerializeField]
    string secondaryTargetTag;
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
    // public float moveAngleDeviation = 30f;
    bool isGrounded;

    Vector3 velocity;

    public float coinValue = 1;

    public float attackRange;

    public float strategyLevel = 5f;
    float currHealth;
    public float shieldTime;

    public EnemyState enemyState;

    public Transform retreatPoint;
    public float hesitationTime;
    public float hesitationTimer;
    public float retreatTime;

    AttackController attackController;
    DefendController defendController;
    HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
        attackController = GetComponent<AttackController>();
        defendController = GetComponent<DefendController>();
        healthController = GetComponent<HealthController>();
        controller = GetComponent<CharacterController>();
        initialRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
        // attackRange = attackController.attackRadius;
        attackRange = 4.5f;
        currHealth = healthController.currentHealth;
        enemyState = EnemyState.CHARGE;
        hesitationTimer = Random.Range(0.5f,strategyLevel);
        retreatPoint = GameObject.Find("RetreatPoint").GetComponent<Transform>();
    }

    void SetTarget(){
        GameObject searchTarget = GameObject.FindGameObjectWithTag(targetTag);
        if(!searchTarget){
            searchTarget = GameObject.FindGameObjectWithTag(secondaryTargetTag);
        }
        target = searchTarget.GetComponent<Transform>();
    }

    // Update is called once per frame 
    void Update()
    {
        if(!GameObject.FindGameObjectWithTag(targetTag))
            target = GameObject.FindGameObjectWithTag(secondaryTargetTag).GetComponent<Transform>();

        switch(enemyState){
            case EnemyState.CHARGE:
                Look();
                if((target.position - transform.position).magnitude > followDistance)
                    MoveTowardsTarget(); //move towards target up to certain distance
                hesitationTimer = Mathf.Clamp(hesitationTimer - Time.deltaTime, -1, strategyLevel);
                if(hesitationTimer < 0){
                    enemyState = EnemyState.HESITATE;
                    hesitationTime = 1f;
                }
                break;
            case EnemyState.HESITATE:
                //basically dont move forward but dont move backwards, just wait
                Look();
                MoveWithGravity();
                hesitationTime -= Time.deltaTime;
                if(hesitationTime < 0f){
                    enemyState = EnemyState.CHARGE;
                    hesitationTimer = Random.Range(0.5f,strategyLevel);
                }
                break;
            case EnemyState.RETREAT:
                retreatTime -= Time.deltaTime;
                target = retreatPoint; //change target to retreat point temporarily
                Look();
                if((target.position - transform.position).magnitude > followDistance && retreatTime > 0f){ //dont jiggle
                    MoveTowardsTarget(); //move in normal fashion
                }
                else{
                    enemyState = EnemyState.CHARGE;
                }
                SetTarget();
                break;
            case EnemyState.BLOCK:
                shieldTime = Mathf.Clamp(shieldTime - Time.deltaTime, -1, strategyLevel);
                Look();
                MoveWithGravity();
                if(defendController.shieldTime > 0){
                    enemyState = EnemyState.RETREAT;
                    retreatTime = defendController.shieldTime;
                    return;
                }
                if(!defendController.IsShieldUp() && shieldTime < 0){
                    defendController.ShieldUp(); //try to put shield up
                    shieldTime = Random.Range(strategyLevel/3f, strategyLevel); //for certain time
                }

                if(shieldTime < 0){ //if time up, put shield down
                    Debug.Log("PLEASE LET ME PUT THE SHIELD DOWN");
                    defendController.ShieldDown();
                    enemyState = EnemyState.CHARGE;
                } //dont move towards target when shielding
                break;
        }

        //logic for attacking
        if((target.position - transform.position).magnitude < attackRange || (GameObject.FindGameObjectWithTag(secondaryTargetTag).GetComponent<Transform>().position - transform.position).magnitude < attackRange){
            attackController.Attack();
        }
        //logic for defending
        if(currHealth > healthController.currentHealth){
            if(defendController.enabled){
                enemyState = EnemyState.BLOCK;
            }
            else{
                enemyState = EnemyState.RETREAT;
                retreatTime = 1f;
            }
        }
        currHealth = healthController.currentHealth;
    }

    void Look(){
        //face player but don't look up
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y, 0);
        transform.rotation = lookRotation;
    }

    void MoveWithGravity(){
        velocity.y += gravity * Time.deltaTime;
        Vector3 move = new Vector3(0, velocity.y, 0);
        controller.Move(move * Time.deltaTime);
    }

    void MoveTowardsTarget(){
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
        // float angleDeviation = Random.Range(-moveAngleDeviation,moveAngleDeviation);
        // int angleModifier = Random.Range(-1,2);
        Vector3 directionalMove = (target.position - transform.position).normalized * speed;
        // directionalMove = Quaternion.AngleAxis(angleDeviation + moveAngleDeviation * angleModifier,Vector3.up) * directionalMove;

        //combine direction and gravity
        Vector3 move = new Vector3(directionalMove.x, velocity.y, directionalMove.z);
        // move = Quaternion.RotateTowards(transform.rotation,Quaternion.AngleAxis(angleDeviation,transform.forward),moveAngleDeviation).eulerAngles;

        //move according to gravity and adjust for framerate
        controller.Move(move * Time.deltaTime);
    }
}
