using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public MenuManager menuManager;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public TMP_Text walletText;

    public TMP_Text nextWaveTimerText;

    public Slider attackCooldownSlider;

    public Slider sprintStaminaSlider;
    public FPSMovement movementController;

    public TMP_Text endGameText;

    public HealthBar playerHealthBar;
    public HealthBar marketHealthBar;

    public HealthController playerHealthController;
    public HealthController marketHealthController;

    public WalletController wallet;

    public PlayerController playerController;

    public AttackController attackController;
    
    public float returnToMenuTime = 3f;

    bool soundPlayed = false;

    public bool pauseMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.setMaxHealth(playerHealthController.maxHealth);
        marketHealthBar.setMaxHealth(marketHealthController.maxHealth);
        attackCooldownSlider.minValue = 0f;
        attackCooldownSlider.maxValue = attackController.attackCooldown;
        sprintStaminaSlider.minValue = 0f;
        sprintStaminaSlider.maxValue = movementController.sprintCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.setHealth(playerHealthController.currentHealth);
        marketHealthBar.setHealth(marketHealthController.currentHealth);
        attackCooldownSlider.maxValue = attackController.attackCooldown;
        attackCooldownSlider.value = Mathf.Clamp(1 - attackController.attackTime/attackController.attackCooldown,attackCooldownSlider.minValue,attackCooldownSlider.maxValue);
        sprintStaminaSlider.maxValue = movementController.sprintCapacity;
        sprintStaminaSlider.value = Mathf.Clamp(movementController.sprintTime,sprintStaminaSlider.minValue,sprintStaminaSlider.maxValue);
        if(playerHealthController.currentHealth <= 0){
            // Debug.Log("LOSE CONDITION");
            // endGameText.gameObject.SetActive(true);
            if(!soundPlayed){
                waveSpawner.audioManager.Play("Game_Over");
                soundPlayed = true;
            }
            // endGameText.text = "LOSE!";
            playerHealthBar.Die();
            returnToMenuTime -= Time.deltaTime;
            if(returnToMenuTime <= 0){
                // Cursor.lockState = CursorLockMode.Confined;
                menuManager.LoadEndScene(false);
            }
        }
        if(!waveSpawner.enabled){
            // Debug.Log("WIN CONDITION");
            // endGameText.gameObject.SetActive(true);
            // endGameText.text = "WIN!";
            returnToMenuTime -= Time.deltaTime;
            if(returnToMenuTime <= 0){
                // Cursor.lockState = CursorLockMode.Confined;
                menuManager.LoadEndScene(true);
            }
        }
        if(playerController.pauseMenuOpen != pauseMenuOpen){
            pauseMenuOpen = playerController.pauseMenuOpen;
            // Debug.Log("pause state changed");
            if(pauseMenuOpen){
                menuManager.Pause();
            }
            else{
                menuManager.Resume();
            }
        }
        if(marketHealthController.currentHealth <= 0)
            marketHealthBar.Die();
        UpdateWaveText(waveSpawner.nextWave+1);
        UpdateEnemyCountText(GameObject.FindGameObjectsWithTag("Enemy").Length);
        UpdateWalletText(wallet.GetBalance());
        UpdateNextWaveTimerText(waveSpawner.waveCountdown);
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

    void UpdateNextWaveTimerText(float time){
        nextWaveTimerText.text = "Next Wave in " + ((int)time).ToString() + " Seconds";
    }
}
