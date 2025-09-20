using UnityEngine;

public class StateGame : MonoBehaviour
{
    public static void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
