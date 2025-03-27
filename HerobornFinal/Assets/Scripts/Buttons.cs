using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void restartGame()
    {
        Utilities.RestartLevel();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
