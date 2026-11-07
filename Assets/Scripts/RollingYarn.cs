using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollingYarn : MonoBehaviour
{
    [Header("Datos Modulares")]
    [SerializeField] private EntityDataSO obstacleData;

    [Header("Comportamiento (Respaldo)")]
    [SerializeField] private float lifetime = 8f;
    [SerializeField] private float pushForce = 15f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        float actualLifetime = obstacleData != null ? obstacleData.lifetime : lifetime;
        Destroy(gameObject, actualLifetime);
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
        string targetTag = obstacleData != null ? obstacleData.targetTag : "Player";

        if (collision.gameObject.CompareTag(targetTag) || collision.transform.root.CompareTag(targetTag))
        {
            Rigidbody playerRb = collision.transform.root.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0f;

                if (pushDirection.sqrMagnitude > 0.001f)
                {
                    pushDirection.Normalize();
                    float actualPushForce = obstacleData != null ? obstacleData.chaseOrPushForce : pushForce;
                    playerRb.AddForce(pushDirection * actualPushForce, ForceMode.Impulse);

                    if (obstacleData != null)
                    {
                        Debug.Log($"[{obstacleData.entityName}] impactó al jugador causando {obstacleData.damage} de daño.");
                    }
                }
            }
        }
    }
}