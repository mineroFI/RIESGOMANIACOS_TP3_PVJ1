using UnityEngine;


public class FloatingEnemy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoint;

    public float patrolSpeed = 3f;

    [Header("Chase")]
    public float chaseSpeed = 6f;

    public float chaseDuration = 5f;

    [Header("Rotation")]
    public float rotationSpeed = 5f;

    public float reachDistance = 0.2f;

    private int currentPoint = 0;

    private Transform player;

    private bool isChasing = false;

    private float chaseTimer;

    private Vector3 startPosition;

    private Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        // DESPARENTAR PUNTOS
        for (int i = 0; i < patrolPoint.Length; i++)
        {
            patrolPoint[i].parent = null;
        }
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            ChasePlayer();

            chaseTimer -= Time.deltaTime;

            if (chaseTimer <= 0)
            {
                isChasing = false;
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoint.Length == 0)
            return;

        Transform targetPoint = patrolPoint[currentPoint];

        MoveTo(targetPoint.position, patrolSpeed);

        float distance = Vector3.Distance(transform.position, targetPoint.position);

        if (distance <= reachDistance)
        {
            currentPoint++;

            if (currentPoint >= patrolPoint.Length)
            {
                currentPoint = 0;
            }
        }
    }

    void ChasePlayer()
    {
        MoveTo(player.position, chaseSpeed);
    }

    void MoveTo(Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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

    public void ResetEnemy()
    {
        isChasing = false;

        player = null;

        chaseTimer = 0f;

        transform.position = startPosition;

        transform.rotation = startRotation;

    }

}
