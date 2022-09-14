using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }
}
