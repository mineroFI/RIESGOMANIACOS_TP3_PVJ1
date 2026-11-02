using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Configuración del Efecto")]
    [SerializeField] private float jumpMultiplier = 1.2f;
    [SerializeField] private float duration = 6f;
    [SerializeField] private float rotationSpeed = 90f;

    [Header("Límite de Usos")]
    [SerializeField] private int maxPickups = 4; // 1 uso inicial + 3 reapariciones 

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
        // Solo rota cuando está visible en la escena
        if (isAvailable)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAvailable) return;

        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null || other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.ApplySuperJump(jumpMultiplier, duration);
            }

            currentPickups++;
            Debug.Log($"Power-Up recogido. Uso {currentPickups} de {maxPickups}.");

            if (currentPickups >= maxPickups)
            {
                Debug.Log("Power-Up agotado definitivamente.");
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(RespawnRoutine());
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {

        isAvailable = false;
        if (itemRenderer != null) itemRenderer.enabled = false;
        if (itemCollider != null) itemCollider.enabled = false;

        yield return new WaitForSeconds(duration);

        // Vuelve a aparecer en el mapa
        if (itemRenderer != null) itemRenderer.enabled = true;
        if (itemCollider != null) itemCollider.enabled = true;
        isAvailable = true;
        Debug.Log("El Power-Up ha reaparecido");
    }
}