using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    //class for holding upgrades and their effects
    [System.Serializable]
    public class Upgrade{
        public string name;
        public string controller;
        public string property;
        public float cost;
        public float impact;
        public enum MathOperation {PLUS, MULTIPLY};
        public MathOperation mathOperation;
        public string description;
        public int currentTier;
        public int totalTiers;
        public bool ConsumeAndRemove(){
            currentTier += 1;
            return currentTier == totalTiers;
        }
    }

    public GameObject upgradeMenuContainer;
    public UpgradeMenu upgradeMenu;
    public bool upgradeMenuOpen = false;

    //public Upgrade[] upgrades;
    public List<Upgrade> upgrades;

    public bool inArea = false;
    [SerializeField]
    GameObject player;

    [SerializeField]
    float healthCost = 10;
    [SerializeField]
    float healthAmount = 10f;

    AudioManager audioManager;
    
    public GameObject rangeTooltip;
    public GameObject rangeTooltip2;

    public void BuyUpgrade(string upgradeID){
        //find upgrade based on "key" value
        Upgrade upgrade = null;
        foreach(Upgrade u in upgrades){
            if(u.name.Equals(upgradeID)){
                upgrade = u;
                break;
            }
        }
        //don't do stuff with no upgrade found
        if(upgrade == null){
            Debug.LogError("upgradeID '" + upgradeID + "' not found");
            return;
        }
        
        //stop if player can't afford it
        WalletController playerWallet = player.GetComponent<WalletController>();
        if(!playerWallet.CanAfford(upgrade.cost)){
            return;
        }
        playerWallet.DecreaseBalance(upgrade.cost);
        audioManager.Play("BuyUpgrade");

        //find the controller we mean to modify
        CustomComponent controller = (CustomComponent)player.GetComponent(System.Type.GetType(upgrade.controller));
        if(controller == null){
            Debug.LogError("controller '" + upgrade.controller + "' not found");
            return;
        }
        //either add or multiply the required field with the required impact number
        if(upgrade.mathOperation == Upgrade.MathOperation.PLUS){
            controller[upgrade.property] = (float)controller[upgrade.property] + upgrade.impact;
        }
        else{
            controller[upgrade.property] = (float)controller[upgrade.property] * upgrade.impact;
        }
        if(upgrade.ConsumeAndRemove()){
            upgrades.Remove(upgrade);
        }
        upgradeMenu.ResetMenu();
        upgradeMenu.UpdateMenu();
    }

    public void BuyHealth(){
        if(!inArea)
            return;
        WalletController playerWallet = player.GetComponent<WalletController>();
        //if player can't afford the cost, stop
        if(!playerWallet.CanAfford(healthCost))
            return;
        HealthController playerHealth = player.GetComponent<HealthController>();
        //don't waste player money
        if(playerHealth.currentHealth == playerHealth.maxHealth)
            return;
        //take their money and heal them
        playerWallet.DecreaseBalance(healthCost);
        playerHealth.Heal(healthAmount);
        audioManager.Play("BuyHealth");
    }

    public bool OpenUpgradeMenu(){
        if(!inArea)
            return false;
        upgradeMenuOpen = true;
        upgradeMenuContainer.SetActive(true);
        player.GetComponentInChildren<MouseLook>().enabled = false;
        player.GetComponent<FPSMovement>().enabled = false;
        upgradeMenu.UpdateMenu();
        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = true;
        return true;
    }

    public void CloseUpgradeMenu(){
        if(!inArea)
            return;
        Debug.Log("closeupgrademenu");
        upgradeMenuOpen = false;
        upgradeMenuContainer.SetActive(false);
        player.GetComponentInChildren<MouseLook>().enabled = true;
        player.GetComponent<FPSMovement>().enabled = true;
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    //keep track of player being in range to buy stuff
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.Equals(player)){
            inArea = true;
            rangeTooltip.SetActive(true);
            rangeTooltip2.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.Equals(player)){
            rangeTooltip.SetActive(false);
            rangeTooltip2.SetActive(false);
            Debug.Log("trigger exit");
            inArea = false;
            upgradeMenuOpen = false;
            upgradeMenuContainer.SetActive(false);
            player.GetComponentInChildren<MouseLook>().enabled = true;
            player.GetComponent<FPSMovement>().enabled = true;
            // Cursor.lockState = CursorLockMode.Locked;
        }
            
    }

    void Start(){
        audioManager = GetComponent<AudioManager>();
    }
}
