using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    private bool levelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (levelCompleted)
        {
            return;
        }

        Debug.Log("¡Algo entró en el punto final!: " + other.gameObject.name);

        if (!other.CompareTag("Objeto"))
        { return; }

        levelCompleted = true;

        Debug.Log("VICTORIA!");
    }
}
