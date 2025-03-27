using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities
{
    public static int playerDeaths = 0;
    public static void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;

        PlayerBehavior.bullets = 10;
        PlayerBehavior.sprint = 1000;
        PlayerBehavior.sprintBoost = false;
        PlayerBehavior.sprintMax = 1000;
        PlayerBehavior.jumpCost = 200;
        EnemyBehavior.RestartCollider();
        OpeningWallScript.destroyThis = false;
        GameBehavior.detected = "HIDDEN";
        GameBehavior.bullets = PlayerBehavior.bullets;
    }
}
