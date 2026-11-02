using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [Header("Señales Visuales de Victoria")]
    [SerializeField] private Renderer zoneRenderer;
    [SerializeField] private Color victoryColor = Color.green;
    [SerializeField] private ParticleSystem victoryParticles;

    [Header("Referencia al Objeto")]
    [SerializeField] private string targetTag = "Coin";

    private bool isDelivered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isDelivered) return;

        if (other.CompareTag(targetTag))
        {
            CarriableItem item = other.GetComponent<CarriableItem>();

            if (item != null && !item.IsCarried)
            {
                TriggerVictory(other.gameObject);
            }
        }
    }

    private void TriggerVictory(GameObject deliveredItem)
    {
        isDelivered = true;
        Debug.Log("VICTORIA :D El objeto ha sido entregado exitosamente en la meta.");

        Rigidbody rb = deliveredItem.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
        deliveredItem.transform.position = transform.position + Vector3.up * 0.5f;

        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = victoryColor;
        }

        if (victoryParticles != null)
        {
            victoryParticles.Play();
        }
    }
}