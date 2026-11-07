using TMPro;
using UnityEngine;

public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text collectibleText;

    public void UpdateCounter(int amount)
    {
        collectibleText.text = "COINS: " + amount;
    }
}
