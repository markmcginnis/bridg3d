using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int[] count;
        public float rate;
        public bool bossWave;
    }

    public Transform[] spawnPoints;

    public Wave[] waves;
    public int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public enum SpawnState {SPAWNING, WAITING, COUNTING };

    public SpawnState state = SpawnState.COUNTING;

    public AudioManager audioManager;

    public bool waveStartMusicPlayed = false;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        // audioManager.Play("Post-Wave Song");
        StartCoroutine("StartSong");
    }

    IEnumerator StartSong(){
        yield return new WaitForSeconds(0.01f);
        audioManager.Play("Post-Wave Song");
        yield break;
    }

    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!enemyIsAlive()) //if no enemies are left, continue
            {
                //wave complete, go to next wave, restart counter/states
                waveCompleted();
                waveStartMusicPlayed = false;
                audioManager.Stop("Mid-Wave Song");
                audioManager.Play("Post-Wave Song");
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 2.8f && !waveStartMusicPlayed){
            StartCoroutine(waveStartMusic());
            waveStartMusicPlayed = true;
        }

        if (waveCountdown <= 0) //after countdown, begin spawning wave
        {
            if(state != SpawnState.SPAWNING)
            {
                // GameObject.Find("BetweenWavesSong").GetComponent<AudioSource>().enabled = false;
                // GameObject.Find("DuringWavesSong").GetComponent<AudioSource>().enabled = true;
                StartCoroutine(spawnWave(waves[nextWave])); //spawn wave method
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime; //countdown
        }
    }

    IEnumerator waveStartMusic(){
        audioManager.Stop("Post-Wave Song");
        audioManager.Play("Transition Song");
        yield return new WaitForSeconds(3.5f);
        audioManager.Play("Charge");
        yield return new WaitForSeconds(1.5f);
        audioManager.Play("Mid-Wave Song");
    }

    IEnumerator spawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemies.Length; i++) //loop thru enemy types
        {
            for (int j = 0; j < _wave.count[i]; j++) //loop thru number of each enemy type
            {
                spawnEnemy(_wave.enemies[i]); //spawn that number of enemies
                yield return new WaitForSeconds(1/_wave.rate); //wait before spawning another
            }
        }
        if(_wave.bossWave){
            audioManager.Play("BossIncoming");
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void spawnEnemy(Transform _enemy)
    {
        Transform spawnpoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length - 1)];
        Instantiate(_enemy, spawnpoint.position, spawnpoint.rotation);
        // FindObjectOfType<AudioManager>().Play("enemy_spawn");
        // UnityEngine.Debug.Log("spawning enemy"); //spawn enemy
    }

    bool enemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) //check every once in a while
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                    return false; //return false if there are no enemies left
            }       
        }
        return true; //return true if there are enemies left
    }

    void waveCompleted()
    {
        // GameObject.Find("DuringWavesSong").GetComponent<AudioSource>().enabled = false;
        // GameObject.Find("BetweenWavesSong").GetComponent<AudioSource>().enabled = true;
        waveCountdown = timeBetweenWaves;
        state = SpawnState.COUNTING;
        // foreach(GameObject obj in GameObject.FindGameObjectsWithTag("DeadEnemy"))
        // {
        //     Destroy(obj);
        // }
        if (nextWave + 1 > waves.Length - 1)
        {
            //end game screen with win state
            // FindObjectOfType<AudioManager>().Play("game_over");
            // FindObjectOfType<GameStart>().EndGame(true);
            this.enabled = false;
        }
        else
        {
            nextWave++;
        }
    }

}
