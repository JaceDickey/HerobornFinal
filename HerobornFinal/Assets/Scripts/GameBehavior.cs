using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public static int staminaText;
    public static string detected = "HIDDEN";
    public static int bullets = 10;

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Stamina: " +
           staminaText);

        GUI.Box(new Rect(20, 50, 150, 25), "Status: " +
           detected);

        GUI.Box(new Rect(20, 80, 150, 25), "Bullets: " +
           bullets);
    }
}
