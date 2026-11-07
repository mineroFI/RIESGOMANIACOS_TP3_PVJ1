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
        playerController player = other.GetComponent<playerController>();
        if (player != null)
        {
            player.SetSpawnPoint(transform);
            if (checkPointRenderer != null)
            {
                checkPointRenderer.material.color = Color.green;
            }
        }
    }
}
