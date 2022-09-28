using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class KnockbackController : CustomComponent
{
    [SerializeField]
    float mass = 3f;
    [SerializeField]
    float knockbackThreshold = 0.2f;
    [SerializeField]
    float dispersionRate = 5f;
    Vector3 impact = Vector3.zero;
    CharacterController characterController;

    void Start(){
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if there is still impact to go then move that direction
        if(impact.magnitude > knockbackThreshold)
            characterController.Move(impact * Time.deltaTime);
        //gradually lower the impact
        impact = Vector3.Lerp(impact, Vector3.zero, dispersionRate*Time.deltaTime);
    }

    public void AddKnockback(Vector3 direction, float force){
        direction.Normalize();
        //don't push into ground weirdly
        if(direction.y < 0)
            direction.y = -direction.y;
        //move in certain direction with certain force
        impact += direction.normalized * force / mass;
    }
}
