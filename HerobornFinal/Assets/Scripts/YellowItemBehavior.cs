using UnityEngine;

public class YellowItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    public static bool pickedUp;
    void Start()
    {               
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Exit elevator unlocked!");
            pickedUp = true;

            OpeningWallScript.destroy();
            PlayerBehavior.yellowKeyPickedUp();
        }
    }
}