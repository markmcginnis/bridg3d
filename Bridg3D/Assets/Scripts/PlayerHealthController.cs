using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    public override void Die()
    {
        //disable components to see outcome
        base.Die();
        GetComponent<AttackController>().enabled = false;
        GetComponent<HealthController>().enabled = false;
        GetComponent<DefendController>().enabled = false;
        GetComponent<FPSMovement>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;
        //may need to add more like quick timer than go to lose menu or whatever else
    }
}
