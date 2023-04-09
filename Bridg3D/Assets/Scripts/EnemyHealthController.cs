using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    Coin coin;
    [SerializeField]
    GameObject pill;
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

    IEnumerator DelayedDestroy(){
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }

    public override void Die()
    {
        //do the normal die stuff, then spawn a coin with a given value
        base.Die();
        coin.value = GetComponent<EnemyController>().coinValue;
        Instantiate(coin.gameObject, coinSpawnpoint.position, coinSpawnpoint.rotation);
        int chance = Random.Range(0,(int)GetComponent<EnemyController>().strategyLevel);
        Debug.Log(chance);
        if(chance == 0){
            Transform pillPoint = coinSpawnpoint;
            pillPoint.position = new Vector3(pillPoint.position.x, pillPoint.position.y + 0.5f, pillPoint.position.z);
            Instantiate(pill,coinSpawnpoint.position,new Quaternion());
        }
        this.GetComponent<EnemyController>().enabled = false;
        Destroy(GetComponent<KnockbackController>());
        // this.GetComponent<CharacterController>().enabled = false;
        Destroy(GetComponent<CharacterController>());
        this.transform.Find("BodyPlaceholder").gameObject.AddComponent<Rigidbody>().mass = 0.1f;
        this.transform.Find("BodyPlaceholder").gameObject.AddComponent<CapsuleCollider>().gameObject.tag = "DeadEnemy";
        this.transform.Find("WeaponSlot").gameObject.SetActive(false);
        this.transform.Find("ShieldSlot+0.5").gameObject.SetActive(false);
        this.gameObject.tag = "DeadEnemy";
        Vector3 backward = this.transform.forward;
        this.transform.Find("BodyPlaceholder").GetComponent<Rigidbody>().AddForce(this.transform.forward*-50, ForceMode.Force);
        this.enabled = false;
        StartCoroutine(DelayedDestroy());
        //GameObject.Destroy(gameObject);
    }
}
