using UnityEngine;

public class respawn : MonoBehaviour
{
    private Renderer checkPointRenderer;
    private void Start()
    {
        checkPointRenderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.SetPosition(transform);
            if (checkPointRenderer != null)
            {
                checkPointRenderer.material.color = Color.green;
            }
        }
    }
}
