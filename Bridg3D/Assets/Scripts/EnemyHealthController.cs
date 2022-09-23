using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    Coin coin;
    [SerializeField]
    Transform coinSpawnpoint;
    public override void Die()
    {
        base.Die();
        coin.value = GetComponent<EnemyController>().coinValue;
        Instantiate(coin.gameObject, coinSpawnpoint.position, coinSpawnpoint.rotation);
        GameObject.Destroy(gameObject);
    }
}
