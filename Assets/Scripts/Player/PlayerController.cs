using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEditor;

public class playerController : MonoBehaviour
{
    public float Speed = 5f;
    public float Gravity = -20f;
    public float JumpHeight = 2f;
    public float rotationSpeed = 10f;
    public Transform spawnPoint;

    Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool jumpPressed;

    private bool isDead = false;
    private bool gameOver = false;


    public Vector3 externalMoveSpeed;



    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (isDead || gameOver) return;


        bool isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (jumpPressed && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            jumpPressed = false;
        }

        verticalVelocity += Gravity * Time.deltaTime;

        // Direccion del movimiento
        // Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        // MOVIMIENTO RELATIVO DE LA CAMARA
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // EVITAR INCLINACION VERTICAL
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        // Invertimos moveInput (-moveInput.x y -moveInput.y) para corregir el modelo volteado
        //Vector3 moveDirection = new Vector3(-moveInput.x, 0f, -moveInput.y);


        // Rotacion del personaje
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Movimiento original
        Vector3 move = moveDirection.normalized;
        //Vector3 move = -moveDirection.normalized;
        move.y = verticalVelocity / Speed;

        controller.Move(move * Speed * Time.deltaTime + externalMoveSpeed * Time.deltaTime);
    }



    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpPressed = true;
        }
    }


    public void SetGameOver()
    {
        gameOver = true;

        moveInput = Vector2.zero;
        jumpPressed = false;
        externalMoveSpeed = Vector3.zero;

    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }   

    public void Die()
    {
        controller.enabled = false;
        transform.position = spawnPoint.position;
        verticalVelocity = 0f;
        controller.enabled = true;

        //Perder una vida
        PlayerLives playerLives = GetComponent<PlayerLives>();

        if (playerLives != null)
        {
            playerLives.LoseLife();
        }
        else
            Debug.Log("Falta el componente PlayerLives");

        //ENEMIGO TERRESTRE
        GroundEnemyAI[] enemiesGround = FindObjectsByType<GroundEnemyAI>(FindObjectsSortMode.None);

        foreach (GroundEnemyAI enemy in enemiesGround)
        {
            enemy.ResetEnemy();
        }

        FloatingEnemy[] enemies = FindObjectsByType<FloatingEnemy>(FindObjectsSortMode.None);

        foreach (FloatingEnemy enemy in enemies)
        {
            enemy.ResetEnemy();
        }

        //StartCoroutine(RespawnCourotine());
    }

}