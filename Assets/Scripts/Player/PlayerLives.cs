using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [Header("Lives")]
    public int maxLives = 3;

    private int currentLives;

    //[SerializeField] private LivesUI livesUI;

    private playerController player;

   // [SerializeField] private GameManage gameOverUI;

    private void Start()
    {
        currentLives = maxLives;

        player = GetComponent<playerController>();

        //livesUI.UpdateLives(currentLives);

        Debug.Log("Vidas: " + currentLives);
    }

    public void LoseLife()
    {
        currentLives--;

        currentLives = Mathf.Max(currentLives, 0);

       // livesUI.UpdateLives(currentLives);

        Debug.Log("Vidas Restantes: " + currentLives);

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void RestoreLives()
    {
        currentLives = maxLives;

        //livesUI.UpdateLives(currentLives);

        Debug.Log("¡Vidas recuperadas al máximo!");
    }

    private void GameOver()
    {
        player.SetGameOver();

       // gameOverUI.ShowGameOver();
    }

    public int GetLives()
    {
        return currentLives;
    }
}
