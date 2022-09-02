using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float damage = 10f;

    public Transform attackPoint;
    public float attackRadius = 3f;
    public LayerMask targetLayer;

    public Animator wepAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //for enemy reuse, maybe make getting input a virtual function and have an
        //EnemyAttackController that inherits from this that gets its input from the
        //main EnemyController and the PlayerAttackController just gets its input
        //the same way but in the virtual function to make porting over easier
        //why write the almost same code twice?
        if(Input.GetButtonDown("Fire1")){
            DefendController defController = GetComponent<DefendController>();
            if(defController != null && defController.shieldAnimator.GetBool("Defend"))
                return;
            //might add delay to actually taking damage so axe swing lines up with potential deaths
            Debug.Log("attack initiated");
            //currently this works by just playing the animation then it can't play again,
            //it would be better to link this to attack cooldown timer tho
            wepAnimator.Play("WepPlaceholder_Attack",0);
            // if(!animator.GetNextAnimatorStateInfo(0).IsName("WepPlaceholder_Attack") || !animator.GetCurrentAnimatorStateInfo(0).IsName("WepPlaceholder_Attack")){
            //     animator.SetTrigger("Attack");
            // }
            Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius, targetLayer, QueryTriggerInteraction.Ignore);
            Debug.Log(colliders.Length);
            foreach(Collider coll in colliders){
                coll.GetComponent<HealthController>().TakeDamage(damage);
            }
        }
    }
}
