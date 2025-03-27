using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 50.0f;
    public bool isGrounded;
    public static int jumpCost = 200;

    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    public bool bulletShoot;
    public static int bullets = 10;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;

    public bool stopped = true;
    public bool walking = false;
    public bool sprinting = false;

    public static int sprint = 1000;
    public static int sprintMax = 1000;
    public int sprintMin = 0;
    public static bool sprintBoost = false;

    public AudioSource walkSound;
    public AudioSource runSound;

    public AudioSource ammoPickup;
    public AudioSource powerupPickup;
    public AudioSource keyPickup;

    public AudioSource startSound;

    public static bool powerupAcquired = false;
    public static bool ammoAcquired = false;
    public static bool keyAcquired = false;

    public EnemyBehavior enemy;
    private GameBehavior _gameManager;

    public delegate void JumpingEvent();
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();

    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (sprint > jumpCost)
            {
                _rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                sprint = sprint - jumpCost;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (bullets > 0)
            {
                bulletShoot = true;
                bullets--;
                GameBehavior.bullets = bullets;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                sprinting = true;
                walking = false;
                walkSound.Stop();
                runSound.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                sprinting = false;
                walking = true;
                runSound.Stop();
                walkSound.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = true;
                walking = false;
                walkSound.Stop();
                runSound.Play();
            }
            else
            {
                runSound.Stop();
                walkSound.Play();
                walking = true;
                sprinting = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            sprinting = false;
            walking = false;
            stopped = true;
            walkSound.Stop();
            runSound.Stop();
        }

        if (sprinting == true)
        {
            if (sprint > sprintMin)
            {
                moveSpeed = 30.0f;
                sprint--;
            }
            else
            {
                moveSpeed = 10.0f;
            }
        }
        else
        {
            moveSpeed = 10.0f;
            if ((sprint < sprintMax) && (sprintBoost == true))
            {
                sprint++;
                sprint++;
            }
            else if (sprint < sprintMax)
            {
                sprint++;
            }
        }

        GameBehavior.staminaText = sprint;

        if (powerupAcquired == true)
        {
            playPowerupSound();
            powerupAcquired = false;
        }
        if (ammoAcquired == true)
        {
            playAmmoSound();
            ammoAcquired = false;
        }
        if (keyAcquired == true)
        {
            playKeySound();
            keyAcquired = false;
        }
    }
    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position +
            this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);

        if (bulletShoot)
        {
            GameObject newBullet = Instantiate(bullet,
               this.transform.position + new Vector3(0, 2, 0),
                  this.transform.rotation) as GameObject;

            Rigidbody bulletRB =
                newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
            bulletShoot = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }
        if (collision.gameObject.CompareTag("Enemy") == true)
        {
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.name == "WinRegion")
        {
            SceneManager.LoadScene(3);
        }
    }
    public static void redStaminaPickedUp()
    {
        sprintMax = 2000;
        sprintBoost = true;
        powerupAcquired = true;
    }
    public static void greenJumpPickedUp()
    {
        jumpCost = 100;
        powerupAcquired = true;
    }
    public static void ammoPickedUp()
    {
        bullets = bullets + 10;
        GameBehavior.bullets = bullets;
        ammoAcquired = true;
    }
    public static void blueSneakyPickedUp()
    {
        powerupAcquired = true;
    }
    public static void yellowKeyPickedUp()
    {
        keyAcquired = true;
    }
    public void playPowerupSound()
    {
        powerupPickup.Play();
    }
    public void playKeySound()
    {
        keyPickup.Play();
    }
    public void playAmmoSound()
    {
        ammoPickup.Play();
    }
}