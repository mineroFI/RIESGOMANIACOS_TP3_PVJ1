using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarriableItem : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    private bool isCarried = false;

    public bool IsCarried => isCarried;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Consigna 4: Emplear SetParent() para asociarlo al punto de transporte
    public void PickUp(Transform holdPoint)
    {
        isCarried = true;

        rb.isKinematic = true;
        col.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    // Consigna 4: Restaurar independencia jerárquica y reactivar físicas
    public void Drop(Vector3 throwDirection, float dropForce = 2f)
    {
        isCarried = false;

        transform.SetParent(null);

        col.enabled = true;
        rb.isKinematic = false;

        rb.AddForce(throwDirection * dropForce, ForceMode.Impulse);
    }
}