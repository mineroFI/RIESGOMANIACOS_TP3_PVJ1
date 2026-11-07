using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Configuración del Obstáculo")]
    [SerializeField] private GameObject yarnPrefab;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float initialDelay = 1f;

    [Header("Fuerza de Lanzamiento")]
    [SerializeField] private float launchForce = 12f;

    void Start()
    {
        
        InvokeRepeating(nameof(SpawnObstacle), initialDelay, spawnInterval);
    }

    private void SpawnObstacle()
    {
        if (yarnPrefab == null) return;

        // Consigna 3: Instanciar prefab en la posición y rotación del spawner
        GameObject newYarn = Instantiate(yarnPrefab, transform.position, transform.rotation);


        RollingYarn yarnScript = newYarn.GetComponent<RollingYarn>();
        if (yarnScript != null)
        {
            yarnScript.Launch(transform.forward, launchForce);
        }
    }

}