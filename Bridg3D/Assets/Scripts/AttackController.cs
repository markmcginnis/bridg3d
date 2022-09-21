using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float damage = 10f;

    public float knockbackForce = 5f;
    public Transform attackPoint;
    public float attackRadius = 3f;
    public LayerMask targetLayer;

    public Animator wepAnimator;

    public float attackCooldown = 1.5f;

    float attackTime;

    void Start(){
        attackTime = 0;
    }

    void Update(){
        attackTime = Mathf.Clamp(attackTime - Time.deltaTime, -0.1f, attackCooldown);
    }

    public void Attack(){
        if(attackTime > 0)
            return;
        attackTime = attackCooldown;
        DefendController defController = GetComponent<DefendController>();
        if(defController != null && defController.shieldAnimator.GetBool("Defend"))
            return;
        //might add delay to actually taking damage so axe swing lines up with potential deaths
        //Debug.Log("attack initiated");
        //currently this works by just playing the animation then it can't play again,
        //it would be better to link this to attack cooldown timer tho
        wepAnimator.Play("WepPlaceholder_Attack",0);
        // if(!animator.GetNextAnimatorStateInfo(0).IsName("WepPlaceholder_Attack") || !animator.GetCurrentAnimatorStateInfo(0).IsName("WepPlaceholder_Attack")){
        //     animator.SetTrigger("Attack");
        // }
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius, targetLayer, QueryTriggerInteraction.Ignore);
        //Debug.Log(colliders.Length);
        foreach(Collider coll in colliders){
            //Debug.Log(coll.gameObject.name);
            coll.GetComponent<HealthController>().TakeDamage(damage);
            coll.GetComponent<KnockbackController>().AddKnockback(transform.forward, knockbackForce);
        }
    }
}
