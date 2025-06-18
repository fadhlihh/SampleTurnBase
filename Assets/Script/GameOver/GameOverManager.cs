using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("TurnBasedGameplay");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
