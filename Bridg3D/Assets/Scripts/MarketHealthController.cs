using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketHealthController : HealthController
{
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        // FindGetComponent<HealthController>().enabled = false;
        // GetComponent<MarketController>().enabled = false;
    }
}
