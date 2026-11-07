using UnityEngine;

public enum EntityType { ObstacleAsteroid, SecurityDrone, JumpBoosterItem, SpaceCoin, MainBattery }
public enum EntityState { Idle, Active, Patrolling, Depleted }

[CreateAssetMenu(fileName = "NuevoEntityData", menuName = "CatSpace/Entity Data (Enemy or Item)")]
public class EntityDataSO : ScriptableObject
{
    [Header("Identificación (Consigna Punto 4)")]
    public string entityName = "Asteroide / Dron";
    public string targetTag = "Player";
    public EntityType entityType = EntityType.ObstacleAsteroid;
    public EntityState initialState = EntityState.Active;

    [Header("Estadísticas de Obstáculo / Enemigo")]
    public int maxHealth = 1;
    public int damage = 1;
    public float chaseOrPushForce = 15f;
    public float lifetime = 8f;

    [Header("Estadísticas de Ítem / PowerUp")]
    public int pointsGranted = 10;
    public float jumpMultiplier = 1.2f;
    public float effectDuration = 6f;
    public float rotationSpeed = 90f;
    public int maxPickups = 4;

    [Header("Visual")]
    public GameObject entityPrefab;
    public Sprite iconSprite;
}