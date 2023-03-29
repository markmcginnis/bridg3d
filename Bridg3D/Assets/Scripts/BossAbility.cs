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

    EnemyHealthController enemyHealthController;
    AudioManager audioManager;

   void Start()
    {
        enemyHealthController = GetComponent<EnemyHealthController>();
        audioManager = GetComponent<AudioManager>();
        StartCoroutine("AbilityTimer");
    }

    IEnumerator AbilityTimer(){
        yield return new WaitForSeconds(timeBetweenAbility);
        AbilityAction();
    }

    void AbilityAction(){
        if(!enemyHealthController.enabled)
            return;
        switch(abilityType){
            case AbilityType.SPAWN:
                audioManager.Play("BossAbilitySpawn");
                Instantiate(yeoman, yeomanSpawn.position, yeomanSpawn.rotation);
                break;
            case AbilityType.HEAL:
                audioManager.Play("BossAbilityHeal");
                enemyHealthController.currentHealth += (0.15f * enemyHealthController.maxHealth);
                break;
            default:
                break;
        }
        StartCoroutine("AbilityTimer");
    }
}