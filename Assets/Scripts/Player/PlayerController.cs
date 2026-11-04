using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Configuración de Salto")]
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody rb;
    private Vector3 movementInput;
    private bool isGrounded;
    private float originalJumpForce;
    private Quaternion targetRotation;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalJumpForce = jumpForce;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementInput = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }

        // Se calcula la rotación objetivo aquí, pero se aplica en FixedUpdate
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

        Vector3 targetVelocity = movementInput * moveSpeed;
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
        jumpForce = originalJumpForce * multiplier;
        yield return new WaitForSeconds(duration);
        jumpForce = originalJumpForce;
    }
}