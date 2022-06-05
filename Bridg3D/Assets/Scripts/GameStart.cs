using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour
{
    public Transform player;
    public Transform playerSpawn;



    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, playerSpawn.position, playerSpawn.rotation);
    }

    public void EndGame(bool win)
    {
        if (win)
        {
            UnityEngine.Debug.Log("won");
            StartCoroutine(loadMenu("EndGameWin"));
        }
        else
        {
            UnityEngine.Debug.Log("lost");
            StartCoroutine(loadMenu("EndGameLoss"));
        }
    }

    IEnumerator loadMenu(string menu)
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(menu);

        yield break;
    }
}
