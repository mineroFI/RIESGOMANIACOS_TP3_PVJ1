using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPlayerStats", menuName = "CatSpace/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Estadísticas Base")]
    public int baseMaxHealth = 3;
    public float baseMoveSpeed = 6f;
    public float baseJumpForce = 7f;

    [Header("Estadísticas Actuales en Runtime")]
    public int maxHealth = 3;
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public int inventoryCapacity = 1;
    public GameObject playerPrefab;

    public void ResetStats()
    {
        maxHealth = baseMaxHealth;
        moveSpeed = baseMoveSpeed;
        jumpForce = baseJumpForce;
    }

    public void UpgradePropulsion(float percentage)
    {
        moveSpeed += moveSpeed * percentage;
    }

    public void UpgradeSuitResistance(int extraHealth)
    {
        maxHealth += extraHealth;
    }
}