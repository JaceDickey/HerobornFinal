using UnityEngine;

public class OpeningWallScript : MonoBehaviour
{
    public static bool destroyThis = false;
    void Update()
    {
        if (destroyThis == true)
        {
            Destroy(gameObject);
        }
    }
    public static void destroy()
    {
        destroyThis = true;
    }
}
