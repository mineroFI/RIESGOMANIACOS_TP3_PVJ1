using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Datos Modulares")]
    [SerializeField] private EntityDataSO itemData;

    [Header("Configuración del Efecto (backup)")]
    [SerializeField] private float jumpMultiplier = 1.2f;
    [SerializeField] private float duration = 6f;
    [SerializeField] private float rotationSpeed = 90f;

    [Header("Límite de Usos (backup)")]
    [SerializeField] private int maxPickups = 4;

    private int currentPickups = 0;
    private Renderer itemRenderer;
    private Collider itemCollider;
    private bool isAvailable = true;

    void Awake()
    {
        itemRenderer = GetComponent<Renderer>();
        itemCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isAvailable)
        {
            float currentRotSpeed = itemData != null ? itemData.rotationSpeed : rotationSpeed;
            transform.Rotate(Vector3.up, currentRotSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAvailable) return;

        string targetTag = itemData != null ? itemData.targetTag : "Player";
        playerController player = other.GetComponentInParent<playerController>();

        if (player != null || other.CompareTag(targetTag))
        {
            float mult = itemData != null ? itemData.jumpMultiplier : jumpMultiplier;
            float dur = itemData != null ? itemData.effectDuration : duration;
            int maxUses = itemData != null ? itemData.maxPickups : maxPickups;
            string itemName = itemData != null ? itemData.entityName : "Power-Up";

            if (player != null)
            {
                player.ApplySuperJump(mult, dur);
            }

            currentPickups++;
            Debug.Log($"{itemName} recogido. Uso {currentPickups} de {maxUses}.");

            if (currentPickups >= maxUses)
            {
                Debug.Log($"{itemName} agotado definitivamente.");
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(RespawnRoutine(dur));
            }
        }
    }

    private IEnumerator RespawnRoutine(float waitDuration)
    {
        isAvailable = false;
        if (itemRenderer != null) itemRenderer.enabled = false;
        if (itemCollider != null) itemCollider.enabled = false;

        yield return new WaitForSeconds(waitDuration);

        if (itemRenderer != null) itemRenderer.enabled = true;
        if (itemCollider != null) itemCollider.enabled = true;
        isAvailable = true;
        Debug.Log("El ítem ha reaparecido");
    }
}