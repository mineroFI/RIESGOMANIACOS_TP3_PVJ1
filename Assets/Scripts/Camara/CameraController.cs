using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float distance = 5f;
    public float height = 2f;

    public float rotationSpeed = 120f;

    private float currentYaw = 0;
    private bool isDragging = false;


    void Update()
    {
        //DETECTAR CLICK
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isDragging = true;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float mouseX = Mouse.current.delta.ReadValue().x;

            currentYaw += mouseX * rotationSpeed * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;

        }

        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);

        Vector3 offset = new Vector3(0, height, -distance);

        // POCICION DE LA CAMARA
        transform.position = target.position + rotation * offset;

        //ROTACION HAACIA EL PLAYER
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }
}
