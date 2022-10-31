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
        //do the normal die stuff, then spawn a coin with a given value
        base.Die();
        coin.value = GetComponent<EnemyController>().coinValue;
        Instantiate(coin.gameObject, coinSpawnpoint.position, coinSpawnpoint.rotation);
        this.GetComponent<EnemyController>().enabled = false;
        Destroy(GetComponent<KnockbackController>());
        // this.GetComponent<CharacterController>().enabled = false;
        Destroy(GetComponent<CharacterController>());
        this.transform.Find("BodyPlaceholder").gameObject.AddComponent<Rigidbody>();
        this.transform.Find("BodyPlaceholder").gameObject.AddComponent<CapsuleCollider>();
        this.transform.Find("WeaponSlot").gameObject.SetActive(false);
        this.transform.Find("ShieldSlot+0.5").gameObject.SetActive(false);
        this.gameObject.tag = "DeadEnemy";
        Vector3 backward = this.transform.forward;
        this.transform.Find("BodyPlaceholder").GetComponent<Rigidbody>().AddForce(this.transform.forward*-50, ForceMode.Force);
        //GameObject.Destroy(gameObject);
    }
}
