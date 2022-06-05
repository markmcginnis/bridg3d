using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        UnityEngine.Debug.Log("quit");
        Application.Quit();
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
