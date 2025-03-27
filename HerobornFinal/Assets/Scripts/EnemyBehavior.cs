using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private SphereCollider sphereCollider;
    public static bool shrinkCheck = false;
    public static bool restartCheck = false;

    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    public Transform player;

    private int _lives = 1;

    public AudioClip[] soundClips;
    private AudioSource audioSource;

    public int EnemyLives
    {
        get {  return _lives; }
        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Defeated!");
            }
        }
    }

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        audioSource = GetComponent<AudioSource>();
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (other.CompareTag("Player"))
            {
                if (soundClips.Length > 0)
                {
                    int randomIndex = Random.Range(0, soundClips.Length);
                    audioSource.clip = soundClips[randomIndex];
                    audioSource.Play();
                }
            }
            agent.destination = player.position;
            GameBehavior.detected = "DETECTED";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
            GameBehavior.detected = "HIDDEN";
        }
    }
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
        if (restartCheck == true)
        {
            RestartCollider();
            shrinkCheck = false;
        }
        if (shrinkCheck == true)
        {
            ShrinkCollider();
            shrinkCheck = false;
        }
    }
    public void ShrinkCollider()
    {
        if (sphereCollider != null)
        {
            float currentRadius = sphereCollider.radius;
            sphereCollider.radius = currentRadius * 0.5f;
            Debug.Log("Enemy Detection Radius Halved!");
        }
        else
        {
            Debug.LogError("SphereCollider component not found!");
        }
    }

    public void RestartCollider()
    {
        if (sphereCollider != null)
        {
            sphereCollider.radius = 12f;
        }
        else
        {
            Debug.LogWarning("No SphereCollider found on this object.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Enemy hurt!");
        }
    }
}
