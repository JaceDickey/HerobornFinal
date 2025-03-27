using UnityEngine;

public class BlueItemBehavior : MonoBehaviour
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
            Debug.Log("Enemy detection radius halved!");

            EnemyBehavior.ShrinkCollider();
            PlayerBehavior.blueSneakyPickedUp();
        }
    }
}