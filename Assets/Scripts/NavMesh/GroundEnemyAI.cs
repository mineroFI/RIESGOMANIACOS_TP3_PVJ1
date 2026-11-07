using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyAI : MonoBehaviour
{
    [Header("Chase")]
    public float chaseDuration = 5f;

    [Header("Rotatio")]
    public float rotationSpeed = 5f;

    private NavMeshAgent agent;

    private Transform player;

    private bool isChasing = false;

    private float chaseTimer;

    private bool isReturning = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            chaseTimer -= Time.deltaTime;

            agent.SetDestination(player.position);

            RotateTowardsPlayer();

            if (chaseTimer <= 0)
            {
                StopChase();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isChasing)
        {
            player = other.transform;

            isChasing = true;

            chaseTimer = chaseDuration;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void StopChase()
    {
        isChasing = false;

        isReturning = true;

        player = null;

        agent.SetDestination(startPosition);
    }

    public void ResetEnemy()
    {
        StopChase();
    }
}
