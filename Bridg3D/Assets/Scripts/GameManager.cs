using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    WaveSpawner waveSpawner;
    AudioManager audioManager;
    MenuManager menuManager;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public TMP_Text walletText;

    public HealthBar playerHealthBar;
    public HealthBar marketHealthBar;

    public HealthController playerHealthController;
    public HealthController marketHealthController;

    public WalletController wallet;

    public PlayerController playerController;
    
    public float returnToMenuTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        audioManager = GetComponent<AudioManager>();
        menuManager = GetComponent<MenuManager>();
        playerHealthBar.setMaxHealth(playerHealthController.maxHealth);
        marketHealthBar.setMaxHealth(marketHealthController.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Cursor.lockState);
        playerHealthBar.setHealth(playerHealthController.currentHealth);
        marketHealthBar.setHealth(marketHealthController.currentHealth);
        if(playerHealthController.currentHealth <= 0){
            Debug.Log("LOSE CONDITION");
            playerHealthBar.Die();
            returnToMenuTime -= Time.deltaTime;
            if(returnToMenuTime <= 0){
                Cursor.lockState = CursorLockMode.None;
                ReturnToMainMenu();
            }
        }
        if(waveSpawner.nextWave >= waveSpawner.waves.Length){
            Debug.Log("WIN CONDITION");
            returnToMenuTime -= Time.deltaTime;
            if(returnToMenuTime <= 0){
                Cursor.lockState = CursorLockMode.None;
                ReturnToMainMenu();
            }
        }
        if(playerController.pauseMenuOpen){
            Pause();
        }
        else{
            Resume();
        }
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

    public void ReturnToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume(){
        playerController.pauseMenuOpen = false;
        menuManager.Resume();
    }

    public void Pause(){
        playerController.pauseMenuOpen = true;
        menuManager.Pause();
    }
}
