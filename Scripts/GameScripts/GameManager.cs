using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        LoadVictoryScreen();
    }

    public void StartGame()
    {
        Debug.Log("Iniciando el juego...");
        SceneManager.LoadScene("Game");
    }
    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego...");
        SceneManager.LoadScene("Menu");
        CoinManager.instance = null; 
       // Lógica para reiniciar el juego
    }
    public void PlayerDied()
    {
        Debug.Log("El jugador ha muerto.");
        SceneManager.LoadScene("Defeat");
        // Lógica para manejar la muerte del jugador
    }
    public void LoadVictoryScreen()
    {
        // Busca todos los objetos con el tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && SceneManager.GetActiveScene().name == "Game")
        {
            Debug.Log("No hay enemigos restantes. Cargando pantalla de victoria...");
            SceneManager.LoadScene("Victory");
        }
    }
}

