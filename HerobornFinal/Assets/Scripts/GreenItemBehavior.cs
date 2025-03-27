using UnityEngine;

public class GreenItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    void Start()
    {                
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            PlayerBehavior.greenJumpPickedUp();
        }
    }
}