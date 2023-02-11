using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    public enum AbilityType { SPAWN, HEAL };
    public Transform yeoman;
    public Transform yeomanSpawn;
    public float timeBetweenAbility = 15f;
    public AbilityType abilityType;
    public int healthAmount;

    public EnemyHealthController enemyHealthController;

   void Start()
    {
        enemyHealthController = GetComponent<EnemyHealthController>();
        StartCoroutine("AbilityTimer");
    }

    IEnumerator AbilityTimer(){
        yield return new WaitForSeconds(timeBetweenAbility);
        AbilityAction();
    }

    void AbilityAction(){
        if(!enemyHealthController.enabled)
            return;
        GetComponent<AudioManager>().Play("Boss Ability");
        switch(abilityType){
            case AbilityType.SPAWN:
                Instantiate(yeoman, yeomanSpawn.position, yeomanSpawn.rotation);
                break;
            case AbilityType.HEAL:
                enemyHealthController.currentHealth += (0.15f * enemyHealthController.maxHealth);
                break;
            default:
                break;
        }
        StartCoroutine("AbilityTimer");
    }
}