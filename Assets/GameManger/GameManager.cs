using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject panelInicio;
    public GameObject panelHUD;
    public GameObject panelPause;
    public GameObject panelGameOver;
    public GameObject panelVictoria;
    public GameState estadoActual;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(panelInicio != null)
            {
                panelInicio.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(panelPause != null)
            {
                panelPause.SetActive(true);
            }
        }
    }
    public void createPanel()
    {
        if(panelHUD != null && panelInicio != null)
        {
            panelHUD.SetActive(true);
        }
    }
    public void GameOver()
    {
        if(panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }
    public void Victoria()
    {
        if(panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }
    }
    public enum GameState
    {
        Inicio,
        Jugando,
        Pausa,
        GameOver,
        Victoria
    }

}
