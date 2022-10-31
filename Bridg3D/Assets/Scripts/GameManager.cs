using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    WaveSpawner waveSpawner;
    AudioManager audioManager;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public TMP_Text walletText;

    public HealthBar playerHealthBar;
    public HealthBar marketHealthBar;

    public HealthController playerHealthController;
    public HealthController marketHealthController;

    public WalletController wallet;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        audioManager = GetComponent<AudioManager>();
        playerHealthBar.setMaxHealth(playerHealthController.maxHealth);
        marketHealthBar.setMaxHealth(marketHealthController.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.setHealth(playerHealthController.currentHealth);
        marketHealthBar.setHealth(marketHealthController.currentHealth);
        if(playerHealthController.currentHealth <= 0)
            playerHealthBar.Die();
        if(marketHealthController.currentHealth <= 0)
            marketHealthBar.Die();
        UpdateWaveText(waveSpawner.nextWave+1);
        UpdateEnemyCountText(GameObject.FindGameObjectsWithTag("Enemy").Length);
        UpdateWalletText(wallet.GetBalance());
    }

    void UpdateWaveText(int waveNum){
        waveText.text = "Wave " + waveNum.ToString();
    }

    void UpdateEnemyCountText(int enemyCount){
        enemyCountText.text = "Enemies Left: " + enemyCount.ToString();
    }

    void UpdateWalletText(float balance){
        walletText.text = "Coins: " + ((int)balance).ToString();
    }
}
