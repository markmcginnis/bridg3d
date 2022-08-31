using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float damage = 10f;

    public Transform attackPoint;
    public float attackRadius = 3f;
    public LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Debug.Log("attack initiated");
            Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius, targetLayer, QueryTriggerInteraction.Ignore);
            Debug.Log(colliders.Length);
            foreach(Collider coll in colliders){
                coll.GetComponent<HealthController>().TakeDamage(damage);
            }
        }
    }
}
