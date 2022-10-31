using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    List<MarketController.Upgrade> upgrades;
    public GameObject upgradeItem;

    void Start(){
        //UpdateMenu();
    }
    public void UpdateMenu(){
        upgrades = FindObjectOfType<MarketController>().upgrades;

        for(int i = 0; i < upgrades.Count; i++){
            MarketController.Upgrade upgrade = upgrades[i];

            GameObject upgradeArea = (GameObject)Instantiate(upgradeItem);
            upgradeArea.transform.SetParent(this.transform);
            upgradeArea.transform.localScale = Vector3.one;

            TMP_Text upgradeNameText = upgradeArea.transform.Find("UpgradeName").GetComponent<TMP_Text>();
            upgradeNameText.text = upgrade.name;

            TMP_Text upgradeEffectText = upgradeArea.transform.Find("UpgradeEffect").GetComponent<TMP_Text>();
            upgradeEffectText.text = upgrade.description;

            TMP_Text upgradeCostText = upgradeArea.transform.Find("UpgradeButton/UpgradeCost").GetComponent<TMP_Text>();
            upgradeCostText.text = "$" + ((int)upgrade.cost).ToString();

            Button upgradeButton = upgradeArea.transform.Find("UpgradeButton").GetComponent<Button>();
            upgradeButton.onClick.AddListener(() => { GameObject.FindObjectOfType<MarketController>().BuyUpgrade(upgrade.name); });
        }
    }

    void OnDisable(){
        ResetMenu();
    }

    public void ResetMenu(){
        foreach(Transform child in transform){
            GameObject.Destroy(child.gameObject);
        }
    }
}
