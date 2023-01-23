using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : CustomComponent
{
    public float damage = 10f;
    public float knockbackForce = 5f;
    public Transform attackPoint;
    public float attackRadius = 3f;
    public LayerMask targetLayer;
    public Animator wepAnimator;
    public float attackCooldown = 1.5f;
    public float attackTime;

    public AudioManager audioManager;

    void Start(){
        //start with being able to attack
        attackTime = 0;
        audioManager = GetComponent<AudioManager>();
    }

    void Update(){
        //over time make sure the cooldown actually cools down
        attackTime = Mathf.Clamp(attackTime - Time.deltaTime, -0.0001f, attackCooldown);
    }

    public void Attack(){
        //check if cooldown is over yet or not
        if(attackTime > 0)
            return;
        //reset cooldown
        attackTime = attackCooldown;
        //see if we have a shield and if we do don't attack if shield is up
        DefendController defController = GetComponent<DefendController>();
        if(defController != null && defController.shieldAnimator.GetBool("Defend"))
            return;
        //this may need to change strings
        wepAnimator.Play("WepPlaceholder_Attack",0);
        audioManager.Play("Attack");
        //may add delay to actual taking of damage to match up with animation
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius, targetLayer, QueryTriggerInteraction.Ignore);
        foreach(Collider coll in colliders){
            HealthController hc = coll.GetComponent<HealthController>();
            if(hc)
                hc.TakeDamage(damage);
            //this could probably have the null check removed but for now its here
            //apply knockback correctly
            KnockbackController knockbackController = coll.GetComponent<KnockbackController>();
            if(knockbackController)
                knockbackController.AddKnockback(transform.forward, knockbackForce);
        }
    }
}
