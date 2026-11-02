using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollingYarn : MonoBehaviour
{
    [Header("Comportamiento")]
    [SerializeField] private float lifetime = 8f;
    [SerializeField] private float pushForce = 15f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Consigna 3: Destruccion automática tras un tiempo de vida
        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector3 direction, float force)
    {
        if (rb != null)
        {
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.transform.root.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.transform.root.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0f;

                if (pushDirection.sqrMagnitude > 0.001f)
                {
                    pushDirection.Normalize();
                    playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }
        }
    }
}