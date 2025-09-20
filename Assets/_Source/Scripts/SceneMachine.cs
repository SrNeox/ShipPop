using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMachine : MonoBehaviour
{
    private int _sceneMenu = 0;
    private int _sceneGame = 1;

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_sceneMenu);
    }

    public void LoadGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(_sceneGame);
    }
}
