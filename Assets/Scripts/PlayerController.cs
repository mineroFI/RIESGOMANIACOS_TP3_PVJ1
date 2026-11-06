using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Datos Modulares")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Configuración de Respawn")]
    [SerializeField] private LayerMask capaSuelo;
    private Transform spawnPoint;

    [Header("Configuración de Movimiento (Respaldo)")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Configuración de Salto (Respaldo)")]
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody rb;
    private Vector3 movementInput;
    private bool isGrounded;
    private float currentJumpMultiplier = 1f;
    private Quaternion targetRotation;
    //efrian: aqui coloco lo de la vida del jugador si necesitan hacer algomas pueden usar la referancia de vida para hacer que los enemigos 
    //detecten la vida del jugador
    public int vida = 3;

    public float MoveSpeed
    {
        get => playerStats != null ? playerStats.moveSpeed : moveSpeed;
        set
        {
            if (playerStats != null) playerStats.moveSpeed = value;
            else moveSpeed = value;
        }
    }

    public float CurrentJumpForce => (playerStats != null ? playerStats.jumpForce : jumpForce) * currentJumpMultiplier;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementInput = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, CurrentJumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }

        if (movementInput.sqrMagnitude > 0.05f)
        {
            targetRotation = Quaternion.LookRotation(movementInput, Vector3.up);
        }
    }

    void FixedUpdate()
    {
        if (movementInput.sqrMagnitude > 0.05f)
        {
            Quaternion nextRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * 100f * Time.fixedDeltaTime);
            rb.MoveRotation(nextRotation);
        }

        Vector3 targetVelocity = movementInput * MoveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.6f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void ApplySuperJump(float multiplier, float duration)
    {
        StartCoroutine(SuperJumpRoutine(multiplier, duration));
    }

    private IEnumerator SuperJumpRoutine(float multiplier, float duration)
    {
        currentJumpMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        currentJumpMultiplier = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & capaSuelo) != 0)
        {
            daño();
            if (vida > 0)
            {
                Respawn();
            }
            
        }
    }

    public void Respawn()
    {
        if (spawnPoint != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = spawnPoint.position;
        }
    }

    public void SetPosition(Transform newSpaunPosition)
    {
        spawnPoint = newSpaunPosition;
    }
    public void daño()
    {
        vida --;
        if (vida <= 0)
        {
            Debug.Log("Game Over");
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }
    }
}