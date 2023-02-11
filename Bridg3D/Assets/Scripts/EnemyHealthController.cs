using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    Coin coin;
    [SerializeField]
    Transform coinSpawnpoint;

    public override void SetMaxHealth()
    {
        // can do something similar in MarketHealthController or PlayerHealthController
        // if i want to adjust them too based on difficulty
        switch(SettingsManager.settings.difficulty){
            case Settings.Difficulty.EASY:
                // lower max health of enemies by a bit for easier difficulty
                maxHealth *= 0.6f;
                break;
            case Settings.Difficulty.MEDIUM:
                // medium i want normal difficulty as currently set
                break;
            case Settings.Difficulty.HARD:
                maxHealth *= 1.5f;
                break;
            default:
                Debug.LogError("YO WHY TF IS DIFFICULTY WACK");
                break;
        }
        base.SetMaxHealth();
    }

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
