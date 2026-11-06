using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject panelInicio;
    public GameObject panelHUD;
    public GameObject panelPause;
    public GameObject panelGameOver;
    public GameObject panelVictoria;
    public GameState estadoActual;
    public void Start()
    {
        Time.timeScale = 1f;
        estadoActual = GameState.Inicio;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(panelInicio != null)
            {
                panelInicio.SetActive(false);
            }
            estadoActual = GameState.Jugando;
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(estadoActual == GameState.Jugando)
            {
                PauseGame();
            }
            else if(estadoActual == GameState.Pausa)
            {
                ReanudarJuego();
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
            Debug.Log("GAME OVER");
            panelGameOver.SetActive(true);
        }
    }
    public void PauseGame()
    {
        estadoActual = GameState.Pausa;
        
        if(panelPause != null)
        {
            Debug.Log("PAUSA");
            panelPause.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void ReanudarJuego()
    {
        estadoActual = GameState.Jugando;
        Time.timeScale = 1f;
        if(panelPause != null)
        {
            Debug.Log("REANUDAR JUEGO");
            panelPause.SetActive(false);
        }
    }
    public void TogglePanel(GameObject panel)
    {
        if(panel != null)
        {
            bool toggle = panel.activeInHierarchy ? false : true;
            panel.SetActive(toggle);
        }
    }
    public void ChangeScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
    public void Victoria()
    {
        estadoActual = GameState.Victoria;
        Time.timeScale = 0f;
        if(panelVictoria != null)
        {
            panelVictoria.SetActive(true);
            Debug.Log("VICTORIA");
        }
        if(panelHUD != null)
        {
            panelHUD.SetActive(false);
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
