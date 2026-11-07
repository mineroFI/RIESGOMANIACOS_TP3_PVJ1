using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Renderer checkPointRender;

    private void OnTriggerEnter(Collider other)
    {
        playerController player = other.GetComponent<playerController>();

        if (player != null)
        {
            player.SetSpawnPoint(transform);

            if (checkPointRender != null)
            {
                checkPointRender.material.color = Color.green;
            }
        }
    }
}