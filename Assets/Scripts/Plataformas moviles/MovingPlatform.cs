using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float waitTime = 2f;

    private Rigidbody rb;
    private Transform currentTarget;
    private bool isWaiting = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Start()
    {
        if (pointB != null)
        {
            currentTarget = pointB;
        }
    }

    void FixedUpdate()
    {
        if (isWaiting || pointA == null || pointB == null) return;

        
        Vector3 newPosition = Vector3.MoveTowards(rb.position, currentTarget.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        
        if (Vector3.Distance(rb.position, currentTarget.position) < 0.05f)
        {
            isWaiting = true;
            Invoke(nameof(SwitchTargetAndResume), waitTime);
        }
    }

    private void SwitchTargetAndResume()
    {
        currentTarget = (currentTarget == pointA) ? pointB : pointA;
        isWaiting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.transform.root.CompareTag("Player"))
        {
            collision.transform.root.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.transform.root.CompareTag("Player"))
        {
            collision.transform.root.SetParent(null);
        }
    }
}