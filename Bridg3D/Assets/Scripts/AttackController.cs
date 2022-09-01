using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float damage = 10f;

    public Transform attackPoint;
    public float attackRadius = 3f;
    public LayerMask targetLayer;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            //might add delay to actually taking damage so axe swing lines up with potential deaths
            Debug.Log("attack initiated");
            //currently this works by just playing the animation then it can't play again,
            //it would be better to link this to attack cooldown timer tho
            animator.Play("WepPlaceholder_Attack",0);
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
