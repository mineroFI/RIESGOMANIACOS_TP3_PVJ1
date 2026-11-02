using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [Header("Punto de Transporte")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float pickupRadius = 2.5f;
    [SerializeField] private LayerMask itemLayer;

    [Header("Teclas")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode dropKey = KeyCode.Q;

    private CarriableItem currentItem = null;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (currentItem == null)
            {
                TryPickUpItem();
            }
            else
            {
                DropItem();
            }
        }

        if (Input.GetKeyDown(dropKey) && currentItem != null)
        {
            DropItem();
        }
    }

    private void TryPickUpItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (Collider hit in hits)
        {
            CarriableItem item = hit.GetComponent<CarriableItem>();
            if (item != null && !item.IsCarried)
            {
                currentItem = item;
                currentItem.PickUp(holdPoint);
                break;
            }
        }
    }

    private void DropItem()
    {
        if (currentItem != null)
        {
            currentItem.Drop(transform.forward, 2f);
            currentItem = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}